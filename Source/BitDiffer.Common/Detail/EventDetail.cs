using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class EventDetail : MemberDetail
	{
		public EventDetail()
		{
		}

		public EventDetail(RootDetail parent, EventInfo ei)
			: base(parent, ei)
		{
			_name = ei.Name;
			_visibility = VisibilityUtil.GetVisibilityFor(ei.GetAddMethod(true));
			_category = "event";

			CodeStringBuilder csb = new CodeStringBuilder();

			AppendAttributesDeclaration(csb);

			csb.Mode = AppendMode.Html;
			csb.AppendVisibility(_visibility);
			csb.AppendText(" ");
			csb.Mode = AppendMode.Both;

			csb.AppendKeyword("event ");
			csb.AppendType(ei.EventHandlerType);
			csb.AppendText(" ");
			csb.AppendText(ei.Name);

			_declaration = csb.ToString();
			_declarationHtml = csb.ToHtmlString();
		}

		protected override string SerializeGetElementName()
		{
			return "Event";
		}
	}
}
