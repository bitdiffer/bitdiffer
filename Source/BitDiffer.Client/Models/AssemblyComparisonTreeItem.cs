using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

using BitDiffer.Client.Properties;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Utility;

namespace BitDiffer.Client.Models
{
	public class AssemblyComparisonTreeItem : TreeItemBase
	{
		private AssemblyComparison _ac;

		public AssemblyComparisonTreeItem(AssemblyComparison ac)
			: base(Resources.VSObject_Assembly)
		{
			_ac = ac;
		}

		public override string Name
		{
			get { return "Groups"; }
		}

		public AssemblyComparison AssemblyComparison
		{
			get { return _ac; }
			set { _ac = value; }
		}

		public override ICanCompare GetItemAt(int assemblyIndex)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
