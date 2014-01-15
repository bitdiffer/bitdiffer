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
	public class Events : TestBase
	{
		[TestMethod]
		public void Event_Extraction()
		{
			EventDetail ei = ExtractEvent(Subjects.One, "BasicClass", "MyEventToFire");
			Assert.AreEqual(Status.Present, ei.Status);
			Assert.AreEqual("public event System.EventHandler MyEventToFire", ei.ToString());
		}

		[TestMethod]
		public void Event_WithAttributes()
		{
			EventDetail ei = ExtractEvent(Subjects.One, "BasicClass", "MyEventWithAttribute");
			Assert.AreEqual(Status.Present, ei.Status);
			CheckForAttribute(ei);
		}

		[TestMethod]
		public void Event_Change_NoChange()
		{
			AssertChange("BasicClass", "MyEventToFire", ChangeType.None);
		}

		[TestMethod]
		public void Event_Change_NotBreaking()
		{
			AssertChange("BasicClass", "InternalEventDeclChange", ChangeType.DeclarationChangedNonBreaking);
		}

		[TestMethod]
		public void Event_Change_Breaking()
		{
			AssertChange("BasicClass", "PublicEventDeclChange", ChangeType.DeclarationChangedBreaking);
		}

		private void AssertChange(string from, string name, ChangeType change)
		{
			RootDetail r1 = ExtractEvent(Subjects.One, from, name);
			RootDetail r2 = ExtractEvent(Subjects.Two, from, name);

			Assert.AreEqual(change, r1.PerformCompare(r2));
		}
	}
}
