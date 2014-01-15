using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BitDiffer.Common.Configuration;
using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;
using BitDiffer.Core;
using BitDiffer.Common.Interfaces;

namespace BitDiffer.Tests
{
	[TestClass]
	public class Filters
	{
		[TestMethod]
		public void Filters_ChangedOnly_Removed()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.ChangedItemsOnly = true;
			DoCompareAndVerifyRemoved(filter, "BitDiffer.Tests.Subject", "BasicClass", "_privateField");
		}

		[TestMethod]
		public void Filters_ChangedOnly_StillPresent()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.ChangedItemsOnly = true;
			DoCompareAndVerifyStillPresent(filter, "BitDiffer.Tests.Subject", "BasicClass", "PublicEventDeclChange");
		}

		[TestMethod]
		public void Filters_ChangedOnly_Nested_Child()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.ChangedItemsOnly = true;
			DoCompareAndVerifyStillPresent(filter, "BitDiffer.Tests.Subject", "ParentClassPublicGrandchildChanges", "NestedPublicClass");
		}

		[TestMethod]
		public void Filters_ChangedOnly_Nested_Parent()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.ChangedItemsOnly = true;
			DoCompareAndVerifyRemoved(filter, "BitDiffer.Tests.Subject", "ParentClass");
		}

		[TestMethod]
		public void Filters_ChangedOnly_ParentRemovedByChild()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.ChangedItemsOnly = true;
			filter.IgnoreAssemblyAttributeChanges = true;
			DoCompareAndVerifyRemoved(filter, "Attributes");
		}

		[TestMethod]
		public void Filters_ChangedOnly_ParentChangedByChild()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.IgnoreAssemblyAttributeChanges = true;
			DoCompareAndVerifyChange(filter, ChangeType.MembersChangedNonBreaking, ChangeType.None, "Attributes");
		}

		[TestMethod]
		public void Filters_PublicOnly_Removed()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.IncludeProtected = false;
            filter.IncludeInternal = false;
            filter.IncludePrivate = false;
            DoCompareAndVerifyRemoved(filter, "BitDiffer.Tests.Subject", "BasicClass", "_privateField");
		}

		[TestMethod]
		public void Filters_PublicOnly_StillPresent()
		{
			ComparisonFilter filter = new ComparisonFilter();
            filter.IncludeProtected = false;
            filter.IncludeInternal = false;
            filter.IncludePrivate = false;
            DoCompareAndVerifyStillPresent(filter, "BitDiffer.Tests.Subject", "BasicClass", "_publicField");
		}

        [TestMethod]
        public void Filters_PublicOrProt_Removed()
        {
            ComparisonFilter filter = new ComparisonFilter();
            filter.IncludeInternal = false;
            filter.IncludePrivate = false;
            DoCompareAndVerifyRemoved(filter, "BitDiffer.Tests.Subject", "BasicClass", "_privateField");
        }

        [TestMethod]
        public void Filters_PublicOrProt_StillPresent()
        {
            ComparisonFilter filter = new ComparisonFilter();
            filter.IncludeInternal = false;
            filter.IncludePrivate = false;
            DoCompareAndVerifyStillPresent(filter, "BitDiffer.Tests.Subject", "BasicClass", "_publicField");
        }

        [TestMethod]
        public void Filters_PublicOrProt_ProtStillPresent()
        {
            ComparisonFilter filter = new ComparisonFilter();
            filter.IncludeInternal = false;
            filter.IncludePrivate = false;
            DoCompareAndVerifyStillPresent(filter, "BitDiffer.Tests.Subject", "BasicClass", "_protectedField");
        }

        [TestMethod]
		public void Filters_MethodImpl()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.CompareMethodImplementations = false;
			DoCompareAndVerifyChange(filter, ChangeType.ImplementationChanged, ChangeType.None, "BitDiffer.Tests.Subject", "BasicClass", "MethodBodyChanges");
		}

		[TestMethod]
		public void Filters_IgnoreAssemAttributes()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.IgnoreAssemblyAttributeChanges = true;
			DoCompareAndVerifyChange(filter, ChangeType.DeclarationChangedNonBreaking, ChangeType.None, "Attributes", "System.Reflection.AssemblyDescriptionAttribute");
		}

		[TestMethod]
		public void Filters_IgnoreAssemAttributes_OtherAttersNotAffected()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.IgnoreAssemblyAttributeChanges = true;
			DoCompareAndVerifyChange(filter, ChangeType.DeclarationChangedNonBreaking, ChangeType.DeclarationChangedNonBreaking, "BitDiffer.Tests.Subject", "BasicClass", "MethodAttributeChanges", "System.ComponentModel.DescriptionAttribute");
		}

		[TestMethod]
		public void Filters_TextFilter_StillPresent()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.TextFilter = "_privateField";
			DoCompareAndVerifyStillPresent(filter, "BitDiffer.Tests.Subject", "BasicClass", "_privateField");
		}

		[TestMethod]
		public void Filters_TextFilter_Removed_Child()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.TextFilter = "Basic"; // This will keep basic class itself but remove it's children
			DoCompareAndVerifyRemoved(filter, "BitDiffer.Tests.Subject", "BasicClass", "_privateField");
		}

		[TestMethod]
		public void Filters_TextFilter_Removed_Sibling()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.TextFilter = "_privateField";
			DoCompareAndVerifyRemoved(filter, "BitDiffer.Tests.Subject", "BasicClass", "_publicField");
		}

		[TestMethod]
		public void Filters_TextFilter_NoMatches()
		{
			ComparisonFilter filter = new ComparisonFilter();
			filter.TextFilter = "bogus";

			AssemblyComparison ac = DoCompare(ComparisonFilter.Default);
			Assert.AreEqual(true, ac.Groups[0].Assemblies[1].FilterChildren<ICanAlign>().GetEnumerator().MoveNext());

			ac.Recompare(filter);
			Assert.AreEqual(false, ac.Groups[0].Assemblies[1].FilterChildren<ICanAlign>().GetEnumerator().MoveNext());
		}

		private static void DoCompareAndVerifyChange(ComparisonFilter filter, ChangeType withoutFilter, ChangeType withFilter, params string[] names)
		{
			AssemblyComparison ac = DoCompare(ComparisonFilter.Default);
			Assert.AreEqual(withoutFilter, FindInAssembly(ac, names).Change);

			ac.Recompare(filter);
			Assert.AreEqual(withFilter, FindInAssembly(ac, names).Change);
		}

		private static void DoCompareAndVerifyRemoved(ComparisonFilter filter, params string[] names)
		{
			AssemblyComparison ac = DoCompare(ComparisonFilter.Default);
			Assert.AreEqual(Status.Present, FindInAssembly(ac, names).Status);

			ac.Recompare(filter);
			Assert.IsNull(FindInAssembly(ac, names));
		}

		private static void DoCompareAndVerifyStillPresent(ComparisonFilter filter, params string[] names)
		{
			AssemblyComparison ac = DoCompare(ComparisonFilter.Default);
			Assert.AreEqual(Status.Present, FindInAssembly(ac, names).Status);

			ac.Recompare(filter);
			Assert.AreEqual(Status.Present, FindInAssembly(ac, names).Status);
		}

		private static AssemblyComparison DoCompare(ComparisonFilter filter)
		{
			AssemblyComparison ac = new AssemblyComparer().CompareAssemblyFiles(DiffConfig.Default, filter, Subjects.One, Subjects.Two);
			Assert.AreEqual(1, ac.Groups.Count);
			Assert.AreEqual(2, ac.Groups[0].Assemblies.Count);
			return ac;
		}

		private static ICanCompare FindInAssembly(AssemblyComparison ac, params string[] names)
		{
			ICanCompare current = ac.Groups[0].Assemblies[1];

			for (int i = 0; i < names.Length; i++)
			{
				ICanCompare child = (ICanCompare)FindChildByName(current, names[i]);

				if ((i < names.Length - 1) && (child == null))
				{
					Assert.Inconclusive("Did not find child named {0} in {1}.", names[i], current.Name);
				}

				current = child;
			}

			return current;
		}

		private static ICanAlign FindChildByName(ICanAlign current, string name)
		{
			IEnumerable<ICanAlign> children = current.FilterChildren<ICanAlign>();

			foreach (ICanAlign child in children)
			{
				if (child.Name == name)
				{
					return child;
				}
			}

			return null;
		}
	}
}
