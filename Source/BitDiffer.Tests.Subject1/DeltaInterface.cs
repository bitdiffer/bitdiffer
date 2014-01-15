using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BitDiffer.Tests.Subject
{
	public interface InterfaceBecomesInternal
	{

	}

	internal interface InterfaceBecomesPublic
	{
	}

	public interface InterfaceAddsAttribute
	{
	}

	public interface PublicInterfacePropertyChanges
	{
		string Prop { get; set; }
	}

	[Description("InterfaceRemovesAttributeAndBecomesInternal")]
	public interface InterfaceRemovesAttributeAndBecomesInternal
	{
	}

	internal interface InternalInterfaceMethodChanges
	{
		void Method1();
	}

	public interface InterfaceNoChange
	{
	}
}
