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
	class AssemblyComparisonModel : ITreeModel
	{
		private AssemblyComparison _ac;

		public AssemblyComparisonModel(AssemblyComparison ac)
		{
			_ac = ac;
		}

		public IEnumerable GetChildren(TreePath treePath)
		{
			if (treePath.IsEmpty())
			{
				foreach (AssemblyGroup grp in _ac.Groups)
				{
					yield return new AssemblyGroupTreeItem(grp);
				}
			}
		}

		public bool IsLeaf(TreePath treePath)
		{
			return (treePath.LastNode is AssemblyGroupTreeItem);
		}

#pragma warning disable 0067

		public event EventHandler<TreeModelEventArgs> NodesChanged;

		public event EventHandler<TreeModelEventArgs> NodesInserted;

		public event EventHandler<TreeModelEventArgs> NodesRemoved;

		public event EventHandler<TreePathEventArgs> StructureChanged;

#pragma warning restore 0067
	}
}
