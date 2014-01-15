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
	public class Interfaces : TestBase
	{
		[TestMethod]
		public void Interface_Extraction()
		{
			InterfaceDetail ii = ExtractInterface(Subjects.One, "IFoo");
			Assert.AreEqual(Status.Present, ii.Status);
			Assert.AreEqual("public interface IFoo", ii.ToString());
		}

		[TestMethod]
		public void Interface_WithAttributes()
		{
			InterfaceDetail ii = ExtractInterface(Subjects.One, "IInterfaceWithAttribute");
			Assert.AreEqual(Status.Present, ii.Status);
			CheckForAttribute(ii);
		}

		[TestMethod]
		public void Interface_Change_NoChange()
		{
			AssertChange("InterfaceNoChange", ChangeType.None);
		}

		[TestMethod]
		public void Interface_Change_InternalMethodChanges()
		{
			AssertChange("InternalInterfaceMethodChanges", ChangeType.MembersChangedNonBreaking);
		}

		[TestMethod]
		public void Interface_Change_PublicProperyChanges()
		{
			AssertChange("PublicInterfacePropertyChanges", ChangeType.MembersChangedBreaking);
		}

		[TestMethod]
		public void Interface_Change_RemovesAttributeAndBecomesInternal()
		{
			AssertChange("InterfaceRemovesAttributeAndBecomesInternal", ChangeType.AttributesChanged | ChangeType.VisibilityChangedBreaking);
		}

		[TestMethod]
		public void Interface_Change_AddsAttribute()
		{
			AssertChange("InterfaceAddsAttribute", ChangeType.AttributesChanged);
		}

		[TestMethod]
		public void Interface_Change_BecomesPublic()
		{
			AssertChange("InterfaceBecomesPublic", ChangeType.VisibilityChangedNonBreaking);
		}

		[TestMethod]
		public void Interface_Change_BecomesInternal()
		{
			AssertChange("InterfaceBecomesInternal", ChangeType.VisibilityChangedBreaking);
		}

		private void AssertChange(string name, ChangeType change)
		{
			InterfaceDetail r1 = ExtractInterface(Subjects.One, name);
			InterfaceDetail r2 = ExtractInterface(Subjects.Two, name);

			Align(r1, r2);

			Assert.AreEqual(change, r2.PerformCompare(r1));
		}
	}
}
