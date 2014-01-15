namespace BitDiffer.Client.Forms
{
	partial class ProjectSetup
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFiles = new System.Windows.Forms.ToolStripButton();
            this.tsbOptions = new System.Windows.Forms.ToolStripButton();
            this.tsbViewFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbReferences = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.compareFiles = new BitDiffer.Client.Controls.SelectAssemblies();
            this.compareOptions = new BitDiffer.Client.Controls.Configuration();
            this.viewFilter = new BitDiffer.Client.Controls.CompareViewFilter();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.referencePaths = new BitDiffer.Client.Controls.ReferencePaths();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFiles,
            this.tsbOptions,
            this.tsbViewFilter,
            this.tsbReferences});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(116, 480);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbFiles
            // 
            this.tsbFiles.Image = global::BitDiffer.Client.Properties.Resources.proj_files;
            this.tsbFiles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFiles.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbFiles.Name = "tsbFiles";
            this.tsbFiles.Size = new System.Drawing.Size(113, 50);
            this.tsbFiles.Text = "Assembly Selection";
            this.tsbFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFiles.Click += new System.EventHandler(this.tsbFiles_Click);
            // 
            // tsbOptions
            // 
            this.tsbOptions.Image = global::BitDiffer.Client.Properties.Resources.proj_config;
            this.tsbOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(113, 50);
            this.tsbOptions.Text = "Configuration";
            this.tsbOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbOptions.Click += new System.EventHandler(this.tsbOptions_Click);
            // 
            // tsbViewFilter
            // 
            this.tsbViewFilter.Image = global::BitDiffer.Client.Properties.Resources.proj_view_filter;
            this.tsbViewFilter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbViewFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbViewFilter.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbViewFilter.Name = "tsbViewFilter";
            this.tsbViewFilter.Size = new System.Drawing.Size(113, 50);
            this.tsbViewFilter.Text = "View Filter";
            this.tsbViewFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbViewFilter.Click += new System.EventHandler(this.tsbDirectories_Click);
            // 
            // tsbReferences
            // 
            this.tsbReferences.Image = global::BitDiffer.Client.Properties.Resources.proj_references;
            this.tsbReferences.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbReferences.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbReferences.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbReferences.Name = "tsbReferences";
            this.tsbReferences.Size = new System.Drawing.Size(113, 50);
            this.tsbReferences.Text = "Reference Paths";
            this.tsbReferences.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbReferences.Click += new System.EventHandler(this.tsbReferences_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Size = new System.Drawing.Size(809, 546);
            this.splitContainer1.SplitterDistance = 484;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.compareFiles);
            this.splitContainer2.Panel2.Controls.Add(this.compareOptions);
            this.splitContainer2.Panel2.Controls.Add(this.viewFilter);
            this.splitContainer2.Panel2.Controls.Add(this.referencePaths);
            this.splitContainer2.Size = new System.Drawing.Size(809, 484);
            this.splitContainer2.SplitterDistance = 117;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(117, 484);
            this.panel1.TabIndex = 3;
            // 
            // compareFiles
            // 
            this.compareFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.compareFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareFiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compareFiles.Location = new System.Drawing.Point(0, 0);
            this.compareFiles.Name = "compareFiles";
            this.compareFiles.Size = new System.Drawing.Size(687, 484);
            this.compareFiles.TabIndex = 0;
            // 
            // compareOptions
            // 
            this.compareOptions.AutoScroll = true;
            this.compareOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.compareOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareOptions.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compareOptions.Location = new System.Drawing.Point(0, 0);
            this.compareOptions.Name = "compareOptions";
            this.compareOptions.Size = new System.Drawing.Size(687, 484);
            this.compareOptions.TabIndex = 2;
            // 
            // viewFilter
            // 
            this.viewFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewFilter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewFilter.Location = new System.Drawing.Point(0, 0);
            this.viewFilter.Name = "viewFilter";
            this.viewFilter.Size = new System.Drawing.Size(687, 484);
            this.viewFilter.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(617, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(710, 17);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(87, 25);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(523, 17);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // referencePaths
            // 
            this.referencePaths.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.referencePaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.referencePaths.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.referencePaths.Location = new System.Drawing.Point(0, 0);
            this.referencePaths.Name = "referencePaths";
            this.referencePaths.Size = new System.Drawing.Size(687, 484);
            this.referencePaths.TabIndex = 3;
            // 
            // ProjectSetup
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(809, 546);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ProjectSetup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Comparison Set: Setup and Configuration";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbFiles;
		private System.Windows.Forms.ToolStripButton tsbViewFilter;
		private System.Windows.Forms.ToolStripButton tsbOptions;
		private BitDiffer.Client.Controls.SelectAssemblies compareFiles;
		private BitDiffer.Client.Controls.Configuration compareOptions;
		private BitDiffer.Client.Controls.CompareViewFilter viewFilter;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolStripButton tsbReferences;
        private Controls.ReferencePaths referencePaths;
	}
}