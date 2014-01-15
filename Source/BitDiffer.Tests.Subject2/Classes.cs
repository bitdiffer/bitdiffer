using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BitDiffer.Tests.Subject
{
    public class MyClass1
    {

    }

    public class MyClass2
    {

    }

	public class BasicClass
	{
		private int _privateField = 0;
		public string _publicField;
        protected int _protectedField = 0;

		[Description("This field has an attribute")]
		public string _fieldWithAttribute;

		public const string _constString = "This is const";
		public const int _constInt = 3;
		public static string _staticField = "This is static";
		public readonly string _readOnlyField = "This is read-only";

		public event EventHandler MyEventToFire;
		internal event EventHandler MyInternalEvent;

		public event CancelEventHandler PublicEventDeclChange;
		internal event CancelEventHandler InternalEventDeclChange;

		private const int _privateFieldDeclChange = 2;
		public string _publicFieldDeclChange;

		private string _fieldLosesAttribute;

        private int _fieldChangesType;
        public MyClass2 _anotherFieldChangesType;

		[Description("This event has an attribute")]
		public event EventHandler MyEventWithAttribute;

		public void SimplePublicMethod()
		{
			if (MyEventToFire != null)
			{
				MyEventToFire(this, EventArgs.Empty);
			}

			if (MyInternalEvent != null)
			{
				MyInternalEvent(this, EventArgs.Empty);
			}
		}

		public string BodyDoesNotChange()
		{
			object x = new object();

			Console.WriteLine(x.GetType());

			if (x.ToString() == "HELLO")
			{
				throw new NotImplementedException();
			}

			return "Hi1";
		}

		public string BodyDoesNotChange2()
		{
			return "777";
		}

		public string MethodBodyChanges()
		{
			object x = new object();

			return "Something else";
		}

		internal string MethodBecomesInternalAndBodyChanges()
		{
			string hello = "HELLO";
			return hello;
		}

		public int MethodSignatureChanges(int x, int y, int z)
		{
			return 3;
		}

		internal string InternalMethod(string x)
		{
			return x;
		}

		[Description("MethodFindsAttribute")]
		public void MethodFindsAttribute()
		{
		}

		[Description("MethodAttributeChanges 2")]
		public void MethodAttributeChanges()
		{
		}

		public string SimplePublicProperty
		{
			get { return "x"; }
			set { }
		}

		public string PropertyBodyGetChanges
		{
			get { return new object().ToString(); }
			set { object z = new object(); }
		}

		public string PropertyBodySetChanges
		{
			get { return "x"; }
			set { throw new NotImplementedException(); }
		}

		public string PropertyGetRemoved
		{
			set { }
		}

		public string PropertySetBecomesInternal
		{
			get { return "x"; }
			internal set { }
		}

		internal string PropertyBecomesInternal
		{
			get { return "x"; }
			set { }
		}

		public string PropertySetBecomesPublic
		{
			get { return "x"; }
			set { }
		}

		public string ReadOnlyProperty
		{
			get { return "x"; }
		}

		internal string InternalProperty
		{
			get { return "x"; }
			set { }
		}

		public string InternalSetProperty
		{
			get { return "x"; }
			internal set { }
		}

		public string PropertyAdded
		{
			get { return "x"; }
			set { }
		}

		[Description("This method has an attribute")]
		public void MethodWithAttribute()
		{
		}

		[Description("This property has an attribute")]
		public string PropertyWithAttribute
		{
			get { return "x"; }
			set { }
		}

		[Description("PropertyFindsAttribute")]
		public string PropertyFindsAttribute
		{
			get { return "x"; }
			set { ; }
		}

		internal void InternalMethodAdded()
		{
		}
	}

	[Description("This class has an attribute")]
	public class ClassWithAttribute
	{
	}

	public static class StaticClass
	{
		public static void SimpleStaticMethod()
		{
		}
	}

	internal class InternalClass
	{
	}

	internal static class InternalStaticClass
	{
	}

	public sealed class SealedClass
	{
	}

	public class ClassImplementsISimple : ISimple
	{
		public void Simple()
		{
		}
	}

	public class DerivedClass : BasicClass
	{
		public virtual void SomeMethod()
		{
		}
	}

	public class DerivedAndInterfaceClass : BasicClass, ISimple
	{
		public virtual void SomeMethod()
		{
		}

		public void Simple()
		{
		}
	}

	public class ChildClass : DerivedClass
	{
		public override void SomeMethod()
		{
			base.SomeMethod();
		}
	}

	public class ChildClass2 : DerivedClass
	{
		public new void SomeMethod()
		{
			base.SomeMethod();
		}
	}

	public abstract class AbstractClass
	{
		public abstract void SimpleAbstractMethod();
	}

	public class IndexerClass
	{
		public int this[int x]
		{
			get
			{
				return 3;
			}
		}

		protected int this[string str]
		{
			get
			{
				return 1;
			}
		}
	}

	public class ClassWithConstructors
	{
		public ClassWithConstructors()
		{
		}

		public ClassWithConstructors(int x)
		{
			throw new ArgumentException("x");
		}

		public ClassWithConstructors(string x)
		{
		}

		public void SomeMethod()
		{
		}
	}

	internal class AnInternalClassWithPublicMethodsChange
	{
		public void Method1(int x) { }
	}
}
