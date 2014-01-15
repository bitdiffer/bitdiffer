using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BitDiffer.Tests.Subject
{
	public class GenericClass<T>
	{
		public string Foo(string x)
		{
			return x;
		}

		public T FooWithT(T one)
		{
			return default(T);
		}

		public string GenericListParameter(List<T> ts)
		{
			return ts.ToString();
		}
	}

	public class GenericClassTwoArgs<T, U>
	{
		public string Foo(string x)
		{
			return x;
		}
	}

	public class NormalClass
	{
		public string NullableParameters(bool? x, int? y)
		{
			return "hi";
		}

		public string Foo<T>(T x)
		{
			return x.ToString();
		}

		public string FooRestricted<T>(T x) where T : INotifyPropertyChanged
		{
			return x.ToString();
		}
	}

	public class GenericClass1Restriction<T> where T : IComparable
	{
		public int Repeat(T a, T b)
		{
			return a.CompareTo(b);
		}
	}

	public class GenericMultipleRestrictions<T, U>
		where T : class, IComparable
		where U : class, IBindingList, new()
	{
		public int Repeat(T a, T b)
		{
			return a.CompareTo(b);
		}
	}

	public class GenericClassAddsArg<T>
	{
		public string Foo()
		{
			return "hi";
		}
	}

	public class GenericClassAddsRestriction<T>
	{
		public string Foo()
		{
			return "hi";
		}
	}

	public class GenericMethodAddsRestriction
	{
		public string Foo<T>(T x)
		{
			return x.ToString();
		}
	}
}
