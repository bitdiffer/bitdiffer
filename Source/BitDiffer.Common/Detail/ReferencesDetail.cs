using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class ReferencesDetail : ParentDetail
	{
		public ReferencesDetail(RootDetail parent)
			: base(parent, "References")
		{
		}

		protected override void GetTextDescriptionBriefMembers(StringBuilder sb)
		{
			AppendClause(sb, "Assembly references changed");
		}

		protected override void GetHtmlChangeDescriptionBriefMembers(StringBuilder sb)
		{
			AppendClauseHtml(sb, false, "Assembly references changed");
		}

		protected override int RelativeSortOrder
		{
			get { return -6; }
		}

		protected override string SerializeGetElementName()
		{
			return "References";
		}
	}
}
