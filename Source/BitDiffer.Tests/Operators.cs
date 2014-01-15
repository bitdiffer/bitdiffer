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
	public class OperatorTests : TestBase
	{
		[TestMethod]
		public void Operator_Extraction()
		{
			MethodDetail mi = ExtractOperator(Subjects.One, "Operators", "op_Equality");
			Assert.AreEqual(Status.Present, mi.Status);
			Assert.AreEqual("public static bool op_Equality(BitDiffer.Tests.Subject.Operators, BitDiffer.Tests.Subject.Operators)", mi.ToString());
		}

		[TestMethod]
		public void Operator_Change()
		{
			AssertChange("Operators", "op_Equality", ChangeType.RemovedBreaking);
		}

		private void AssertChange(string from, string name, ChangeType change)
		{
			AssertChange(from, name, change, DiffConfig.Default);
		}

		private void AssertChange(string from, string name, ChangeType change, DiffConfig config)
		{
			MemberDetail r1 = ExtractOperator(Subjects.One, from, name, config);
			MemberDetail r2 = ExtractOperator(Subjects.Two, from, name, config);

			Align(r1, r2);

			Assert.AreEqual(change, r2.PerformCompare(r1));
		}
	}
}
