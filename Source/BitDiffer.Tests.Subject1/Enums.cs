using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BitDiffer.Tests.Subject
{
	public enum Hairdo
	{
		Bun,
		Afro,
		Bald,
		Mohawk
	};

	[Flags]
	public enum BagelType
	{
		Plain = 0,
		Cheese = 1,
		Onions = 2,
		Eggs = 4,
		Everything = 8
	};

	internal enum InternalEnum
	{
		X, Y, Z
	};

	public enum EnumValueChanges
	{
		X = 1, Y = 2, Z = 8
	};

	public enum PublicEnumBecomesInternal
	{
		A, B, C
	};

	internal enum InternalEnumBecomesPublic
	{
		A, B, C
	};

	public enum PublicEnumDeclChange
	{
		A, B, C
	};

	public enum PublicEnumDeclChangeBreaking
	{
		A, B, C
	};

	internal enum InternalEnumDeclChange
	{
		A, B, C
	};

	internal enum InternalEnumBecomesPublicAndDeclChange
	{
		A, B, C
	};

	public enum PublicEnumBecomesInternalAndDeclChange
	{
		A, B, C
	};

	[Description("EnumLosesAttribute")]
	public enum EnumLosesAttribute
	{
		A, B, C
	};
}
