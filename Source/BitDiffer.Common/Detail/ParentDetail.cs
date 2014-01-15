using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class ParentDetail : RootDetail
	{
		public ParentDetail()
		{
		}

		public ParentDetail(RootDetail parent, string name)
			: base(parent, name)
		{
		}

		protected override bool SerializeShouldWriteName()
		{
			return false;
		}

		protected override bool FullNameRoot
		{
			get { return true; }
		}
	}
}

