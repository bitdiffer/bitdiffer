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
	public class GenericsTests : TestBase
	{
		[TestMethod]
		public void Generics_Class_Extraction()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "GenericClass<T>");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public class GenericClass<T>", ci.ToString());
		}

		[TestMethod]
		public void Generics_Class_ExtractionTwoArgs()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "GenericClassTwoArgs<T, U>");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public class GenericClassTwoArgs<T, U>", ci.ToString());
		}

		[TestMethod]
		public void Generics_Method_Extraction()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "NormalClass", "Foo<T>");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public string Foo<T>(T)", mi.ToString());
		}

		[TestMethod]
		public void Generics_Method_Restricted_Extraction()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "NormalClass", "FooRestricted<T>");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public string FooRestricted<T>(T) where T : System.ComponentModel.INotifyPropertyChanged", mi.ToString());
		}
	
		[TestMethod]
		public void Generics_Nullables_Extraction()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "NormalClass", "NullableParameters");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public string NullableParameters(bool?, int?)", mi.ToString());
		}

		[TestMethod]
		public void Generics_Parameters_Extraction()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "GenericClass<T>", "GenericListParameter");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public string GenericListParameter(System.Collections.Generic.List<T>)", mi.ToString());
		}

		[TestMethod]
		public void Generics_Class_Extract_Restriction()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "GenericClass1Restriction<T>");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public class GenericClass1Restriction<T> where T : System.IComparable", ci.ToString());
		}

		[TestMethod]
		public void Generics_Class_Extract_MultiRestriction()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "GenericMultipleRestrictions<T, U>");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("class GenericMultipleRestrictions<T, U> where T : class, System.IComparable where U : class, new(), System.ComponentModel.IBindingList", ci.Declaration);
		}

		[TestMethod]
		public void Generics_Compare_Class_AddRestriction()
		{
			AssertClassChange("GenericClassAddsRestriction<T>", ChangeType.DeclarationChangedBreaking);
		}

		[TestMethod]
		public void Generics_Compare_Method_AddRestriction()
		{
			AssertMethodChange("GenericMethodAddsRestriction", "Foo<T>", ChangeType.DeclarationChangedBreaking);
		}

		private void AssertMethodChange(string from, string name, ChangeType change)
		{
			MemberDetail r1 = ExtractMethod(Subjects.One, from, name);
			MemberDetail r2 = ExtractMethod(Subjects.Two, from, name);

			Align(r1, r2);

			Assert.AreEqual(change, r1.PerformCompare(r2));
		}

		private void AssertClassChange(string name, ChangeType change)
		{
			ClassDetail c1 = ExtractClass(Subjects.One, name);
			ClassDetail c2 = ExtractClass(Subjects.Two, name);

			Align(c1, c2);

			Assert.AreEqual(change, c1.PerformCompare(c2));
		}
	}
}
