using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BitDiffer.Tests.Subject
{
	internal interface InterfaceBecomesInternal
	{

	}

	public interface InterfaceBecomesPublic
	{
	}

	public interface PublicInterfacePropertyChanges
	{
		string Prop { get; }
	}

	[Description("InterfaceAddsAttribute")]
	public interface InterfaceAddsAttribute
	{
	}

	internal interface InterfaceRemovesAttributeAndBecomesInternal
	{
	}

	internal interface InternalInterfaceMethodChanges
	{
		void Method1(string x);
	}

	public interface InterfaceNoChange
	{
	}
}
