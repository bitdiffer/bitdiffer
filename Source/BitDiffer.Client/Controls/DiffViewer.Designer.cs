namespace BitDiffer.Client.Controls
{
	partial class DiffViewer
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
			this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
			this.treeColumn1 = new Aga.Controls.Tree.TreeColumn();
			this.nodeIcon1 = new BitDiffer.Client.Controls.ItemIconNodeControl();
			this.nodeTextBox1 = new BitDiffer.Client.Controls.DetailItemNodeControl();
			this.SuspendLayout();
			// 
			// treeViewAdv1
			// 
			this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
			this.treeViewAdv1.Columns.Add(this.treeColumn1);
			this.treeViewAdv1.ColumnSelectionMode = Aga.Controls.Tree.ColumnSelectionMode.Single;
			this.treeViewAdv1.DefaultToolTipProvider = null;
			this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
			this.treeViewAdv1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeViewAdv1.FullRowSelect = true;
			this.treeViewAdv1.GridLineStyle = ((Aga.Controls.Tree.GridLineStyle)((Aga.Controls.Tree.GridLineStyle.Horizontal | Aga.Controls.Tree.GridLineStyle.Vertical)));
			this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
			this.treeViewAdv1.Location = new System.Drawing.Point(0, 0);
			this.treeViewAdv1.Model = null;
			this.treeViewAdv1.Name = "treeViewAdv1";
			this.treeViewAdv1.NodeControls.Add(this.nodeIcon1);
			this.treeViewAdv1.NodeControls.Add(this.nodeTextBox1);
			this.treeViewAdv1.RowHeight = 20;
			this.treeViewAdv1.SelectedColumnIndex = -1;
			this.treeViewAdv1.SelectedNode = null;
			this.treeViewAdv1.ShowNodeToolTips = true;
			this.treeViewAdv1.Size = new System.Drawing.Size(1156, 433);
			this.treeViewAdv1.TabIndex = 1;
			this.treeViewAdv1.Text = "treeViewAdv1";
			this.treeViewAdv1.UseColumns = true;
			this.treeViewAdv1.SelectionChanged += new System.EventHandler(this.treeViewAdv1_SelectionChanged);
			// 
			// treeColumn1
			// 
			this.treeColumn1.Header = "Item";
			this.treeColumn1.SortOrder = System.Windows.Forms.SortOrder.None;
			this.treeColumn1.TooltipText = null;
			this.treeColumn1.Width = 350;
			// 
			// nodeIcon1
			// 
			this.nodeIcon1.LeftMargin = 1;
			this.nodeIcon1.ParentColumn = this.treeColumn1;
			// 
			// nodeTextBox1
			// 
			this.nodeTextBox1.EditEnabled = false;
			this.nodeTextBox1.IncrementalSearchEnabled = true;
			this.nodeTextBox1.LeftMargin = 3;
			this.nodeTextBox1.ParentColumn = this.treeColumn1;
			this.nodeTextBox1.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
			// 
			// DiffViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeViewAdv1);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "DiffViewer";
			this.Size = new System.Drawing.Size(1156, 433);
			this.ResumeLayout(false);

		}

		#endregion

		private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
		private Aga.Controls.Tree.TreeColumn treeColumn1;
		private ItemIconNodeControl nodeIcon1;
		private DetailItemNodeControl nodeTextBox1;
	}
}
