using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using Aga.Controls.Tree;

using BitDiffer.Common.Model;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Misc;

namespace BitDiffer.Client.Models
{
	class AssemblyGroupModel : ITreeModel
	{
		private AssemblyGroup _grp;

		public AssemblyGroupModel(AssemblyGroup grp)
		{
			_grp = grp;
		}

		public IEnumerable GetChildren(TreePath treePath)
		{
			if (treePath.IsEmpty())
			{
				//yield return new AssemblyGroupTreeItem(_grp);

				if (_grp.Assemblies.Count > 0)
				{
					foreach (RootDetail child in _grp.Assemblies[0].FilterChildrenInAll<RootDetail>())
					{
						yield return new DetailTreeItem(child);
					}
				}
			}
			else if (treePath.LastNode is AssemblyGroupTreeItem)
			{
				AssemblyGroup grp = ((AssemblyGroupTreeItem)treePath.LastNode).Group;

				if (grp.Assemblies.Count > 0)
				{
					foreach (RootDetail child in grp.Assemblies[0].FilterChildrenInAll<RootDetail>())
					{
						yield return new DetailTreeItem(child);
					}
				}
			}
			else if (treePath.LastNode is DetailTreeItem)
			{
				ICanCompare item = ((DetailTreeItem)treePath.LastNode).Item;

				foreach (RootDetail child in item.FilterChildrenInAll<RootDetail>())
				{
					yield return new DetailTreeItem(child);
				}
			}
		}

		public bool IsLeaf(TreePath treePath)
		{
			return (treePath.LastNode is DetailTreeItem) && (((DetailTreeItem)treePath.LastNode).Item.Children.Count == 0);
		}

#pragma warning disable 0067

		public event EventHandler<TreeModelEventArgs> NodesChanged;

		public event EventHandler<TreeModelEventArgs> NodesInserted;

		public event EventHandler<TreeModelEventArgs> NodesRemoved;

		public event EventHandler<TreePathEventArgs> StructureChanged;

#pragma warning restore 0067
	}
}
