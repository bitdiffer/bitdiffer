namespace BitDiffer.Client.Controls
{
	partial class SelectAssemblies
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAssemblies));
            this.label9 = new System.Windows.Forms.Label();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.gridFiles = new BitDiffer.Client.Controls.FileSystemDataGridView();
            this.gridFolders = new BitDiffer.Client.Controls.FileSystemDataGridView();
            this.cbRecurse = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFolders)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(211, 14);
            this.label9.TabIndex = 13;
            this.label9.Text = "Select files or directories to compare.";
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Compare specific selected assemblies",
            "Compare all assemblies in selected directories"});
            this.cbMode.Location = new System.Drawing.Point(227, 38);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(301, 22);
            this.cbMode.TabIndex = 15;
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.cbMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "Assembly Comparison Mode:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDescription
            // 
            this.lbDescription.Location = new System.Drawing.Point(12, 74);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(516, 60);
            this.lbDescription.TabIndex = 17;
            // 
            // gridFiles
            // 
            this.gridFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFiles.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("gridFiles.Items")));
            this.gridFiles.ItemType = BitDiffer.Client.Controls.FileSystemItemType.File;
            this.gridFiles.Location = new System.Drawing.Point(15, 153);
            this.gridFiles.Name = "gridFiles";
            this.gridFiles.Size = new System.Drawing.Size(659, 299);
            this.gridFiles.TabIndex = 18;
            // 
            // gridFolders
            // 
            this.gridFolders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFolders.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("gridFolders.Items")));
            this.gridFolders.ItemType = BitDiffer.Client.Controls.FileSystemItemType.Folder;
            this.gridFolders.Location = new System.Drawing.Point(15, 153);
            this.gridFolders.Name = "gridFolders";
            this.gridFolders.Size = new System.Drawing.Size(659, 299);
            this.gridFolders.TabIndex = 19;
            // 
            // cbRecurse
            // 
            this.cbRecurse.AutoSize = true;
            this.cbRecurse.Location = new System.Drawing.Point(549, 40);
            this.cbRecurse.Name = "cbRecurse";
            this.cbRecurse.Size = new System.Drawing.Size(69, 18);
            this.cbRecurse.TabIndex = 20;
            this.cbRecurse.Text = "Recurse";
            this.cbRecurse.UseVisualStyleBackColor = true;
            // 
            // SelectAssemblies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbRecurse);
            this.Controls.Add(this.gridFiles);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.cbMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.gridFolders);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SelectAssemblies";
            this.Size = new System.Drawing.Size(692, 468);
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFolders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cbMode;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbDescription;
		private FileSystemDataGridView gridFiles;
		private FileSystemDataGridView gridFolders;
        private System.Windows.Forms.CheckBox cbRecurse;

	}
}
