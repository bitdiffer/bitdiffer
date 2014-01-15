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
	public class Methods : TestBase
	{
		[TestMethod]
		public void Method_Extraction()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "BasicClass", "SimplePublicMethod");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public void SimplePublicMethod()", mi.ToString());
		}

		[TestMethod]
		public void Method_WithAttributes()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "BasicClass", "MethodWithAttribute");
			Assert.AreEqual(Status.Present, mi.Status);
			CheckForAttribute(mi);
		}

		[TestMethod]
		public void Method_Virtual()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "DerivedClass", "SomeMethod");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public virtual void SomeMethod()", mi.ToString());
		}

		[TestMethod]
		public void Method_Override()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "ChildClass", "SomeMethod");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public override void SomeMethod()", mi.ToString());
		}

		[TestMethod]
		public void Method_Abstract()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "AbstractClass", "SimpleAbstractMethod");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public abstract void SimpleAbstractMethod()", mi.ToString());
		}

		[TestMethod]
		public void Method_Static()
		{
			MethodDetail mi = ExtractMethod(Subjects.One, "StaticClass", "SimpleStaticMethod");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public static void SimpleStaticMethod()", mi.ToString());
		}

		[TestMethod]
		public void Method_Change_NoChange()
		{
			AssertChange("BasicClass", "BodyDoesNotChange", ChangeType.None);
		}

		[TestMethod]
		public void Method_Change_NoChange2()
		{
			AssertChange("BasicClass", "BodyDoesNotChange2", ChangeType.None);
		}

		[TestMethod]
		public void Method_Change_BodyChanges()
		{
			AssertChange("BasicClass", "MethodBodyChanges", ChangeType.ImplementationChanged);
		}

		[TestMethod]
		public void Method_Change_BodyAndVisibilityChanges()
		{
			AssertChange("BasicClass", "MethodBecomesInternalAndBodyChanges", ChangeType.ImplementationChanged | ChangeType.VisibilityChangedBreaking);
		}

		[TestMethod]
		public void Method_Change_SignatureChanges()
		{
			AssertChange("BasicClass", "MethodSignatureChanges", ChangeType.DeclarationChangedBreaking);
		}

		[TestMethod]
		public void Method_Change_AttributeAdded()
		{
			AssertChange("BasicClass", "MethodFindsAttribute", ChangeType.AttributesChanged);
		}

		[TestMethod]
		public void Method_Change_AttributeChanged()
		{
			AssertChange("BasicClass", "MethodAttributeChanges", ChangeType.AttributesChanged);
		}

		[TestMethod]
		public void Method_Change_PublicRemoved()
		{
			AssertChange("BasicClass", "PublicMethodRemoved", ChangeType.RemovedBreaking);
		}

		[TestMethod]
		public void Method_Change_InternalRemoved()
		{
			AssertChange("BasicClass", "InternalMethodRemoved", ChangeType.RemovedNonBreaking);
		}

		[TestMethod]
		public void Method_Change_Added()
		{
			AssertChange("BasicClass", "InternalMethodAdded", ChangeType.Added);
		}

		private void AssertChange(string from, string name, ChangeType change)
		{
			AssertChange(from, name, change, DiffConfig.Default);
		}

		private void AssertChange(string from, string name, ChangeType change, DiffConfig config)
		{
			MemberDetail r1 = ExtractMethod(Subjects.One, from, name, config);
			MemberDetail r2 = ExtractMethod(Subjects.Two, from, name, config);

			Align(r1, r2);

			Assert.AreEqual(change, r2.PerformCompare(r1));
		}
	}
}
