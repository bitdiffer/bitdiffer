using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

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
	public class VisibilityTests : TestBase
	{
		[TestMethod]
		public void Visibility_Extract_Internal()
		{
			EnumDetail ed = ExtractEnum(Subjects.One, "InternalEnumBecomesPublic");
			Assert.AreEqual(Visibility.Internal, ed.Visibility);
		}

		[TestMethod]
		public void Visibility_Extract_Public()
		{
			EnumDetail ed = ExtractEnum(Subjects.One, "PublicEnumBecomesInternal");
			Assert.AreEqual(Visibility.Public, ed.Visibility);
		}

		[TestMethod]
		public void Visibility_Change_NotBreaking()
		{
			AssertChange("InternalEnumBecomesPublic", ChangeType.VisibilityChangedNonBreaking);
		}

		[TestMethod]
		public void Visibility_Change_Breaking()
		{
			AssertChange("PublicEnumBecomesInternal", ChangeType.VisibilityChangedBreaking);
		}

		[TestMethod]
		public void Visibility_Change_MultiChange1()
		{
			AssertChange("InternalEnumBecomesPublicAndDeclChange", ChangeType.DeclarationChangedNonBreaking | ChangeType.VisibilityChangedNonBreaking);
		}

		[TestMethod]
		public void Visibility_Change_MultiChange2()
		{
			AssertChange("PublicEnumBecomesInternalAndDeclChange", ChangeType.DeclarationChangedNonBreaking | ChangeType.VisibilityChangedBreaking);
		}

		private void AssertChange(string name, ChangeType change)
		{
			RootDetail r1 = ExtractEnum(Subjects.One, name);
			RootDetail r2 = ExtractEnum(Subjects.Two, name);

			Align(r1, r2);

			Assert.AreEqual(Status.Present, r1.Status);
			Assert.AreEqual(Status.Present, r2.Status);

			Assert.AreEqual(change, r2.PerformCompare(r1));
		}
	}
}
