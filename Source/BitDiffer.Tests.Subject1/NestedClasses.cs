using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BitDiffer.Tests.Subject
{
	public class ParentClass
	{
		private interface INestedPrivateInterface
		{
		}

		public interface INestedPublicInterface
		{
		}

		private enum NestedPrivateEnum
		{
			A, B, C
		}

		private class NestedPrivateClass
		{
		}

		private class GenericClass<T>
		{
		}

		protected class NestedProtectedClass
		{
			private void NestedPrivateMethod()
			{
			}

			private class NestedGrandchildClass
			{
				private enum NestedGranchildEnum
				{
					GA, GB, GC
				}
			}
		}
	}

	public class ParentClassPrivateNestedClassChanges
	{
		private class NestedPrivateClass
		{
		}
	}

	public class ParentClassPrivateNestedEnumChanges
	{
		private enum NestedEnum { A, B, C };
	}

	public class ParentClassPrivateNestedInterfaceChanges
	{
		private interface INestedInterface
		{
			void Hello();
		}
	}

	public class ParentClassPublicGrandchildChanges
	{
		public class NestedPublicClass
		{
			public class NestedPrivateGrandchildClass
			{
				public string MethodGetsRemoved()
				{
					return "hi";
				}
			}
		}
	}

	public class ParentClassNoChange
	{
		public class NestedClassNoChange
		{
			public class NestedGranchildNoChange
			{
				public string MethodNoChange()
				{
					return "hi";
				}
			}
		}
	}
}
