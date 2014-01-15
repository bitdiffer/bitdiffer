using System;
using System.Collections.Generic;
using System.Text;

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
		X = 1, Y = 5, Z = 8
	};

	internal enum PublicEnumBecomesInternal
	{
		A, B, C
	};

	public enum InternalEnumBecomesPublic
	{
		A, B, C
	};

	public enum PublicEnumDeclChange
	{
		A, B, C, D
	};

	public enum PublicEnumDeclChangeBreaking
	{
		A, B
	};

	internal enum InternalEnumDeclChange
	{
		A, B
	};

	public enum InternalEnumBecomesPublicAndDeclChange
	{
		A, B, C, D
	};

	internal enum PublicEnumBecomesInternalAndDeclChange
	{
		A, B, C, D
	};

	public enum EnumLosesAttribute
	{
		A, B, C
	};
}
