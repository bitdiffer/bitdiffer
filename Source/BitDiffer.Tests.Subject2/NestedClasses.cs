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
		private class NestedPrivateClass : ISupportInitialize
		{
			public void  BeginInit()
			{
 				throw new Exception("The method or operation is not implemented.");
			}

			public void  EndInit()
			{
 				throw new Exception("The method or operation is not implemented.");
			}
		}
	}

	public class ParentClassPrivateNestedEnumChanges
	{
		private enum NestedEnum { A, B, C, D };
	}

	public class ParentClassPrivateNestedInterfaceChanges
	{
		private interface INestedInterface
		{
			void HelloChanged();
		}
	}

	public class ParentClassPublicGrandchildChanges
	{
		public class NestedPublicClass
		{
			public class NestedPrivateGrandchildClass
			{
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
