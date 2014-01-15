using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class TypeDetail : MemberDetail
	{
		public TypeDetail()
		{
		}

		public TypeDetail(RootDetail parent, Type type)
			: base(parent, type)
		{
			CodeStringBuilder csb = new CodeStringBuilder(AppendMode.Text);
			csb.AppendType(type, false);
			_name = csb.ToString();
		}

		protected override string SerializeGetElementName()
		{
			return "Type";
		}
	}
}
