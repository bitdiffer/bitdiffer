using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Diagnostics;

using BitDiffer.Core;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Exceptions;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class ListOperationsTests : TestBase
	{
		[TestMethod]
		public void List_Align_SameLen()
		{
			List<ICanAlign> l1 = new List<ICanAlign>();
			List<ICanAlign> l2 = new List<ICanAlign>();

			l1.Add(new RootDetail("X1"));
			l1.Add(new RootDetail("X2"));
			l1.Add(new RootDetail("X3"));

			l2.Add(new RootDetail("X3"));
			l2.Add(new RootDetail("X1"));
			l2.Add(new RootDetail("X2"));

			ListOperations.AlignListsNoParent(l1, l2);
			ListOperations.CheckAlignment(l1, l2);
		}

		[TestMethod]
		public void List_Align_OneItemMissing1()
		{
			List<ICanAlign> l1 = new List<ICanAlign>();
			List<ICanAlign> l2 = new List<ICanAlign>();

			l1.Add(new RootDetail("X1"));
			l1.Add(new RootDetail("X3"));

			l2.Add(new RootDetail("X3"));
			l2.Add(new RootDetail("X1"));
			l2.Add(new RootDetail("X2"));

			ListOperations.AlignListsNoParent(l1, l2);
			ListOperations.CheckAlignment(l1, l2);

			Assert.AreEqual("X1", l1[0].Name);
			Assert.AreEqual(Status.Missing, l1[1].Status);
		}

		[TestMethod]
		public void List_Align_OneItemMissing2()
		{
			List<ICanAlign> l1 = new List<ICanAlign>();
			List<ICanAlign> l2 = new List<ICanAlign>();

			l1.Add(new RootDetail("X3"));
			l1.Add(new RootDetail("X1"));
			l1.Add(new RootDetail("X2"));

			l2.Add(new RootDetail("X3"));
			l2.Add(new RootDetail("X2"));

			ListOperations.AlignListsNoParent(l1, l2);
			ListOperations.CheckAlignment(l1, l2);

			Assert.AreEqual("X1", l1[0].Name);
			Assert.AreEqual(Status.Missing, l2[0].Status);
		}

		[TestMethod]
		public void List_Align_OneEmpty()
		{
			List<ICanAlign> l1 = new List<ICanAlign>();
			List<ICanAlign> l2 = new List<ICanAlign>();

			l2.Add(new RootDetail("X3"));
			l2.Add(new RootDetail("X2"));
			l2.Add(new RootDetail("X1"));

			ListOperations.AlignListsNoParent(l1, l2);
			ListOperations.CheckAlignment(l1, l2);

			Assert.AreEqual("X1", l2[0].Name);
			Assert.AreEqual(Status.Missing, l1[0].Status);
		}

		[TestMethod]
		public void List_Align_AllMismatch()
		{
			List<ICanAlign> l1 = new List<ICanAlign>();
			List<ICanAlign> l2 = new List<ICanAlign>();

			l1.Add(new RootDetail("Y3"));
			l1.Add(new RootDetail("Y2"));
			l1.Add(new RootDetail("Y1"));

			l2.Add(new RootDetail("X3"));
			l2.Add(new RootDetail("X2"));
			l2.Add(new RootDetail("X1"));

			ListOperations.AlignListsNoParent(l1, l2);
			ListOperations.CheckAlignment(l1, l2);

			Assert.AreEqual("X1", l2[0].Name);
			Assert.AreEqual(Status.Missing, l2[5].Status);
			Assert.AreEqual(Status.Missing, l1[0].Status);
		}

		[TestMethod]
		public void List_Align_ThreeLists_Overlap()
		{
			List<ICanAlign> l1 = new List<ICanAlign>();
			List<ICanAlign> l2 = new List<ICanAlign>();
			List<ICanAlign> l3 = new List<ICanAlign>();

			l1.Add(new RootDetail("X1"));
			l1.Add(new RootDetail("X2"));

			l2.Add(new RootDetail("X3"));
			l2.Add(new RootDetail("X2"));

			l3.Add(new RootDetail("X3"));
			l3.Add(new RootDetail("X4"));

			ListOperations.AlignListsNoParent(l1, l2, l3);
			ListOperations.CheckAlignment(l1, l2);
			ListOperations.CheckAlignment(l3, l2);
			ListOperations.CheckAlignment(l1, l3);

			Assert.AreEqual("X1", l1[0].Name);
			Assert.AreEqual(Status.Missing, l2[0].Status);
			Assert.AreEqual(Status.Missing, l3[0].Status);

			Assert.AreEqual(Status.Missing, l1[3].Status);
			Assert.AreEqual(Status.Missing, l2[3].Status);
			Assert.AreEqual("X4", l3[3].Name);
		}

		[TestMethod]
		public void List_Align_ThreeLists_AllMismatch()
		{
			List<ICanAlign> l1 = new List<ICanAlign>();
			List<ICanAlign> l2 = new List<ICanAlign>();
			List<ICanAlign> l3 = new List<ICanAlign>();

			l1.Add(new RootDetail("X3"));
			l1.Add(new RootDetail("X4"));

			l2.Add(new RootDetail("X2"));
			l2.Add(new RootDetail("X5"));

			l3.Add(new RootDetail("X1"));
			l3.Add(new RootDetail("X6"));

			ListOperations.AlignListsNoParent(l1, l2, l3);
			ListOperations.CheckAlignment(l1, l2);
			ListOperations.CheckAlignment(l3, l2);
			ListOperations.CheckAlignment(l1, l3);

			Assert.AreEqual(Status.Missing, l1[0].Status);
			Assert.AreEqual(Status.Missing, l2[0].Status);
			Assert.AreEqual("X1", l3[0].Name);

			Assert.AreEqual("X4", l1[3].Name);
			Assert.AreEqual(Status.Missing, l2[3].Status);
			Assert.AreEqual(Status.Missing, l3[3].Status);
		}

		[TestMethod]
		[ExpectedException(typeof(UnalignedListException))]
		public void List_Align_Classes_Fail()
		{
			ClassDetail d1 = ExtractClass(Subjects.One, "BasicClass");
			ClassDetail d2 = ExtractClass(Subjects.Two, "BasicClass");

			d1.PerformCompare(d2);
		}

		[TestMethod]
		public void List_Align_Classes_OK()
		{
			ClassDetail d1 = ExtractClass(Subjects.One, "BasicClass");
			ClassDetail d2 = ExtractClass(Subjects.Two, "BasicClass");

			Align(d1, d2);

			d1.PerformCompare(d2);
		}

		[TestMethod]
		public void List_Align_Classes_Navigate()
		{
			StubDetail d1a = new StubDetail("A1");
			StubDetail d1b = new StubDetail("A2");
			StubDetail d2 = new StubDetail("A1");
			StubDetail d3 = new StubDetail("A2");

			List<ICanAlign> list1 = new List<ICanAlign>();
			List<ICanAlign> list2 = new List<ICanAlign>();
			List<ICanAlign> list3 = new List<ICanAlign>();

			list1.Add(d1a);
			list1.Add(d1b);
			list2.Add(d2);
			list3.Add(d3);

			ListOperations.AlignListsNoParent(list1, list2, list3);

			Assert.AreEqual(d2, d1a.NavigateForward);
			Assert.AreEqual(Status.Missing, ((ICanAlign)d1b.NavigateForward).Status);
			Assert.AreEqual(d3, d1b.NavigateForward.NavigateForward);
			Assert.AreEqual(d1a, d2.NavigateBackward);
			Assert.AreEqual(Status.Missing, ((ICanAlign)d3.NavigateBackward).Status);
		}
	}
}
