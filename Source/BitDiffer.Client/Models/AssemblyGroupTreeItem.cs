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
	public class AssemblyGroupTreeItem : TreeItemBase
	{
		private AssemblyGroup _grp;

		public AssemblyGroupTreeItem(AssemblyGroup grp)
			: base(grp.HasErrors ? Resources.NoAction : Resources.VSObject_Assembly)
		{
			_grp = grp;
		}

		public override string Name
		{
			get { return _grp.Name; }
		}

		public override string Title
		{
			get
			{
				return _grp.HasErrors ? "ERROR" : "OK";
			}
		}

		public AssemblyGroup Group
		{
			get { return _grp; }
			set { _grp = value; }
		}

		public override ICanCompare GetItemAt(int assemblyIndex)
		{
			if (assemblyIndex < 0)
			{
				return null;
			}

			return _grp.Assemblies[assemblyIndex];
		}
	}
}
