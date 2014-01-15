using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Tests.Reference
{
	public class ReferencedClass1
	{
		public ReferencedClass1()
		{

		}

		public static ReferencedClass1 UseMe()
		{
			return new ReferencedClass1();
		}
	}
}
