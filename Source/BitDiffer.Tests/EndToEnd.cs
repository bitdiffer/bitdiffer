using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

using BitDiffer.Core;
using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class EndToEnd : TestBase
	{
		[TestMethod]
		public void EndToEnd_AttributeAdded()
		{
			List<AssemblyDetail> list = DoCompare();

			for (int i = 0; i < list[0].Children.Count; i++)
			{
				if (list[0].Children[i].GetType() == typeof(AttributesDetail))
				{
					for (int j = 0; j < list[0].Children[i].Children.Count; j++)
					{
						if (list[0].Children[i].Children[j].Status == Status.Missing)
						{
							Assert.AreEqual(ChangeType.Added, ((ICanCompare)list[1].Children[i].Children[j]).Change);
							return;
						}
					}
				}
			}

			Assert.Inconclusive("Did not find an added attribute");
		}

		[TestMethod]
		public void EndToEnd_AttributeRemoved()
		{
			List<AssemblyDetail> list = DoCompare();

			for (int i = 0; i < list[1].Children.Count; i++)
			{
				if (list[1].Children[i].GetType() == typeof(AttributesDetail))
				{
					for (int j = 0; j < list[1].Children[i].Children.Count; j++)
					{
						if (list[1].Children[i].Children[j].Status == Status.Missing)
						{
							Assert.AreEqual(ChangeType.RemovedNonBreaking, ((ICanCompare)list[1].Children[i].Children[j]).Change);
							return;
						}
					}
				}
			}

			Assert.Inconclusive("Did not find a removed attribute");
		}

		[TestMethod]
		public void EndToEnd_ChildrenOfAddedItem()
		{
			PropertyDetail pd1 = ExtractProperty(Subjects.One, "BasicClass", "PropertyAdded");
			PropertyDetail pd2 = ExtractProperty(Subjects.Two, "BasicClass", "PropertyAdded");

			Align(pd1, pd2);

			pd2.PerformCompare(pd1);

			Assert.AreEqual(ChangeType.Added, pd2.Change);

			foreach (ICanCompare child in pd2.FilterChildren<ICanCompare>())
			{
				Assert.AreEqual(ChangeType.Added, child.Change);
			}
		}

		private static List<AssemblyDetail> DoCompare()
		{
			return DoCompare(DiffConfig.Default);
		}

		private static List<AssemblyDetail> DoCompare(DiffConfig config)
		{
			AssemblyComparison ac = new AssemblyComparer().CompareAssemblyFiles(config, ComparisonFilter.Default, Subjects.One, Subjects.Two);
			Assert.AreEqual(1, ac.Groups.Count);
			return ac.Groups[0].Assemblies;
		}
	}
}