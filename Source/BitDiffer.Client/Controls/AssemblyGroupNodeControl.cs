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
	public class AssemblyGroupNodeControl : BaseTextControl
	{
		public AssemblyGroupNodeControl()
		{
			base.DrawText += new EventHandler<DrawEventArgs>(AssemblyGroupNodeControl_DrawText);
		}

		public override object GetValue(TreeNodeAdv node)
		{
			return ((TreeItemBase)node.Tag).Name;
		}

		public override string GetToolTip(TreeNodeAdv node)
		{
			AssemblyGroup grp = ((AssemblyGroupTreeItem)node.Tag).Group;

			string tip = "Assembly " + grp.Name;

			if (grp.HasErrors)
			{
				tip += " failed to load. Check log messages for detailed error information.";
			}
			else if (ChangeTypeUtil.HasBreaking(grp.Change))
			{
				tip += " has breaking changes.";
			}
			else if (ChangeTypeUtil.HasNonBreaking(grp.Change))
			{
				tip += " has non-breaking changes.";
			}
			else
			{
				tip += " has no changes.";
			}

			return tip;
		}

		private void AssemblyGroupNodeControl_DrawText(object sender, DrawEventArgs e)
		{
			if (e.TextColor != SystemColors.ControlText)
			{
				return;
			}

			AssemblyGroup grp = ((AssemblyGroupTreeItem)e.Node.Tag).Group;

			e.TextColor = Color.Gray;

			if (grp.HasErrors)
			{
				e.TextColor = Color.LightGray;
			}
			else if (ChangeTypeUtil.HasBreaking(grp.Change))
			{
				e.TextColor = Color.Red;
			}
			else if (ChangeTypeUtil.HasNonBreaking(grp.Change))
			{
				e.TextColor = Color.Black;
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
