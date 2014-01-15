using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Aga.Controls.Tree.NodeControls;
using Aga.Controls.Tree;

using BitDiffer.Common.Model;
using BitDiffer.Client.Models;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Interfaces;

namespace BitDiffer.Client.Controls
{
	public class ItemIconNodeControl : NodeIcon
	{
		public ItemIconNodeControl()
		{
		}

		protected override Image GetIcon(TreeNodeAdv node)
		{
			return ((TreeItemBase)node.Tag).Icon;
		}
	}
}
