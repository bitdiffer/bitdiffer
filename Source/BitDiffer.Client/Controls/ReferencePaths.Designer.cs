namespace BitDiffer.Client.Controls
{
    partial class ReferencePaths
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReferencePaths));
            this.gridFolders = new BitDiffer.Client.Controls.FileSystemDataGridView();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridFolders)).BeginInit();
            this.SuspendLayout();
            // 
            // gridFolders
            // 
            this.gridFolders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFolders.Items = ((System.Collections.Generic.List<string>)(resources.GetObject("gridFolders.Items")));
            this.gridFolders.ItemType = BitDiffer.Client.Controls.FileSystemItemType.Folder;
            this.gridFolders.Location = new System.Drawing.Point(15, 77);
            this.gridFolders.Name = "gridFolders";
            this.gridFolders.Size = new System.Drawing.Size(659, 375);
            this.gridFolders.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(643, 42);
            this.label9.TabIndex = 20;
            this.label9.Text = "Select reference directories.\r\n\r\nThese directories will be searched for dependenc" +
    "ies when loading the assemblies you have selected for comparison.\r\n";
            // 
            // ReferencePaths
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.gridFolders);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReferencePaths";
            this.Size = new System.Drawing.Size(692, 468);
            ((System.ComponentModel.ISupportInitialize)(this.gridFolders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private FileSystemDataGridView gridFolders;
        private System.Windows.Forms.Label label9;



    }
}
