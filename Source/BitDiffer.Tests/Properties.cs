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
	public class Properties : TestBase
	{
		[TestMethod]
		public void Property_Extraction()
		{
			PropertyDetail pi = ExtractProperty(Subjects.One, "BasicClass", "SimplePublicProperty");
			Assert.AreEqual(Status.Present, pi.Status);
			Assert.AreEqual("public string SimplePublicProperty", pi.ToString());
		}

		[TestMethod]
		public void Property_WithAttributes()
		{
			PropertyDetail pi = ExtractProperty(Subjects.One, "BasicClass", "PropertyWithAttribute");
			Assert.AreEqual(Status.Present, pi.Status);
			CheckForAttribute(pi);
		}

		[TestMethod]
		public void Property_ReadOnly()
		{
			PropertyDetail pi = ExtractProperty(Subjects.One, "BasicClass", "ReadOnlyProperty");
			Assert.AreEqual(Status.Present, pi.Status);
			Assert.AreEqual("public string ReadOnlyProperty", pi.ToString());
			// TODO exmaine description for accessors
		}

		[TestMethod]
		public void Property_InternalSet()
		{
			PropertyDetail pi = ExtractProperty(Subjects.One, "BasicClass", "InternalSetProperty");
			Assert.AreEqual(Status.Present, pi.Status);
			Assert.AreEqual("public string InternalSetProperty", pi.ToString());
			// TODO exmaine description for accessors
		}

		[TestMethod]
		public void Property_Change_NoChange()
		{
			AssertChange("BasicClass", "SimplePublicProperty", ChangeType.None);
		}

		[TestMethod]
		public void Property_Change_BodyGetChanges()
		{
			AssertChange("BasicClass", "PropertyBodyGetChanges", ChangeType.ImplementationChanged);
		}

		[TestMethod]
		public void Property_Change_BodySetChanges()
		{
			AssertChange("BasicClass", "PropertyBodySetChanges", ChangeType.ImplementationChanged);
		}

		[TestMethod]
		public void Property_Change_DeclarationChanged_GetRemoved()
		{
			AssertChange("BasicClass", "PropertyGetRemoved", ChangeType.DeclarationChangedBreaking);
		}

		[TestMethod]
		public void Property_Change_DeclarationChanged_SetVisibilityChanged()
		{
			AssertChange("BasicClass", "PropertySetBecomesInternal", ChangeType.VisibilityChangedBreaking);
		}

		[TestMethod]
		public void Property_Change_VisbilityChanged()
		{
			AssertChange("BasicClass", "PropertyBecomesInternal", ChangeType.VisibilityChangedBreaking);
		}

		[TestMethod]
		public void Property_Change_DeclarationChanged_PropertySetBecomesPublic()
		{
			AssertChange("BasicClass", "PropertySetBecomesPublic", ChangeType.VisibilityChangedNonBreaking);
		}

		[TestMethod]
		public void Property_Change_Attributes()
		{
			AssertChange("BasicClass", "PropertyFindsAttribute", ChangeType.AttributesChanged);
		}

		private void AssertChange(string from, string name, ChangeType change)
		{
			AssertChange(from, name, change, DiffConfig.Default);
		}

		private void AssertChange(string from, string name, ChangeType change, DiffConfig config)
		{
			PropertyDetail r1 = ExtractProperty(Subjects.One, from, name, config);
			PropertyDetail r2 = ExtractProperty(Subjects.Two, from, name, config);

			Align(r1, r2);

			Assert.AreEqual(change, r2.PerformCompare(r1));
		}
	}
}
