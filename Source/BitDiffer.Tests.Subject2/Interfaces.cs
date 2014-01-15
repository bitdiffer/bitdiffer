using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BitDiffer.Tests.Subject
{
	public interface ISimple
	{
		void Simple();
	}

	public interface IFoo
	{
		void SimpleMethod();

		string BasicMethod(string x);

		[Description("This is a method with an attribute")]
		string MethodWithAttribute();

		string ReadWriteProp { get; set; }

		string ReadOnlyProp { get; }

		string WriteOnlyProp { set; }

		[Description("This is a property with an attribute")]
		string PropWithAttribute { get; set; }
	};

	[Guid("59300B13-E698-40e3-977A-621DAE7DBDD6")]
	public interface IInterfaceWithAttribute
	{
		void SimpleMethod();
	};

	internal interface IInternalInterface
	{
		void SimpleMethod();
	};
}
