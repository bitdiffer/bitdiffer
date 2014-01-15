using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using BitDiffer.Common.Misc;
using BitDiffer.Client.Models;
using Aga.Controls.Tree.NodeControls;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using Aga.Controls.Tree;

namespace BitDiffer.Client.Controls
{
	public partial class AssemblyGroupNavigator : UserControl
	{
		public event EventHandler SelectedAssemblyGroupChanged;

		public AssemblyGroupNavigator()
		{
			InitializeComponent();
		}

		public void LoadFrom(AssemblyComparison ac, bool preserveSelection)
		{
			string selection = null;

			if (preserveSelection && treeViewAdv1.SelectedNode != null)
			{
				selection = ((AssemblyGroupTreeItem)treeViewAdv1.SelectedNode.Tag).Group.Name;
			}

			treeViewAdv1.Model = new AssemblyComparisonModel(ac);

			int selIndex = 0;

			if (selection != null)
			{
				for (int i=0; i<treeViewAdv1.Root.Children.Count; i++)
				{
					if (((AssemblyGroupTreeItem)treeViewAdv1.Root.Children[i].Tag).Name == selection)
					{
						selIndex = i;
						break;
					}
				}
			}

            if (selIndex < treeViewAdv1.Root.Children.Count)
            {
                treeViewAdv1.Root.Children[selIndex].IsSelected = true;
            }
		}

		public AssemblyGroup SelectedAssemblyGroup
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

		private void treeViewAdv1_SizeChanged(object sender, EventArgs e)
		{
			treeViewAdv1.Columns[0].Width = this.ClientRectangle.Width - 5;
		}

		private void treeViewAdv1_SelectionChanged(object sender, EventArgs e)
		{
			if (SelectedAssemblyGroupChanged != null)
			{
				SelectedAssemblyGroupChanged(this, EventArgs.Empty);
			}
		}
	}
}
