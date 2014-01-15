using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class InterfaceDetail : EntityDetail
	{
		public InterfaceDetail()
		{
		}

		public InterfaceDetail(RootDetail parent, Type type)
			: base(parent, type, true)
		{
			_category = "interface";
		}

		protected override string SerializeGetElementName()
		{
			return "Interface";
		}
	}
}
