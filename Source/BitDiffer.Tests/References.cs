using BitDiffer.Common.Misc;
using BitDiffer.Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitDiffer.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class References : TestBase
	{
		[TestMethod]
		public void Reference_Extraction()
		{
			ReferenceDetail ri = ExtractReference(Subjects.One, "System.Data");
			Assert.AreEqual(Status.Present, ri.Status);
			Assert.AreEqual("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", ri.ToString());
		}

		[TestMethod]
		public void Reference_NoChange()
		{
			ReferenceDetail ri1 = ExtractReference(Subjects.One, "System.Data");
			ReferenceDetail ri2 = ExtractReference(Subjects.Two, "System.Data");

			Assert.AreEqual(ChangeType.None, ri1.PerformCompare(ri2));
		}

		[TestMethod]
		public void Reference_Changed()
		{
			ReferenceDetail ri1 = ExtractReference(Subjects.One, "BitDiffer.Tests.Reference");
			ReferenceDetail ri2 = ExtractReference(Subjects.Two, "BitDiffer.Tests.Reference");

			Assert.AreEqual(ChangeType.ValueChangedNonBreaking, ri1.PerformCompare(ri2));
		}
	}
}
