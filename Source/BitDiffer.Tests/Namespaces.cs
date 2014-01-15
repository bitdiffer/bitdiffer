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
	public class Namespaces : TestBase
	{
		[TestMethod]
		public void Namespace_Removed_Public()
		{
			AssertChange("BitDiffer.Tests.PublicNamespaceRemoved", ChangeType.RemovedBreaking);
		}

		[TestMethod]
		public void Namespace_Removed_Internal()
		{
			AssertChange("BitDiffer.Tests.InternalNamespaceRemoved", ChangeType.RemovedNonBreaking);
		}

		private void AssertChange(string name, ChangeType change)
		{
			NamespaceDetail n1 = ExtractNamespace(Subjects.One, name);
			NamespaceDetail n2 = ExtractNamespace(Subjects.Two, name);

			Align(n1, n2);

			Assert.AreEqual(change, n2.PerformCompare(n1));
		}
	}
}
