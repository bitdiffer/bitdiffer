using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BitDiffer.Client.Properties;
using BitDiffer.Common.Misc;

namespace BitDiffer.Client.Forms
{
	public partial class ProjectSetup : Form
	{
		private ComparisonSet _set;

		public ProjectSetup()
		{
			InitializeComponent();
		}

		public ProjectSetup(ComparisonSet set)
			: this()
		{
			this.Icon = Resources.App;
			tsbFiles.Checked = true;
			_set = set;

			compareFiles.LoadSet(set);
            referencePaths.LoadOptions(set.Config);
            compareOptions.LoadOptions(set.Config);
			viewFilter.LoadFilter(set.Filter);

			ProfessionalColorTable colorTable = new ProfessionalColorTable();
			colorTable.UseSystemColors = true;
			toolStrip1.Renderer = new ToolStripProfessionalRenderer(colorTable); 

			compareFiles.BackColor =
			compareOptions.BackColor =
            referencePaths.BackColor = 
			viewFilter.BackColor = ((ToolStripProfessionalRenderer)toolStrip1.Renderer).ColorTable.ToolStripPanelGradientEnd;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (Program.IsRuntime)
			{
				btnOK.Focus();
				btnOK.Select();
			}
		}

		private void tsbFiles_Click(object sender, EventArgs e)
		{
			if (!tsbFiles.Checked)
			{
				tsbViewFilter.Checked = false;
				tsbOptions.Checked = false;
                tsbReferences.Checked = false;
				tsbFiles.Checked = true;
				compareFiles.BringToFront();
			}
		}

		private void tsbOptions_Click(object sender, EventArgs e)
		{
			if (!tsbOptions.Checked)
			{
				tsbFiles.Checked = false;
				tsbViewFilter.Checked = false;
                tsbReferences.Checked = false;
				tsbOptions.Checked = true;
				compareOptions.BringToFront();
			}
		}

		private void tsbDirectories_Click(object sender, EventArgs e)
		{
			if (!tsbViewFilter.Checked)
			{
				tsbFiles.Checked = false;
				tsbOptions.Checked = false;
				tsbViewFilter.Checked = true;
                tsbReferences.Checked = false;
                viewFilter.BringToFront();
			}
		}

        private void tsbReferences_Click(object sender, EventArgs e)
        {
            if (!tsbReferences.Checked)
            {
                tsbFiles.Checked = false;
                tsbOptions.Checked = false;
                tsbViewFilter.Checked = false;
                tsbReferences.Checked = true;
                referencePaths.BringToFront();
            }
        }

		private void btnOK_Click(object sender, EventArgs e)
		{
			compareFiles.SaveSet(_set);
            referencePaths.SaveOptions(_set.Config);
			compareOptions.SaveOptions(_set.Config);
			viewFilter.SaveFilter(_set.Filter);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			if (tsbFiles.Checked)
			{
				ShowHelp("Assembly Selection.html");
			}
			else if (tsbOptions.Checked)
			{
				ShowHelp("Configuration.html");
			}
			else if (tsbViewFilter.Checked)
			{
				ShowHelp("View Filter.html");
			}
		}

		private void ShowHelp(string topic)
		{
			Help.ShowHelp(this, Program.HelpFile, topic);
		}
	}
}