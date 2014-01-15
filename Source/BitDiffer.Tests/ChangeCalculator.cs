using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	[TestClass]
	public class ChangeCalculatorTests
	{
		private static StubDetail _missing;

		[ClassInitialize]
		public static void ClassInit(TestContext ctx)
		{
			_missing = new StubDetail("", "", Visibility.Public);
			_missing.Status = Status.Missing;
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Change_DifferentTypes()
		{
			ResourceDetail r1 = new ResourceDetail(null, "test", new byte[0]);
			StubDetail s1 = new StubDetail("x1", null, Visibility.Public);

			r1.PerformCompare(s1);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Change_DifferentName()
		{
			StubDetail sd1 = new StubDetail("X1", "", Visibility.Public);
			StubDetail sd2 = new StubDetail("X2", "", Visibility.Public);

			sd1.PerformCompare(sd2);
		}

		[TestMethod]
		public void Change_Added()
		{
			Assert.IsTrue(new StubDetail("A1", "", Visibility.Public).PerformCompare(_missing) == ChangeType.Added);
		}

		[TestMethod]
		public void Change_Removed_Public()
		{
			Assert.IsTrue(_missing.PerformCompare(new StubDetail("X1", "", Visibility.Public)) == ChangeType.RemovedBreaking);
		}

		[TestMethod]
		public void Change_Removed_Internal()
		{
			Assert.IsTrue(_missing.PerformCompare(new StubDetail("X1", "", Visibility.Internal)) == ChangeType.RemovedNonBreaking);
		}

		[TestMethod]
		public void Change_Missing()
		{
			Assert.IsTrue(_missing.PerformCompare(_missing) == ChangeType.None);
		}
	}
}
