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
	public abstract class TreeItemBase
	{
		private Bitmap _icon;

		public TreeItemBase()
		{
		}

		public TreeItemBase(Bitmap icon)
		{
			_icon = icon;

			if (_icon != null)
			{
				_icon.MakeTransparent(_icon.GetPixel(0, 0));
			}
		}

		public Bitmap Icon
		{
			get { return _icon; }
		}

		public virtual string Title
		{
			get { return this.Name; }
		}

		public abstract string Name { get; }

		public abstract ICanCompare GetItemAt(int assemblyIndex);
	}
}
