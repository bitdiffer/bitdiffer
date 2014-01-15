namespace BitDiffer.Client.Forms
{
	partial class MainFrm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.changeInfoDetail1 = new BitDiffer.Client.Controls.ChangeInfoDetail();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.traceViewer1 = new BitDiffer.Client.Controls.TraceViewer();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.diffViewer1 = new BitDiffer.Client.Controls.DiffViewer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.navigator = new BitDiffer.Client.Controls.AssemblyGroupNavigator();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.auxWebBrowser = new System.Windows.Forms.WebBrowser();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPrintSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrintAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSendSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSendAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelpSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbPrint = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbPrintSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPrintAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbConfigure = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbSendTo = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbSendSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSendAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbView = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiShowChangesOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.showTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPublic = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProtected = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInternal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrivate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowImplChanges = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIgnoreAttrChanges = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tslTextFilter = new System.Windows.Forms.ToolStripLabel();
            this.tstbTextFilter = new System.Windows.Forms.ToolStripTextBox();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.typeFilterTimer = new System.Windows.Forms.Timer(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.resultToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.showTipTimer = new System.Windows.Forms.Timer(this.components);
            this.windowStatePersister1 = new BitDiffer.Client.Controls.WindowStatePersister(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowStatePersister1)).BeginInit();
            this.SuspendLayout();
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "&Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.ItemSize = new System.Drawing.Size(125, 18);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1108, 348);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.changeInfoDetail1);
            this.tabPage1.ImageKey = "book_open.bmp";
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1100, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // changeInfoDetail1
            // 
            this.changeInfoDetail1.BackColor = System.Drawing.SystemColors.Window;
            this.changeInfoDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeInfoDetail1.Location = new System.Drawing.Point(3, 3);
            this.changeInfoDetail1.Name = "changeInfoDetail1";
            this.changeInfoDetail1.Size = new System.Drawing.Size(1094, 316);
            this.changeInfoDetail1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.traceViewer1);
            this.tabPage2.ImageKey = "appwindow_info_annotation_16.bmp";
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1100, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log Messages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // traceViewer1
            // 
            this.traceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.traceViewer1.Location = new System.Drawing.Point(3, 3);
            this.traceViewer1.Name = "traceViewer1";
            this.traceViewer1.Size = new System.Drawing.Size(1094, 316);
            this.traceViewer1.TabIndex = 4;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "appwindow_info_annotation_16.bmp");
            this.imageList1.Images.SetKeyName(1, "book_open.bmp");
            // 
            // diffViewer1
            // 
            this.diffViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diffViewer1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diffViewer1.Location = new System.Drawing.Point(0, 0);
            this.diffViewer1.Name = "diffViewer1";
            this.diffViewer1.Size = new System.Drawing.Size(1108, 346);
            this.diffViewer1.TabIndex = 3;
            this.diffViewer1.SelectedDetailItemChanged += new System.EventHandler(this.diffViewer1_SelectedDetailItemChanged);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.auxWebBrowser);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1276, 698);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1276, 747);
            this.toolStripContainer1.TabIndex = 6;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.navigator);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1276, 698);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // navigator
            // 
            this.navigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigator.Location = new System.Drawing.Point(0, 0);
            this.navigator.Name = "navigator";
            this.navigator.Size = new System.Drawing.Size(163, 698);
            this.navigator.TabIndex = 0;
            this.navigator.SelectedAssemblyGroupChanged += new System.EventHandler(this.navigator_SelectedAssemblyGroupChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.diffViewer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(1108, 698);
            this.splitContainer2.SplitterDistance = 346;
            this.splitContainer2.TabIndex = 0;
            // 
            // auxWebBrowser
            // 
            this.auxWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.auxWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.auxWebBrowser.Name = "auxWebBrowser";
            this.auxWebBrowser.Size = new System.Drawing.Size(50, 50);
            this.auxWebBrowser.TabIndex = 4;
            this.auxWebBrowser.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1276, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiOpen,
            this.toolStripSeparator,
            this.tsmiSave,
            this.tsmiSaveAs,
            this.toolStripMenuItem1,
            this.tsmiPrintSelected,
            this.tsmiPrintAll,
            this.tsmiPrintPreview,
            this.toolStripSeparator3,
            this.tsmiExit});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(36, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // tsmiNew
            // 
            this.tsmiNew.Image = ((System.Drawing.Image)(resources.GetObject("tsmiNew.Image")));
            this.tsmiNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmiNew.Size = new System.Drawing.Size(191, 22);
            this.tsmiNew.Text = "&New";
            this.tsmiNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpen.Image")));
            this.tsmiOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiOpen.Size = new System.Drawing.Size(191, 22);
            this.tsmiOpen.Text = "&Open";
            this.tsmiOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(188, 6);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSave.Image")));
            this.tsmiSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSave.Size = new System.Drawing.Size(191, 22);
            this.tsmiSave.Text = "&Save";
            this.tsmiSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsmiSaveAs
            // 
            this.tsmiSaveAs.Name = "tsmiSaveAs";
            this.tsmiSaveAs.Size = new System.Drawing.Size(191, 22);
            this.tsmiSaveAs.Text = "Save &As";
            this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(188, 6);
            // 
            // tsmiPrintSelected
            // 
            this.tsmiPrintSelected.Image = ((System.Drawing.Image)(resources.GetObject("tsmiPrintSelected.Image")));
            this.tsmiPrintSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiPrintSelected.Name = "tsmiPrintSelected";
            this.tsmiPrintSelected.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsmiPrintSelected.Size = new System.Drawing.Size(191, 22);
            this.tsmiPrintSelected.Text = "&Print Selected";
            this.tsmiPrintSelected.Click += new System.EventHandler(this.tsmiPrintSelected_Click);
            // 
            // tsmiPrintAll
            // 
            this.tsmiPrintAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiPrintAll.Image")));
            this.tsmiPrintAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiPrintAll.Name = "tsmiPrintAll";
            this.tsmiPrintAll.Size = new System.Drawing.Size(191, 22);
            this.tsmiPrintAll.Text = "Print All";
            this.tsmiPrintAll.Click += new System.EventHandler(this.tsmiPrintAll_Click);
            // 
            // tsmiPrintPreview
            // 
            this.tsmiPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("tsmiPrintPreview.Image")));
            this.tsmiPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiPrintPreview.Name = "tsmiPrintPreview";
            this.tsmiPrintPreview.Size = new System.Drawing.Size(191, 22);
            this.tsmiPrintPreview.Text = "Print Pre&view";
            this.tsmiPrintPreview.Click += new System.EventHandler(this.tsmiPrintPreview_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(188, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(191, 22);
            this.tsmiExit.Text = "E&xit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopy});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCopy.Image")));
            this.tsmiCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiCopy.Size = new System.Drawing.Size(141, 22);
            this.tsmiCopy.Text = "&Copy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConfig,
            this.toolStripSeparator2,
            this.tsmiSendSelected,
            this.tsmiSendAll,
            this.toolStripMenuItem2,
            this.tsmiExport});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // tsmiConfig
            // 
            this.tsmiConfig.Image = global::BitDiffer.Client.Properties.Resources.PropertiesHS;
            this.tsmiConfig.Name = "tsmiConfig";
            this.tsmiConfig.Size = new System.Drawing.Size(247, 22);
            this.tsmiConfig.Text = "Comparison Set Configuration...";
            this.tsmiConfig.Click += new System.EventHandler(this.tsmiConfig_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(244, 6);
            // 
            // tsmiSendSelected
            // 
            this.tsmiSendSelected.Image = global::BitDiffer.Client.Properties.Resources.eps_closedHS;
            this.tsmiSendSelected.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsmiSendSelected.Name = "tsmiSendSelected";
            this.tsmiSendSelected.Size = new System.Drawing.Size(247, 22);
            this.tsmiSendSelected.Text = "Send Selected Item To...";
            this.tsmiSendSelected.Click += new System.EventHandler(this.tsmiSendSelected_Click);
            // 
            // tsmiSendAll
            // 
            this.tsmiSendAll.Image = global::BitDiffer.Client.Properties.Resources.eps_closedHS;
            this.tsmiSendAll.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsmiSendAll.Name = "tsmiSendAll";
            this.tsmiSendAll.Size = new System.Drawing.Size(247, 22);
            this.tsmiSendAll.Text = "Send All Items To...";
            this.tsmiSendAll.Click += new System.EventHandler(this.tsmiSendAll_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(244, 6);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Image = global::BitDiffer.Client.Properties.Resources.Webcontrol_ImportCatalogPart;
            this.tsmiExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(247, 22);
            this.tsmiExport.Text = "Export...";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelpSearch,
            this.toolStripSeparator6,
            this.tsmiAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // tsmiHelpSearch
            // 
            this.tsmiHelpSearch.Name = "tsmiHelpSearch";
            this.tsmiHelpSearch.Size = new System.Drawing.Size(152, 22);
            this.tsmiHelpSearch.Text = "Help";
            this.tsmiHelpSearch.Click += new System.EventHandler(this.tsmiHelpSearch_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(152, 22);
            this.tsmiAbout.Text = "&About...";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.tsbPrint,
            this.toolStripSeparator7,
            this.tsbRefresh,
            this.tsbConfigure,
            this.toolStripSeparator1,
            this.tsbExport,
            this.tsbSendTo,
            this.toolStripSeparator4,
            this.tsbView,
            this.toolStripSeparator8,
            this.tslTextFilter,
            this.tstbTextFilter,
            this.tsbHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1276, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "&New";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "&Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrintSelected,
            this.tsbPrintAll});
            this.tsbPrint.Image = global::BitDiffer.Client.Properties.Resources.PrintHS;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(29, 22);
            this.tsbPrint.Text = "toolStripDropDownButton1";
            // 
            // tsbPrintSelected
            // 
            this.tsbPrintSelected.Name = "tsbPrintSelected";
            this.tsbPrintSelected.Size = new System.Drawing.Size(181, 22);
            this.tsbPrintSelected.Text = "Print Selected Item";
            this.tsbPrintSelected.Click += new System.EventHandler(this.tsbPrintSelected_Click);
            // 
            // tsbPrintAll
            // 
            this.tsbPrintAll.Name = "tsbPrintAll";
            this.tsbPrintAll.Size = new System.Drawing.Size(181, 22);
            this.tsbPrintAll.Text = "Print All Items";
            this.tsbPrintAll.Click += new System.EventHandler(this.tsbPrintAll_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::BitDiffer.Client.Properties.Resources.Refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbConfigure
            // 
            this.tsbConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbConfigure.Image = global::BitDiffer.Client.Properties.Resources.PropertiesHS;
            this.tsbConfigure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConfigure.Name = "tsbConfigure";
            this.tsbConfigure.Size = new System.Drawing.Size(23, 22);
            this.tsbConfigure.Text = "Configure";
            this.tsbConfigure.Click += new System.EventHandler(this.tsbConfigure_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExport
            // 
            this.tsbExport.Image = global::BitDiffer.Client.Properties.Resources.Webcontrol_ImportCatalogPart;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(63, 22);
            this.tsbExport.Text = "Export";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbSendTo
            // 
            this.tsbSendTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSendSelected,
            this.tsbSendAll});
            this.tsbSendTo.Image = global::BitDiffer.Client.Properties.Resources.eps_closedHS;
            this.tsbSendTo.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbSendTo.Name = "tsbSendTo";
            this.tsbSendTo.Size = new System.Drawing.Size(83, 22);
            this.tsbSendTo.Text = "Send To";
            // 
            // tsbSendSelected
            // 
            this.tsbSendSelected.Name = "tsbSendSelected";
            this.tsbSendSelected.Size = new System.Drawing.Size(184, 22);
            this.tsbSendSelected.Text = "Send Selected Item";
            this.tsbSendSelected.Click += new System.EventHandler(this.tsbSendSelected_Click);
            // 
            // tsbSendAll
            // 
            this.tsbSendAll.Name = "tsbSendAll";
            this.tsbSendAll.Size = new System.Drawing.Size(184, 22);
            this.tsbSendAll.Text = "Send All Items";
            this.tsbSendAll.Click += new System.EventHandler(this.tsbSendAll_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbView
            // 
            this.tsbView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowChangesOnly,
            this.showTypesToolStripMenuItem,
            this.tsmiShowImplChanges,
            this.tsmiIgnoreAttrChanges});
            this.tsbView.Image = global::BitDiffer.Client.Properties.Resources.LegendHS;
            this.tsbView.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbView.Name = "tsbView";
            this.tsbView.Size = new System.Drawing.Size(63, 22);
            this.tsbView.Text = "View";
            // 
            // tsmiShowChangesOnly
            // 
            this.tsmiShowChangesOnly.Name = "tsmiShowChangesOnly";
            this.tsmiShowChangesOnly.Size = new System.Drawing.Size(263, 22);
            this.tsmiShowChangesOnly.Text = "Show only changed items";
            this.tsmiShowChangesOnly.Click += new System.EventHandler(this.tsmiShowChangesOnly_Click);
            // 
            // showTypesToolStripMenuItem
            // 
            this.showTypesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPublic,
            this.tsmiProtected,
            this.tsmiInternal,
            this.tsmiPrivate});
            this.showTypesToolStripMenuItem.Name = "showTypesToolStripMenuItem";
            this.showTypesToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.showTypesToolStripMenuItem.Text = "Show types";
            // 
            // tsmiPublic
            // 
            this.tsmiPublic.Name = "tsmiPublic";
            this.tsmiPublic.Size = new System.Drawing.Size(129, 22);
            this.tsmiPublic.Text = "Public";
            this.tsmiPublic.Click += new System.EventHandler(this.tsmiPublic_Click);
            // 
            // tsmiProtected
            // 
            this.tsmiProtected.Name = "tsmiProtected";
            this.tsmiProtected.Size = new System.Drawing.Size(129, 22);
            this.tsmiProtected.Text = "Protected";
            this.tsmiProtected.Click += new System.EventHandler(this.tsmiProtected_Click);
            // 
            // tsmiInternal
            // 
            this.tsmiInternal.Name = "tsmiInternal";
            this.tsmiInternal.Size = new System.Drawing.Size(129, 22);
            this.tsmiInternal.Text = "Internal";
            this.tsmiInternal.Click += new System.EventHandler(this.tsmiInternal_Click);
            // 
            // tsmiPrivate
            // 
            this.tsmiPrivate.Name = "tsmiPrivate";
            this.tsmiPrivate.Size = new System.Drawing.Size(129, 22);
            this.tsmiPrivate.Text = "Private";
            this.tsmiPrivate.Click += new System.EventHandler(this.tsmiPrivate_Click);
            // 
            // tsmiShowImplChanges
            // 
            this.tsmiShowImplChanges.Name = "tsmiShowImplChanges";
            this.tsmiShowImplChanges.Size = new System.Drawing.Size(263, 22);
            this.tsmiShowImplChanges.Text = "Show implementation changes";
            this.tsmiShowImplChanges.Click += new System.EventHandler(this.tsmiCompareMethods_Click);
            // 
            // tsmiIgnoreAttrChanges
            // 
            this.tsmiIgnoreAttrChanges.Name = "tsmiIgnoreAttrChanges";
            this.tsmiIgnoreAttrChanges.Size = new System.Drawing.Size(263, 22);
            this.tsmiIgnoreAttrChanges.Text = "Ignore assembly attribute changes";
            this.tsmiIgnoreAttrChanges.Click += new System.EventHandler(this.tsmiIgnoreAttrChanges_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tslTextFilter
            // 
            this.tslTextFilter.Image = global::BitDiffer.Client.Properties.Resources.Filter2;
            this.tslTextFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tslTextFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tslTextFilter.Name = "tslTextFilter";
            this.tslTextFilter.Size = new System.Drawing.Size(61, 22);
            this.tslTextFilter.Text = "Filter : ";
            // 
            // tstbTextFilter
            // 
            this.tstbTextFilter.Name = "tstbTextFilter";
            this.tstbTextFilter.Size = new System.Drawing.Size(100, 25);
            this.tstbTextFilter.TextChanged += new System.EventHandler(this.tstbTextFilter_TextChanged);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "He&lp";
            this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripIcon,
            this.statusStripStatusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 747);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1276, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusStripIcon
            // 
            this.statusStripIcon.Image = global::BitDiffer.Client.Properties.Resources.OK;
            this.statusStripIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statusStripIcon.Name = "statusStripIcon";
            this.statusStripIcon.Size = new System.Drawing.Size(16, 17);
            // 
            // statusStripStatusText
            // 
            this.statusStripStatusText.Name = "statusStripStatusText";
            this.statusStripStatusText.Size = new System.Drawing.Size(42, 17);
            this.statusStripStatusText.Text = " Ready";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // typeFilterTimer
            // 
            this.typeFilterTimer.Interval = 500;
            this.typeFilterTimer.Tick += new System.EventHandler(this.typeFilterTimer_Tick);
            // 
            // resultToolTip
            // 
            this.resultToolTip.AutoPopDelay = 2000;
            this.resultToolTip.InitialDelay = 500;
            this.resultToolTip.IsBalloon = true;
            this.resultToolTip.ReshowDelay = 100;
            // 
            // showTipTimer
            // 
            this.showTipTimer.Interval = 500;
            this.showTipTimer.Tick += new System.EventHandler(this.showTipTimer_Tick);
            // 
            // windowStatePersister1
            // 
            this.windowStatePersister1.ParentForm = this;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 769);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainFrm";
            this.Text = "BitDiffer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowStatePersister1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private BitDiffer.Client.Controls.DiffViewer diffViewer1;
		private BitDiffer.Client.Controls.TraceViewer traceViewer1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private BitDiffer.Client.Controls.AssemblyGroupNavigator navigator;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbRefresh;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusStripIcon;
		private System.Windows.Forms.ImageList imageList1;
		private BitDiffer.Client.Controls.ChangeInfoDetail changeInfoDetail1;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.ToolStripStatusLabel statusStripStatusText;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel tslTextFilter;
        private System.Windows.Forms.ToolStripDropDownButton tsbView;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowImplChanges;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiNew;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem tsmiSave;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrintAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrintPreview;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiConfig;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiHelpSearch;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
		private System.Windows.Forms.ToolStripButton tsbNew;
		private System.Windows.Forms.ToolStripButton tsbOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripButton tsbHelp;
		private System.Windows.Forms.ToolStripTextBox tstbTextFilter;
		private System.Windows.Forms.ToolStripMenuItem tsmiIgnoreAttrChanges;
		private System.Windows.Forms.Timer typeFilterTimer;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowChangesOnly;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
		private System.Windows.Forms.ToolStripMenuItem tsmiExit;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.HelpProvider helpProvider1;
		private System.Windows.Forms.ToolTip resultToolTip;
		private System.Windows.Forms.Timer showTipTimer;
		private System.Windows.Forms.ToolStripButton tsbConfigure;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private BitDiffer.Client.Controls.WindowStatePersister windowStatePersister1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiSendAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
		private System.Windows.Forms.ToolStripDropDownButton tsbPrint;
		private System.Windows.Forms.ToolStripMenuItem tsbPrintSelected;
		private System.Windows.Forms.ToolStripMenuItem tsbPrintAll;
		private System.Windows.Forms.ToolStripDropDownButton tsbSendTo;
		private System.Windows.Forms.ToolStripMenuItem tsbSendSelected;
		private System.Windows.Forms.ToolStripMenuItem tsbSendAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrintSelected;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiSendSelected;
		private System.Windows.Forms.ToolStripButton tsbExport;
		private System.Windows.Forms.WebBrowser auxWebBrowser;
        private System.Windows.Forms.ToolStripMenuItem showTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiPublic;
        private System.Windows.Forms.ToolStripMenuItem tsmiProtected;
        private System.Windows.Forms.ToolStripMenuItem tsmiInternal;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrivate;
	}
}