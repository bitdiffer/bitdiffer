using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BitDiffer.Tests.Subject
{
	public class Operators
	{
		public void NotAnOperator()
		{
		}

		public static bool operator ==(Operators a, Operators b)
		{
			return object.ReferenceEquals(a, b);
		}

		public static bool operator !=(Operators a, Operators b)
		{
			return !object.ReferenceEquals(a, b);
		}

		public static bool operator %(Operators a, Operators b)
		{
			return object.ReferenceEquals(a, b);
		}
	}
}
