using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Tests.Subject
{
	public class UseReferences
	{
		public Reference.ReferencedClass1 UseAllReferences()
		{
			System.Data.DataTable dt = new System.Data.DataTable();
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

			Console.WriteLine(Reference.ReferencedClass1.UseMe().ToString());
			return Reference.ReferencedClass1.UseMe();
		}
	}
}
