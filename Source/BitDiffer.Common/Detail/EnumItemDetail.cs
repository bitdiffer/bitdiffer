using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;
using BitDiffer.Common.Utility;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class EnumItemDetail : MemberDetail
	{
		private long _value;

		public EnumItemDetail()
		{
		}

		public EnumItemDetail(RootDetail parent, string name, long value, Visibility visibility)
		{
			_name = name;
			_parent = parent;
			_value = value;
			_visibility = visibility;
			_category = "enum value";

			CodeStringBuilder csb = new CodeStringBuilder();

			AppendAttributesDeclaration(csb);

			csb.AppendText(name);
			csb.AppendText(" = ");
			csb.AppendText(value.ToString());

			_declaration = csb.ToString();
			_declarationHtml = csb.ToHtmlString();
		}

		protected override ChangeType CompareInstance(ICanCompare previous, bool suppressBreakingChanges)
		{
			ChangeType change = ChangeType.None;

			EnumItemDetail other = (EnumItemDetail)previous;

			if (_value != other._value)
			{
				if ((!suppressBreakingChanges) && (_visibility == Visibility.Public && other._visibility == Visibility.Public))
				{
					change |= ChangeType.ValueChangedBreaking;
				}
				else
				{
					change |= ChangeType.ValueChangedNonBreaking;
				}
			}

			return change;
		}

		protected override string SerializeGetElementName()
		{
			return "Item";
		}
	}
}
