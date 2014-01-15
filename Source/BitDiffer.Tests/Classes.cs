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
	public class Classes : TestBase
	{
		[TestMethod]
		public void Class_Extraction()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "BasicClass");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public class BasicClass", ci.ToString());
		}

		[TestMethod]
		public void Class_WithAttributes()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "ClassWithAttribute");
			Assert.AreEqual(Status.Present, ci.Status);
			CheckForAttribute(ci);
		}

		[TestMethod]
		public void Class_Static()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "StaticClass");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public static class StaticClass", ci.ToString());
		}

		[TestMethod]
		public void Class_Sealed()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "SealedClass");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public sealed class SealedClass", ci.ToString());
		}

		[TestMethod]
		public void Class_ImplementInterface()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "ClassImplementsISimple");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public class ClassImplementsISimple", ci.ToString());
		}

		[TestMethod]
		public void Class_Derived()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "DerivedClass");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public class DerivedClass", ci.ToString());
		}

		[TestMethod]
		public void Class_DerivedWithInterface()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "DerivedAndInterfaceClass");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public class DerivedAndInterfaceClass", ci.ToString());
		}

		[TestMethod]
		public void Class_Abstract()
		{
			ClassDetail ci = ExtractClass(Subjects.One, "AbstractClass");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("public abstract class AbstractClass", ci.ToString());
		}

		[TestMethod]
		public void Class_Nested_InterfaceExtraction()
		{
			InterfaceDetail ii = ExtractNestedInterface(Subjects.One, "ParentClass", "INestedPublicInterface");
			Assert.AreEqual(Status.Present, ii.Status);
			Assert.AreEqual("public interface INestedPublicInterface", ii.ToString());
		}

		[TestMethod]
		public void Class_Nested_EnumExtraction()
		{
			EnumDetail ei = ExtractNestedEnum(Subjects.One, "ParentClass", "NestedPrivateEnum");
			Assert.AreEqual(Status.Present, ei.Status);
			Assert.AreEqual("private enum NestedPrivateEnum", ei.ToString());
		}

		[TestMethod]
		public void Class_Nested_ClassExtraction()
		{
			ClassDetail ci = ExtractNestedClass(Subjects.One, "ParentClass", "NestedPrivateClass");
			Assert.AreEqual(Status.Present, ci.Status);
			Assert.AreEqual("private class NestedPrivateClass", ci.ToString());
		}

		[TestMethod]
		public void Class_Nested_TwiceClassExtraction()
		{
			ClassDetail ci = ExtractNestedClass(Subjects.One, "ParentClass", "NestedProtectedClass");
			Assert.AreEqual(Status.Present, ci.Status);
			ClassDetail gc = ListOperations.FindOrReturnMissing<ClassDetail>(ci.FilterChildren<ClassDetail>(), "NestedGrandchildClass");
			Assert.AreEqual(Status.Present, gc.Status);
		}

		[TestMethod]
		public void Class_Change_NoChange()
		{
			AssertChange("ClassNoChange", ChangeType.None);
		}

		[TestMethod]
		public void Class_Change_BecomesInternal()
		{
			AssertChange("ClassBecomesInternal", ChangeType.VisibilityChangedBreaking);
		}

		[TestMethod]
		public void Class_Change_BecomesPublic()
		{
			AssertChange("ClassBecomesPublic", ChangeType.VisibilityChangedNonBreaking);
		}

		[TestMethod]
		public void Class_Change_BecomesStatic()
		{
			AssertChange("ClassBecomesStatic", ChangeType.DeclarationChangedBreaking | ChangeType.MembersChangedBreaking);
		}

		[TestMethod]
		public void Class_Change_AddsAttribute()
		{
			AssertChange("ClassAddsAttribute", ChangeType.AttributesChanged);
		}

		[TestMethod]
		public void Class_Change_PublicMethodChanges()
		{
			AssertChange("ClassPublicMethodChanges", ChangeType.MembersChangedBreaking | ChangeType.MembersChangedNonBreaking);
		}

		[TestMethod]
		public void Class_Change_InternalMethodChanges()
		{
			AssertChange("ClassInternalMethodChanges", ChangeType.MembersChangedNonBreaking);
		}

		[TestMethod]
		public void Class_Change_PropertySetBecomesInternal()
		{
			AssertChange("ClassPropertySetBecomesInternal", ChangeType.MembersChangedBreaking);
		}

		[TestMethod]
		public void Class_Change_RemovesAttributeAndBecomesInternal()
		{
			AssertChange("ClassRemovesAttributeAndBecomesInternal", ChangeType.AttributesChanged | ChangeType.VisibilityChangedBreaking);
		}

		[TestMethod]
		public void Class_Change_FieldAddsAttribute()
		{
			AssertChange("ClassFieldAddsAttribute", ChangeType.MembersChangedNonBreaking);
		}

		[TestMethod]
		public void Class_Change_MultipleChanges()
		{
			AssertChange("BasicClass", ChangeType.MembersChangedBreaking | ChangeType.MembersChangedNonBreaking);
		}

		[TestMethod]
		public void Class_Change_NestedPrivateClass()
		{
			AssertChange("ParentClassPrivateNestedClassChanges", ChangeType.MembersChangedNonBreaking);
		}

		[TestMethod]
		public void Class_Change_NestedPrivateEnum()
		{
			AssertChange("ParentClassPrivateNestedEnumChanges", ChangeType.MembersChangedNonBreaking);
		}

		[TestMethod]
		public void Class_Change_NestedPrivateInterface()
		{
			AssertChange("ParentClassPrivateNestedInterfaceChanges", ChangeType.MembersChangedNonBreaking);
		}

		[TestMethod]
		public void Class_Change_NestedPublicGrandchild()
		{
			AssertChange("ParentClassPublicGrandchildChanges", ChangeType.MembersChangedBreaking);
		}

		[TestMethod]
		public void Class_Change_NestedNoChange()
		{
			AssertChange("ParentClassNoChange", ChangeType.None);
		}

		private void AssertChange(string name, ChangeType change)
		{
			ClassDetail c1 = ExtractClass(Subjects.One, name);
			ClassDetail c2 = ExtractClass(Subjects.Two, name);

			Align(c1, c2);

			Assert.AreEqual(change, c2.PerformCompare(c1));
		}

		private InterfaceDetail ExtractNestedInterface(string assembly, string className, string interfaceName)
		{
			return ExtractNestedInterface(assembly, className, interfaceName, DiffConfig.Default);
		}

		private InterfaceDetail ExtractNestedInterface(string assembly, string className, string interfaceName, DiffConfig config)
		{
			ClassDetail cd = ExtractClass(assembly, className, config);
			Assert.AreEqual(Status.Present, cd.Status);
			return ListOperations.FindOrReturnMissing(cd.FilterChildren<InterfaceDetail>(), interfaceName);
		}

		private EnumDetail ExtractNestedEnum(string assembly, string className, string interfaceName)
		{
			ClassDetail cd = ExtractClass(assembly, className);
			Assert.AreEqual(Status.Present, cd.Status);
			return ListOperations.FindOrReturnMissing(cd.FilterChildren<EnumDetail>(), interfaceName);
		}

		private ClassDetail ExtractNestedClass(string assembly, string className, string interfaceName)
		{
			ClassDetail cd = ExtractClass(assembly, className);
			Assert.AreEqual(Status.Present, cd.Status);
			return ListOperations.FindOrReturnMissing(cd.FilterChildren<ClassDetail>(), interfaceName);
		}
	}
}
