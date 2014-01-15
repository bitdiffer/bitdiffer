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
	public class DetailChangeNodeControl : BaseTextControl
	{
		private int _assemblyIndex;

		public DetailChangeNodeControl(int assemblyIndex)
		{
			_assemblyIndex = assemblyIndex;

			base.DrawText += new EventHandler<DrawEventArgs>(DetailNodeControl_DrawText);
		}

		public override object GetValue(TreeNodeAdv node)
		{
			return GetItem(node).GetTextSummary();
		}

		public override string GetToolTip(TreeNodeAdv node)
		{
			return GetItem(node).GetTextChangeDescription();
		}

		private RootDetail GetItem(TreeNodeAdv node)
		{
			return (RootDetail)((TreeItemBase)node.Tag).GetItemAt(_assemblyIndex);
		}

		private ICanCompare GetPreviousItem(TreeNodeAdv node)
		{
			if (_assemblyIndex == 0)
			{
				return null;
			}

			return ((TreeItemBase)node.Tag).GetItemAt(_assemblyIndex-1);
		}

		private void DetailNodeControl_DrawText(object sender, DrawEventArgs e)
		{
			if (e.TextColor != SystemColors.ControlText)
			{
				return;
			}

			TreeItemBase tib = ((TreeItemBase)e.Node.Tag);
			ChangeType change = tib.GetItemAt(_assemblyIndex).Change;

			if (ChangeTypeUtil.HasBreaking(change))
			{
				e.TextColor = Color.Red;
			}
			else if (change == ChangeType.Added)
			{
				e.TextColor = Color.Green;
			}
			else if (tib.GetItemAt(0).GetStrongestFilterStatus() < FilterStatus.DontCare)
			{
				e.TextColor = Color.LightGray;
			}
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
