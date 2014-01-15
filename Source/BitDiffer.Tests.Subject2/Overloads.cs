using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Tests.Subject
{
	public class OverloadsExactMatch
	{
		public void OverloadedMethod()
		{
		}

		public void IrrelevantMethod1()
		{
		}

		public void OverloadedMethod(string p1)
		{
		}

		public void OverloadedMethod(int p1)
		{
		}

		public void IrrelevantMethod2(int p1)
		{
		}

		public void IrrelevantMethod2()
		{
		}

		public void OverloadedMethod(string p1, int p2)
		{
		}

		public void IrrelevantMethod3(string p1, int p2)
		{
		}
	}

	public class OverloadsReordered
	{
		public void OverloadedMethod(string p1, int p2)
		{
		}

		public void IrrelevantMethod1()
		{
		}

		public void OverloadedMethod(int p1)
		{
		}

		public void OverloadedMethod(string p1)
		{
		}

		public void IrrelevantMethod2(int p1)
		{
		}

		public void IrrelevantMethod2()
		{
		}

		public void OverloadedMethod()
		{
		}

		public void IrrelevantMethod3(string p1, int p2)
		{
		}
	}

	public class OverloadRemoved
	{
		public void OverloadedMethod()
		{
		}

		public void IrrelevantMethod1()
		{
		}

		public void OverloadedMethod(string p1)
		{
		}

		public void IrrelevantMethod2(int p1)
		{
		}

		public void IrrelevantMethod2()
		{
		}

		public void OverloadedMethod(string p1, int p2)
		{
		}

		public void IrrelevantMethod3(string p1, int p2)
		{
		}
	}

	public class OverloadsReorderedAndRemoved
	{
		public void OverloadedMethod(string p1, int p2)
		{
		}

		public void OverloadedMethod()
		{
		}

		public void IrrelevantMethod1()
		{
		}

		public void OverloadedMethod(int p1)
		{
		}

		public void IrrelevantMethod2(int p1)
		{
		}

		public void IrrelevantMethod2()
		{
		}

		public void IrrelevantMethod3(string p1, int p2)
		{
		}
	}

	public class OverloadAdded
	{
		public void OverloadedMethod()
		{
		}

		public void IrrelevantMethod1()
		{
		}

		public void OverloadedMethod(string p1)
		{
		}

		public void OverloadedMethod(int p1)
		{
		}

		public void OverloadedMethod(object p1)
		{
		}

		public void IrrelevantMethod2(int p1)
		{
		}

		public void IrrelevantMethod2()
		{
		}

		public void OverloadedMethod(string p1, int p2)
		{
		}

		public void IrrelevantMethod3(string p1, int p2)
		{
		}
	}

	public class OverloadNoMatches
	{
		public void OverloadMethod(int p1)
		{
		}

		public void OverloadMethod(int p1, string p2)
		{
		}
	}
}
