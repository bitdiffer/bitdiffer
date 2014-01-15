using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.IO;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Common.Misc
{
	public enum AssemblyComparisonXmlWriteMode
	{
		Raw,
		Normal
	};

	[XmlRoot("AssemblyComparison")]
	public class AssemblyComparison : IXmlSerializable
	{
		private AssemblyComparisonXmlWriteMode _xmlWriteMode;
		private List<AssemblyGroup> _groups = new List<AssemblyGroup>();

		public List<AssemblyGroup> Groups
		{
			get { return _groups; }
			set { _groups = value; }
		}

		public void Recompare(ComparisonFilter filter)
		{
			foreach (AssemblyGroup group in _groups)
			{
				group.PerformCompare(filter);
			}
		}

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Groups");
			foreach (AssemblyGroup group in _groups)
			{
				if (_xmlWriteMode == AssemblyComparisonXmlWriteMode.Raw)
				{
					group.SerializeWriteRawXml(writer);
				}
				else if (_xmlWriteMode == AssemblyComparisonXmlWriteMode.Normal)
				{
					group.SerializeWriteXml(writer);
				}
			}
			writer.WriteEndElement();
		}

		public void WriteXmlReport(string fileName, AssemblyComparisonXmlWriteMode mode)
		{
			Log.Info("Writing XML {0} report to {1}", mode.ToString().ToLower(), fileName);

			using (FileStream fs = File.Open(fileName, FileMode.Create))
			{
				_xmlWriteMode = mode;
				XmlSerializer xs = new XmlSerializer(typeof(AssemblyComparison));
				xs.Serialize(fs, this);
			}
		}

		public void WriteHtmlReport(string fileName)
		{
			Log.Info("Writing HTML change report to {0}", fileName);

			using (FileStream fs = File.Open(fileName, FileMode.Create))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					HtmlUtility.WriteHtmlStart(sw);

					foreach (AssemblyGroup group in _groups)
					{
						group.WriteHtmlReport(sw);
					}

					HtmlUtility.WriteHtmlEnd(sw);
				}
			}
		}

		public void WriteReport(string fileName, AssemblyComparisonXmlWriteMode assemblyComparisonXmlWriteMode)
		{
			if (Path.GetExtension(fileName).ToLower() == ".html")
			{
				WriteHtmlReport(fileName);
			}
			else
			{
				WriteXmlReport(fileName, AssemblyComparisonXmlWriteMode.Normal);
			}
		}
	}
}
