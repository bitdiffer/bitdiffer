using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

using BitDiffer.Core;
using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class DirCompareTests : TestBase
	{
		[TestMethod]
		public void DirCompare_Both()
		{
			AssemblyComparer ac = new AssemblyComparer();

            AssemblyComparison results = ac.CompareAssemblyDirectories(false, DiffConfig.Default, ComparisonFilter.Default, Subjects.DirOne, Subjects.DirTwo);

			// Two assemblies present in both directories
			Assert.AreEqual(2, results.Groups.Count);
			Assert.AreEqual(2, results.Groups[0].Assemblies.Count);
			Assert.AreEqual(2, results.Groups[1].Assemblies.Count);
		}

		[TestMethod]
		public void DirCompare_OneEmpty()
		{
			AssemblyComparer ac = new AssemblyComparer();

            AssemblyComparison results = ac.CompareAssemblyDirectories(false, DiffConfig.Default, ComparisonFilter.Default, Subjects.DirOne, Subjects.DirEmpty);

			// Two assemblies present in just one directories
			Assert.AreEqual(2, results.Groups.Count);
			Assert.AreEqual(1, results.Groups[0].Assemblies.Count);
			Assert.AreEqual(1, results.Groups[1].Assemblies.Count);
		}
	}
}
