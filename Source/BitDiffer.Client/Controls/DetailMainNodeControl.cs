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
	public class DetailItemNodeControl : BaseTextControl
	{
		public DetailItemNodeControl()
		{
			base.DrawText += new EventHandler<DrawEventArgs>(DetailItemNodeControl_DrawText);
		}

		void DetailItemNodeControl_DrawText(object sender, DrawEventArgs e)
		{
			if (e.TextColor != SystemColors.ControlText)
			{
				return;
			}

			TreeItemBase tib = ((TreeItemBase)e.Node.Tag);

			if (tib.GetItemAt(0).GetStrongestFilterStatus() < FilterStatus.DontCare)
			{
				e.TextColor = Color.LightGray;
			}
		}

		public override object GetValue(TreeNodeAdv node)
		{
			return ((TreeItemBase)node.Tag).Name;
		}

		public override string GetToolTip(TreeNodeAdv node)
		{
			return ((TreeItemBase)node.Tag).Title;
		}

		protected override Size CalculateEditorSize(EditorContext context)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override void DoApplyChanges(TreeNodeAdv node, Control editor)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override Control CreateEditor(TreeNodeAdv node)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}

