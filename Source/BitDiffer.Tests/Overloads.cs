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
using BitDiffer.Common.Interfaces;

namespace BitDiffer.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class Overloads : TestBase
	{
		[TestMethod]
		public void Overload_Exact_Match()
		{
			ClassDetail cd = ExtractCompared("OverloadsExactMatch");

			AssertOnlyChange(cd, "public void OverloadedMethod()", ChangeType.None);
			AssertOnlyChange(cd, "public void OverloadedMethod(string)", ChangeType.None);
			AssertOnlyChange(cd, "public void OverloadedMethod(int)", ChangeType.None);
			AssertOnlyChange(cd, "public void OverloadedMethod(string, int)", ChangeType.None);
		}

		[TestMethod]
		public void Overload_Reorder()
		{
			ClassDetail cd = ExtractCompared("OverloadsReordered");

			AssertOnlyChange(cd, "public void OverloadedMethod()", ChangeType.None);
			AssertOnlyChange(cd, "public void OverloadedMethod(string)", ChangeType.None);
			AssertOnlyChange(cd, "public void OverloadedMethod(int)", ChangeType.None);
			AssertOnlyChange(cd, "public void OverloadedMethod(string, int)", ChangeType.None);
		}

		[TestMethod]
		public void Overload_Added()
		{
			ClassDetail cd = ExtractCompared("OverloadAdded");

			AssertOnlyChange(cd, "public void OverloadedMethod(object)", ChangeType.Added);
		}

		[TestMethod]
		public void Overload_Removed()
		{
			ClassDetail cd = ExtractCompared("OverloadRemoved");

			AssertOnlyChange(cd, "public void OverloadedMethod(int)", ChangeType.RemovedBreaking);
		}

		[TestMethod]
		public void Overload_Reorder_And_Removed()
		{
			ClassDetail cd = ExtractCompared("OverloadsReorderedAndRemoved");

			AssertOnlyChange(cd, "public void OverloadedMethod(string)", ChangeType.RemovedBreaking);
		}

		[TestMethod]
		public void Overload_No_Matches()
		{
			ClassDetail cd = (ClassDetail)ExtractCompared("OverloadNoMatches").NavigateBackward;

			Assert.AreEqual(3, cd.Children.Count);
		}

		private ClassDetail ExtractCompared(string from)
		{
			ClassDetail c1 = ExtractClass(Subjects.One, from);
			ClassDetail c2 = ExtractClass(Subjects.Two, from);

			Align(c1, c2);

			c2.PerformCompare(c1);

			return c2;
		}

		private void AssertOnlyChange(ClassDetail cd, string declaration, ChangeType changeType)
		{
			bool found = false;

			foreach (ICanAlign child in cd.Children)
			{
				if (child is MethodDetail)
				{
					MethodDetail method = (MethodDetail)child;

					if ((method.GetTextDeclaration() == declaration) || (((MethodDetail)method.NavigateBackward).GetTextDeclaration() == declaration))
					{
						Assert.AreEqual(changeType, method.Change);
						found = true;
					}
					else
					{
						Assert.AreEqual(ChangeType.None, method.Change);
					}
				}
			}

			Assert.IsTrue(found, string.Format("Method '{0}' was not found in {1}", declaration, cd.Name));
		}
	}
}
