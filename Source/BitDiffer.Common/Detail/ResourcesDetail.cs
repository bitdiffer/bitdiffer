using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class ResourcesDetail : ParentDetail
	{
		public ResourcesDetail(RootDetail parent)
			: base(parent, "Resources")
		{
		}

		protected override void GetTextDescriptionBriefMembers(StringBuilder sb)
		{
			AppendClause(sb, "Assembly resources changed");
		}

		protected override void GetHtmlChangeDescriptionBriefMembers(StringBuilder sb)
		{
			AppendClauseHtml(sb, false, "Assembly resources changed");
		}

		protected override int RelativeSortOrder
		{
			get { return -6; }
		}

		protected override string SerializeGetElementName()
		{
			return "Resources";
		}
	}
}
