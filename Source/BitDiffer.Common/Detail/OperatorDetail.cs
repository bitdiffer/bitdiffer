using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class OperatorDetail : MethodDetail
	{
		public OperatorDetail()
		{
		}

		public OperatorDetail(RootDetail parent, MethodBase mi)
			: base(parent, mi)
		{
			_category = "operator";
		}

		protected override string SerializeGetElementName()
		{
			return "Operator";
		}
	}
}
