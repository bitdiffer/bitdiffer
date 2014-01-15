namespace BitDiffer.Client.Controls
{
	partial class AssemblyGroupNavigator
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
			this.nodeTextBox1 = new BitDiffer.Client.Controls.AssemblyGroupNodeControl();
			this.SuspendLayout();
			// 
			// treeViewAdv1
			// 
			this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
			this.treeViewAdv1.Columns.Add(this.treeColumn1);
			this.treeViewAdv1.DefaultToolTipProvider = null;
			this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
			this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
			this.treeViewAdv1.Location = new System.Drawing.Point(0, 0);
			this.treeViewAdv1.Model = null;
			this.treeViewAdv1.Name = "treeViewAdv1";
			this.treeViewAdv1.NodeControls.Add(this.nodeIcon1);
			this.treeViewAdv1.NodeControls.Add(this.nodeTextBox1);
			this.treeViewAdv1.SelectedColumnIndex = -1;
			this.treeViewAdv1.SelectedNode = null;
			this.treeViewAdv1.ShowLines = false;
			this.treeViewAdv1.ShowNodeToolTips = true;
			this.treeViewAdv1.ShowPlusMinus = false;
			this.treeViewAdv1.Size = new System.Drawing.Size(356, 359);
			this.treeViewAdv1.TabIndex = 0;
			this.treeViewAdv1.Text = "treeViewAdv1";
			this.treeViewAdv1.UseColumns = true;
			this.treeViewAdv1.SelectionChanged += new System.EventHandler(this.treeViewAdv1_SelectionChanged);
			this.treeViewAdv1.SizeChanged += new System.EventHandler(this.treeViewAdv1_SizeChanged);
			// 
			// treeColumn1
			// 
			this.treeColumn1.Header = "Assemblies";
			this.treeColumn1.SortOrder = System.Windows.Forms.SortOrder.None;
			this.treeColumn1.TooltipText = null;
			this.treeColumn1.Width = 100;
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
			// AssemblyGroupNavigator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeViewAdv1);
			this.Name = "AssemblyGroupNavigator";
			this.Size = new System.Drawing.Size(356, 359);
			this.ResumeLayout(false);

		}

		#endregion

		private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
		private ItemIconNodeControl nodeIcon1;
		private AssemblyGroupNodeControl nodeTextBox1;
		private Aga.Controls.Tree.TreeColumn treeColumn1;
	}
}
