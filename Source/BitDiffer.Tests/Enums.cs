using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

using BitDiffer.Core;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class Enums : TestBase
	{
		[TestMethod]
		public void Enum_Extraction()
		{
			EnumDetail ei = ExtractEnum(Subjects.One, "Hairdo");
			Assert.AreEqual(Status.Present, ei.Status);
			Assert.AreEqual("public enum Hairdo", ei.ToString());
		}

		[TestMethod]
		public void Enum_WithAttributes()
		{
			EnumDetail ei = ExtractEnum(Subjects.One, "BagelType");
			Assert.AreEqual(Status.Present, ei.Status);
			CheckForAttribute(ei);
		}

		[TestMethod]
		public void Enum_Change_NoChange()
		{
			AssertChange("Hairdo", ChangeType.None);
		}

		[TestMethod]
		public void Enum_Change_NotBreakingInternal()
		{
			AssertChange("InternalEnumDeclChange", ChangeType.DeclarationChangedNonBreaking);
		}

		[TestMethod]
		public void Enum_Change_NotBreakingAdded()
		{
			AssertChange("PublicEnumDeclChange", ChangeType.DeclarationChangedNonBreaking);
		}

		[TestMethod]
		public void Enum_Change_PublicBreaking()
		{
			AssertChange("PublicEnumDeclChangeBreaking", ChangeType.DeclarationChangedBreaking);
		}

		[TestMethod]
		public void Enum_Change_Attributes()
		{
			AssertChange("EnumLosesAttribute", ChangeType.AttributesChanged);
		}

		[TestMethod]
		public void Enum_Change_Value()
		{
			EnumDetail r1 = ExtractEnum(Subjects.One, "EnumValueChanges");
			EnumDetail r2 = ExtractEnum(Subjects.Two, "EnumValueChanges");

			Align(r1, r2);

			r2.PerformCompare(r1);

			Assert.AreEqual(ChangeType.DeclarationChangedBreaking, r2.Change);

			IEnumerator<ICanCompare> values = r2.FilterChildren<ICanCompare>().GetEnumerator();

			values.MoveNext();
			Assert.AreEqual(ChangeType.None, values.Current.Change);
			values.MoveNext();
			Assert.AreEqual(ChangeType.ValueChangedBreaking, values.Current.Change);
		}

		private void AssertChange(string name, ChangeType change)
		{
			EnumDetail r1 = ExtractEnum(Subjects.One, name);
			EnumDetail r2 = ExtractEnum(Subjects.Two, name);

			Align(r1, r2);

			Assert.AreEqual(change, r2.PerformCompare(r1));
		}
	}
}
