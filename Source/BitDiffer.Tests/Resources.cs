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
	public class Resources : TestBase
	{
		[TestMethod]
		public void Resource_Extraction()
		{
			ResourceDetail ri = ExtractResource(Subjects.One, "BitDiffer.Tests.Subject.ResourceA.txt");
			Assert.AreEqual(Status.Present, ri.Status);
			Assert.AreEqual("5A555A09783EBF2C664B570A8367C151CD05C010", ri.ToString());
		}

		[TestMethod]
		public void Resource_NoChange()
		{
			ResourceDetail ri1 = ExtractResource(Subjects.One, "BitDiffer.Tests.Subject.ResourceA.txt");
			ResourceDetail ri2 = ExtractResource(Subjects.Two, "BitDiffer.Tests.Subject.ResourceA.txt");

			Assert.AreEqual(ChangeType.None, ri1.PerformCompare(ri2));
		}

		[TestMethod]
		public void Resource_Changed()
		{
			ResourceDetail ri1 = ExtractResource(Subjects.One, "BitDiffer.Tests.Subject.ResourceB.txt");
			ResourceDetail ri2 = ExtractResource(Subjects.Two, "BitDiffer.Tests.Subject.ResourceB.txt");

			Assert.AreEqual(ChangeType.ContentChanged, ri1.PerformCompare(ri2));
		}
	}
}
