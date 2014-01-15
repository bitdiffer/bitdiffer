using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Diagnostics;

using BitDiffer.Core;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class Fields : TestBase
	{
		[TestMethod]
		public void Field_Extraction()
		{
			FieldDetail fi = ExtractField(Subjects.One, "BasicClass", "_publicField");
			Assert.AreEqual(Status.Present, fi.Status);
			Assert.AreEqual("public string _publicField", fi.ToString());
		}

		[TestMethod]
		public void Field_WithAttributes()
		{
			FieldDetail fi = ExtractField(Subjects.One, "BasicClass", "_fieldWithAttribute");
			Assert.AreEqual(Status.Present, fi.Status);
			CheckForAttribute(fi);
		}

		[TestMethod]
		public void Field_Const_Int()
		{
			FieldDetail fi = ExtractField(Subjects.One, "BasicClass", "_constInt");
			Assert.AreEqual(Status.Present, fi.Status);
			Assert.AreEqual("public const int _constInt = 3", fi.ToString());
		}

		[TestMethod]
		public void Field_Const_String()
		{
			FieldDetail fi = ExtractField(Subjects.One, "BasicClass", "_constString");
			Assert.AreEqual(Status.Present, fi.Status);
			Assert.AreEqual("public const string _constString = \"This is const\"", fi.ToString());
		}

		[TestMethod]
		public void Field_Static()
		{
			FieldDetail fi = ExtractField(Subjects.One, "BasicClass", "_staticField");
			Assert.AreEqual(Status.Present, fi.Status);
			Assert.AreEqual("public static string _staticField", fi.ToString());
		}

		[TestMethod]
		public void Field_ReadOnly()
		{
			FieldDetail fi = ExtractField(Subjects.One, "BasicClass", "_readOnlyField");
			Assert.AreEqual(Status.Present, fi.Status);
			Assert.AreEqual("public readonly string _readOnlyField", fi.ToString());
		}

		[TestMethod]
		public void Field_Change_NoChange()
		{
			AssertChange("BasicClass", "_staticField", ChangeType.None);
		}

		[TestMethod]
		public void Field_Change_NotBreaking()
		{
			AssertChange("BasicClass", "_privateFieldDeclChange", ChangeType.DeclarationChangedNonBreaking);
		}

		[TestMethod]
		public void Field_Change_Breaking()
		{
			AssertChange("BasicClass", "_publicFieldDeclChange", ChangeType.DeclarationChangedBreaking);
		}

		[TestMethod]
		public void Field_Change_Attributes()
		{
			AssertChange("BasicClass", "_fieldLosesAttribute", ChangeType.AttributesChanged);
		}

        [TestMethod]
        public void Field_Change_Type()
        {
            AssertChange("BasicClass", "_fieldChangesType", ChangeType.DeclarationChangedNonBreaking);
        }

        private void AssertChange(string from, string name, ChangeType change)
		{
			FieldDetail r1 = ExtractField(Subjects.One, from, name);
			FieldDetail r2 = ExtractField(Subjects.Two, from, name);

			Align(r1, r2);

			Assert.AreEqual(change, r1.PerformCompare(r2));
		}
	}
}
