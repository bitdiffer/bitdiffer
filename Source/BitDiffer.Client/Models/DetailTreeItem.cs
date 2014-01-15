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
	public class DetailTreeItem : TreeItemBase
	{
		private ICanCompare _item;

		public DetailTreeItem(ICanCompare item)
			: base(IconMap.GetIconForItem(item))
		{
			_item = item;
		}

		public override string Name
		{
			get { return _item.Name; }
		}

		public override string Title
		{
			get { return ((RootDetail)_item).GetTextDeclaration(); }
		}

		public ICanCompare Item
		{
			get { return _item; }
			set { _item = value; }
		}

		public override ICanCompare GetItemAt(int assemblyIndex)
		{
			return (ICanCompare)_item.NavigateForwardTo(assemblyIndex);
		}
	}
}
