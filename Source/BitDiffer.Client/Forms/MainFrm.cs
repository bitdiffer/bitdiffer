using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

using BitDiffer.Common.Utility;
using BitDiffer.Client.Properties;
using BitDiffer.Common.Misc;
using BitDiffer.Core;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.TraceListeners;
using BitDiffer.Common.Configuration;
using BitDiffer.Client.Controls;
using BitDiffer.Client.MailSender;
using BitDiffer.Client.Models;
using BitDiffer.Common.Exceptions;

namespace BitDiffer.Client.Forms
{
	public partial class MainFrm : Form, IHandleProgress
	{
		private bool _enableEvents;
		private bool _dirty;
		private string _tipText;
		private const string _filter = "Comparison Sets|*.cset";
		private Progress _progress;
		private AssemblyComparison _ac;
		private ComparisonSet _set;
		private ErrorLevelTraceListener _eltl = new ErrorLevelTraceListener();

		public MainFrm()
		{
			InitializeComponent();

			if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
			{
				this.Icon = Resources.App;

				//ProfessionalColorTable colorTable = new ProfessionalColorTable();
				//colorTable.UseSystemColors = true; 
				//statusStrip1.Renderer = new ToolStripProfessionalRenderer(colorTable);
				//toolStrip1.Renderer = new ToolStripProfessionalRenderer(colorTable); 

				Trace.Listeners.Add(_eltl);

			    ProgramArguments args = new ProgramArguments();

			    try
			    {
                    args.Parse(false, Environment.GetCommandLineArgs());

                    if (args.Help)
                    {
                        ShowHelp();
                    }

                    if (args.ComparisonSet.Items.Count > 0)
                    {
                        _set = args.ComparisonSet;

                        this.Show();

                        UpdateCheckStates();
                        UpdateTitleBar();

                        Compare();
                    }
                }
			    catch (ArgumentParserException ex)
			    {
                    MessageBox.Show(this, ex.Message, "Error Parsing Arguments", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

			    Application.Idle += new EventHandler(Application_Idle);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			UpdateTitleBar();

			Log.Info("{0} GUI", Constants.ProductName);
			Log.Info("Version {0} ({1:d})", Assembly.GetExecutingAssembly().GetName().Version, DateTime.Today);
		}

		private void Compare()
		{
			SetStatusWorking();

			_eltl.Reset();
			traceViewer1.Clear();

			_progress = new Progress();
			backgroundWorker1.RunWorkerAsync();
			_progress.ShowDialog(this);
		}

		private void ApplyFilter()
		{
			_ac.Recompare(_set.Filter);

			navigator.LoadFrom(_ac, true);
		}

		private void SetStatusOK()
		{
			SetStatus(Resources.OK, "Ready");
		}

		private void SetStatusWorking()
		{
			SetStatus(Resources.clock, "Working...");
		}

		private void SetStatus(TraceLevel traceLevel)
		{
			switch (traceLevel)
			{
				case TraceLevel.Error:
					SetStatus(Resources.Critical, "Completed with errors. Check the log messages for details.");
					_tipText = "Errors occurred during the comparison. Check the log messages for details.";
					resultToolTip.ToolTipTitle = "Completed With Errors";
					resultToolTip.ToolTipIcon = ToolTipIcon.Error;
					showTipTimer.Start();
					break;
				case TraceLevel.Warning:
					SetStatus(Resources.Warning, "Completed with warnings. Check the log messages for details.");
					_tipText = "Warnings occurred during the comparison. Check the log messages for details.";
					resultToolTip.ToolTipTitle = "Completed With Warnings";
					resultToolTip.ToolTipIcon = ToolTipIcon.Warning;
					showTipTimer.Start();
					break;
				default:
					SetStatus(Resources.OK, "Ready");
					break;
			}
		}

		private void showTipTimer_Tick(object sender, EventArgs e)
		{
			showTipTimer.Stop();
			resultToolTip.Show(_tipText, this, 4, this.Height - 90, 2500);
		}

		private void SetStatus(Image image, string text)
		{
			statusStripIcon.Image = image;
			statusStripStatusText.Text = " " + text;
			Update();
		}

		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			Compare();
		}

		private void navigator_SelectedAssemblyGroupChanged(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				AssemblyGroup grp = navigator.SelectedAssemblyGroup;
				changeInfoDetail1.LoadFrom(grp);
				diffViewer1.LoadFrom(grp);

				if (!string.IsNullOrEmpty(_set.Filter.TextFilter))
				{
					diffViewer1.ExpandAll();
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void diffViewer1_SelectedDetailItemChanged(object sender, EventArgs e)
		{
			ICanCompare item = diffViewer1.SelectedItem;

			if (item != null)
			{
				changeInfoDetail1.LoadFromItem(item, diffViewer1.SelectedColumnIndex == 0);
				return;
			}

			AssemblyGroup grp = diffViewer1.SelectedGroupItem;

			if (grp != null)
			{
				changeInfoDetail1.LoadFrom(grp);
				return;
			}

			changeInfoDetail1.Clear();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			using (new BitDiffer.Common.Utility.Stopwatch("Main loop"))
			{
				e.Result = new AssemblyComparer(this).CompareAssemblies(_set);
			}
		}

        private void DoLoadSet(string fileName)
        {
            _set = LoadSet(fileName);
            _dirty = false;

            UserPrefs.LastSelectedComparisonSet = fileName;

            UpdateCheckStates();
            UpdateTitleBar();

            Compare();
        }

		private ComparisonSet LoadSet(string fileName)
		{
			_enableEvents = true;

			try
			{
				using (FileStream fs = File.Open(fileName, FileMode.Open))
				{
					XmlSerializer xs = new XmlSerializer(typeof(ComparisonSet));
					ComparisonSet set = (ComparisonSet)xs.Deserialize(fs);
					set.FileName = fileName;
					return set;
				}
			}
			finally
			{
				_enableEvents = true;
			}
		}

		private void SaveSet(bool promptRename)
		{
			if (_set.FileName == null || promptRename)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = _filter;

				string lastSelected = UserPrefs.LastSelectedComparisonSet;

				if (!string.IsNullOrEmpty(lastSelected))
				{
					sfd.InitialDirectory = Path.GetDirectoryName(lastSelected);
				}

				if (sfd.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				UserPrefs.LastSelectedComparisonSet = sfd.FileName;

				_set.FileName = sfd.FileName;
			}

			using (FileStream fs = File.Open(_set.FileName, FileMode.Create))
			{
				XmlSerializer xs = new XmlSerializer(typeof(ComparisonSet));
				xs.Serialize(fs, _set);

				_dirty = false;
				UpdateTitleBar();
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			lock (this)
			{
				_progress.UpdateProgress((ProgressStatus)e.UserState);
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_progress.Close();
			_progress = null;

			if (e.Error != null)
			{
				ThreadExceptionDialog ted = new ThreadExceptionDialog(e.Error);
				ted.ShowDialog();
			}
			else
			{
				SetStatus(_eltl.HighestLoggedLevel);

				_ac = (AssemblyComparison)e.Result;

				if (_ac != null)
				{
					ApplyFilter();
				}
			}
		}

		private delegate void SetMaxRangeDelegate(int max);
		public void SetMaxRange(int max)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new SetMaxRangeDelegate(SetMaxRange), max);
			}
			else
			{
				_progress.SetMaxRange(max);
			}
		}

		public bool CancelRequested
		{
			get { return _progress.CancelRequested; }
		}

		public void UpdateProgress(ProgressStatus progress)
		{
			backgroundWorker1.ReportProgress(0, progress);
		}

		private void tstbTextFilter_TextChanged(object sender, EventArgs e)
		{
			if (_enableEvents)
			{
				typeFilterTimer.Stop();
				typeFilterTimer.Start();
			}
		}

		private void typeFilterTimer_Tick(object sender, EventArgs e)
		{
			typeFilterTimer.Stop();
			_set.Filter.TextFilter = tstbTextFilter.Text;
			ApplyFilter();
		}

		private void tsmiPublic_Click(object sender, EventArgs e)
		{
			if (_enableEvents)
			{
				_dirty = true;
				tsmiPublic.Checked = !tsmiPublic.Checked;
				_set.Filter.IncludePublic = tsmiPublic.Checked;
				ApplyFilter();
			}
		}

        private void tsmiProtected_Click(object sender, EventArgs e)
        {
            if (_enableEvents)
            {
                _dirty = true;
                tsmiProtected.Checked = !tsmiProtected.Checked;
                _set.Filter.IncludeProtected = tsmiProtected.Checked;
                ApplyFilter();
            }
        }

        private void tsmiInternal_Click(object sender, EventArgs e)
        {
            if (_enableEvents)
            {
                _dirty = true;
                tsmiInternal.Checked = !tsmiInternal.Checked;
                _set.Filter.IncludeInternal = tsmiInternal.Checked;
                ApplyFilter();
            }
        }

        private void tsmiPrivate_Click(object sender, EventArgs e)
        {
            if (_enableEvents)
            {
                _dirty = true;
                tsmiPrivate.Checked = !tsmiPrivate.Checked;
                _set.Filter.IncludePrivate = tsmiPrivate.Checked;
                ApplyFilter();
            }
        }

        private void tsmiCompareMethods_Click(object sender, EventArgs e)
		{
			if (_enableEvents)
			{
				_dirty = true;
				tsmiShowImplChanges.Checked = !tsmiShowImplChanges.Checked;
				_set.Filter.CompareMethodImplementations = tsmiShowImplChanges.Checked;
				ApplyFilter();
			}
		}

		private void tsmiIgnoreAttrChanges_Click(object sender, EventArgs e)
		{
			if (_enableEvents)
			{
				_dirty = true;
				tsmiIgnoreAttrChanges.Checked = !tsmiIgnoreAttrChanges.Checked;
				_set.Filter.IgnoreAssemblyAttributeChanges = tsmiIgnoreAttrChanges.Checked;
				ApplyFilter();
			}
		}

		private void tsmiShowChangesOnly_Click(object sender, EventArgs e)
		{
			if (_enableEvents)
			{
				_dirty = true;
				tsmiShowChangesOnly.Checked = !tsmiShowChangesOnly.Checked;
				_set.Filter.ChangedItemsOnly = tsmiShowChangesOnly.Checked;
				ApplyFilter();
			}
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			bool enable = (_ac != null);

			tsbPrint.Enabled = tsmiPrintAll.Enabled = tsmiPrintSelected.Enabled = tsmiPrintPreview.Enabled = tsbPrintSelected.Enabled = tsbPrintAll.Enabled = enable;
			tsbSendTo.Enabled = tsmiSendAll.Enabled = tsmiSendSelected.Enabled = tsbSendSelected.Enabled = tsbSendAll.Enabled = enable;
			tsbExport.Enabled = tsmiExport.Enabled = enable;
			tsbRefresh.Enabled = enable;
			tsbView.Enabled = enable;
			tslTextFilter.Enabled = enable;
			tstbTextFilter.Enabled = enable;

			enable = (_set != null);
			tsmiConfig.Enabled = tsbConfigure.Enabled = enable;
			tsbSave.Enabled = tsmiSave.Enabled = (enable && _dirty);
			tsmiSaveAs.Enabled = enable;
		}

		private void tsbNew_Click(object sender, EventArgs e)
		{
			if (!CheckSaveChanges())
			{
				return;
			}

			_set = new ComparisonSet();

			if (EditProject())
			{
				UpdateCheckStates();
				UpdateTitleBar();

				Compare();
			}
		}

		private bool CheckSaveChanges()
		{
			if (!_dirty)
			{
				return true;
			}

			switch (MessageBox.Show(this, string.Format("You have unsaved changes. Save changes to comparison set {0}?", Path.GetFileName(_set.FileName)), "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
			{
				case DialogResult.Yes:
					this.SaveSet(false);
					return true;
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				default:
					return false;
			}
		}

		private void tsbOpen_Click(object sender, EventArgs e)
		{
			if (!CheckSaveChanges())
			{
				return;
			}

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = _filter;

			string lastSelected = UserPrefs.LastSelectedComparisonSet;

			if (!string.IsNullOrEmpty(lastSelected))
			{
				ofd.InitialDirectory = Path.GetDirectoryName(lastSelected);
			}

			if (ofd.ShowDialog() == DialogResult.OK)
			{
			    if (Path.GetExtension(ofd.FileName).ToUpper() != ".CSET")
				{
					MessageBox.Show(this, "Please select an existing comparison set (*.cset) file.\n\nTo compare a new set of binaries, select 'New' from the toolbar.", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}

			    DoLoadSet(ofd.FileName);
			}
		}

	    private void tsbSave_Click(object sender, EventArgs e)
		{
			SaveSet(false);
		}

		private void tsmiSaveAs_Click(object sender, EventArgs e)
		{
			SaveSet(true);
		}

		private void tsmiExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void tsmiAbout_Click(object sender, EventArgs e)
		{
			AboutBox ab = new AboutBox();
			ab.ShowDialog();
		}

		private void tsbHelp_Click(object sender, EventArgs e)
		{
            ShowHelp();
		}

        private void UpdateCheckStates()
		{
			_enableEvents = false;

			try
			{
				tsmiShowImplChanges.Checked = _set.Filter.CompareMethodImplementations;
			    tsmiPublic.Checked = _set.Filter.IncludePublic;
			    tsmiProtected.Checked = _set.Filter.IncludeProtected;
			    tsmiInternal.Checked = _set.Filter.IncludeInternal;
			    tsmiPrivate.Checked = _set.Filter.IncludePrivate;
				tsmiIgnoreAttrChanges.Checked = _set.Filter.IgnoreAssemblyAttributeChanges;
				tsmiShowChangesOnly.Checked = _set.Filter.ChangedItemsOnly;
				tstbTextFilter.Text = _set.Filter.TextFilter;
			}
			finally
			{
				_enableEvents = true;
			}
		}

		private void UpdateTitleBar()
		{
			if (_set == null || _set.FileName == null)
			{
				this.Text = string.Format("{0} {1}", Constants.ProductName, Assembly.GetExecutingAssembly().GetName().Version);
			}
			else
			{
                this.Text = string.Format("{0} - {1} {2}", Path.GetFileName(_set.FileName), Constants.ProductName, Assembly.GetExecutingAssembly().GetName().Version);
			}
		}

		private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CheckSaveChanges())
			{
				e.Cancel = true;
				return;
			}
		}

		private void tsmiConfig_Click(object sender, EventArgs e)
		{
			if (EditProject())
			{
				Compare();
			}
		}

		private void tsbConfigure_Click(object sender, EventArgs e)
		{
			if (EditProject())
			{
				Compare();
			}
		}

		private bool EditProject()
		{
			if (_set != null)
			{
				ProjectSetup ps = new ProjectSetup(_set);

				if (ps.ShowDialog(this) == DialogResult.OK)
				{
					_dirty = true;
					return true;
				}
			}

			return false;
		}

		private void tsmiCopy_Click(object sender, EventArgs e)
		{
			changeInfoDetail1.Copy();
		}

		private void tsmiPrintSelected_Click(object sender, EventArgs e)
		{
			PrintSelected();
		}

		private void tsbPrintSelected_Click(object sender, EventArgs e)
		{
			PrintSelected();
		}

		private void PrintSelected()
		{
			changeInfoDetail1.Print();
		}

		private void tsmiPrintPreview_Click(object sender, EventArgs e)
		{
			changeInfoDetail1.PrintPreview();
		}

		private void tsbPrintAll_Click(object sender, EventArgs e)
		{
			PrintAll();
		}

		private void tsmiPrintAll_Click(object sender, EventArgs e)
		{
			PrintAll();
		}

		private void PrintAll()
		{
			if (!CheckFilterSanity())
			{
				return;
			}

			auxWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(auxWebBrowser_PrintContent);
			auxWebBrowser.DocumentText = GetFullReportText();
		}

		private string GetFullReportText()
		{
			StringBuilder sb = new StringBuilder();

			using (StringWriter sw = new StringWriter(sb))
			{
				HtmlUtility.WriteHtmlStart(sw);
				this.navigator.SelectedAssemblyGroup.WriteHtmlReport(sw);
				HtmlUtility.WriteHtmlEnd(sw);
			}

			return sb.ToString();
		}

		private bool CheckFilterSanity()
		{
			if ((!_set.Filter.ChangedItemsOnly || _set.Filter.IncludePrivate || _set.Filter.IncludeInternal) && (string.IsNullOrEmpty(_set.Filter.TextFilter)))
			{
				if (MessageBox.Show(this, "Your current view filter will be set to show only public or protected changed items excluding implementation before printing all items. To reset the filter, use the dropdown menu on the toolbar.\n\nTo archive unfiltered data, use the 'export' option.", "Filter Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
				{
					return false;
				}

				_set.Filter.ChangedItemsOnly = true;
			    _set.Filter.IncludePrivate = false;
                _set.Filter.IncludeInternal = false;
                _set.Filter.CompareMethodImplementations = false;

				ApplyFilter();
				UpdateCheckStates();
			}

			return true;
		}

		void auxWebBrowser_PrintContent(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			auxWebBrowser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(auxWebBrowser_PrintContent);
			auxWebBrowser.ShowPrintDialog();
		}

		private void tsbSendSelected_Click(object sender, EventArgs e)
		{
			SendSelected();
		}

		private void tsmiSendSelected_Click(object sender, EventArgs e)
		{
			SendSelected();
		}

		private void SendSelected()
		{
			changeInfoDetail1.SendTo();
		}

		private void tsbExport_Click(object sender, EventArgs e)
		{
			Export();
		}

		private void tsmiExport_Click(object sender, EventArgs e)
		{
			Export();
		}

		private void Export()
		{
			if (this.navigator.SelectedAssemblyGroup == null)
			{
				return;
			}

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "HTML Report (*.html)|*.html|XML Report (*.xml)|*.xml";
			sfd.CheckPathExists = true;
			sfd.AddExtension = true;

			if (sfd.ShowDialog(this) == DialogResult.OK)
			{
				_ac.WriteReport(sfd.FileName, AssemblyComparisonXmlWriteMode.Normal);

				Process.Start(sfd.FileName);
			}
		}

		private void tsbSendAll_Click(object sender, EventArgs e)
		{
			SendAll();
		}

		private void tsmiSendAll_Click(object sender, EventArgs e)
		{
			SendAll();
		}

		private void SendAll()
		{
			if (!CheckFilterSanity())
			{
				return;
			}

			MailSenderController.SendEmail(this, Constants.ComparisonEmailSubject, GetFullReportText());
		}

        private void tsmiHelpSearch_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void ShowHelp()
        {
            Process.Start(Constants.HelpUrl);
        }
	}
}

