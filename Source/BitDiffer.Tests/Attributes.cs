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
	public class Attributes : TestBase
	{
		[TestMethod]
		public void Attribute_Extraction()
		{
			AttributeDetail ai = ExtractAttribute(Subjects.One, "System.Reflection.AssemblyConfigurationAttribute");
			Assert.AreEqual(Status.Present, ai.Status);
			Assert.AreEqual("System.Reflection.AssemblyConfigurationAttribute(\"Test\")", ai.Declaration);
		}

		[TestMethod]
		public void Attribute_Change_NoChange()
		{
			AssertChange("System.Reflection.AssemblyTitleAttribute", ChangeType.None);
		}

		[TestMethod]
		public void Attribute_Change_Modified()
		{
			AssertChange("System.Reflection.AssemblyDescriptionAttribute", ChangeType.DeclarationChangedNonBreaking);
		}

		private void AssertChange(string name, ChangeType change)
		{
			RootDetail r1 = ExtractAttribute(Subjects.One, name);
			RootDetail r2 = ExtractAttribute(Subjects.Two, name);

			Assert.AreEqual(change, r1.PerformCompare(r2));
		}
	}
}
