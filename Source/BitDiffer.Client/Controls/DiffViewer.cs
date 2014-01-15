using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Aga.Controls.Tree.NodeControls;
using Aga.Controls.Tree;

using BitDiffer.Common.Model;
using BitDiffer.Client.Models;
using BitDiffer.Core;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Interfaces;

namespace BitDiffer.Client.Controls
{
	public partial class DiffViewer : UserControl
	{
		public event EventHandler SelectedDetailItemChanged;

		public DiffViewer()
		{
			InitializeComponent();
		}

		public int SelectedColumnIndex
		{
			get { return treeViewAdv1.SelectedColumnIndex; }
		}

		public ICanCompare SelectedItem
		{
			get
			{
				if (treeViewAdv1.SelectedNode != null)
				{
					if (treeViewAdv1.SelectedNode.Tag is TreeItemBase)
					{
						TreeItemBase selectedItem = (TreeItemBase)treeViewAdv1.SelectedNode.Tag;
						return selectedItem.GetItemAt(treeViewAdv1.SelectedColumnIndex - 1);
					}
				}

				return null;
			}
		}

		public AssemblyGroup SelectedGroupItem
		{
			get
			{
				if (treeViewAdv1.SelectedNode != null)
				{
					if (treeViewAdv1.SelectedNode.Tag is AssemblyGroupTreeItem)
					{
						return ((AssemblyGroupTreeItem)treeViewAdv1.SelectedNode.Tag).Group;
					}
				}

				return null;
			}
		}

		public void LoadFrom(AssemblyGroup grp)
		{
			treeViewAdv1.BeginUpdate();

			try
			{
				while (treeViewAdv1.Columns.Count > 1)
				{
					treeViewAdv1.Columns.RemoveAt(1);
				}

				while (treeViewAdv1.NodeControls.Count > 2)
				{
					treeViewAdv1.NodeControls.RemoveAt(2);
				}

				treeViewAdv1.Model = new TreeModel();

				if ((grp != null) && (!grp.HasErrors))
				{
					int divisor = Math.Max(1, grp.Assemblies.Count);
					int colWidth = Math.Max(1, (treeViewAdv1.ClientRectangle.Width - treeViewAdv1.Columns[0].Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth - 5) / divisor);

					int col = 0;
					foreach (AssemblyDetail ad in grp.Assemblies)
					{
						TreeColumn tc = new TreeColumn();
						tc.Header = ad.Location;
						tc.TooltipText = tc.Header;
						tc.Width = colWidth;
						treeViewAdv1.Columns.Add(tc);

						DetailChangeNodeControl dnc = new DetailChangeNodeControl(col++);
						dnc.ParentColumn = tc;
						dnc.DisplayHiddenContentInToolTip = true;
						dnc.EditEnabled = false;
						dnc.Trimming = StringTrimming.EllipsisCharacter;

						treeViewAdv1.NodeControls.Add(dnc);
					}

					treeViewAdv1.Model = new AssemblyGroupModel(grp);
					//treeViewAdv1.Root.Children[0].Expand();
				}
			}
			finally
			{
				treeViewAdv1.EndUpdate();
			}
		}

		private void treeViewAdv1_SelectionChanged(object sender, EventArgs e)
		{
			if (SelectedDetailItemChanged != null)
			{
				SelectedDetailItemChanged(this, EventArgs.Empty);
			}
		}

		internal void ExpandAll()
		{
			treeViewAdv1.ExpandAll();
		}
	}
}
