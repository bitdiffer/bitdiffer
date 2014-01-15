using System;
using System.Collections.Generic;
using System.Text;
using BitDiffer.Common.Configuration;
using System.Xml.Serialization;

namespace BitDiffer.Common.Misc
{
	public enum ComparisonMode
	{
		CompareFiles,
		CompareDirectories
	}

	public class ComparisonSet
	{
		private List<string> _items = new List<string>();
        private List<string> _referenceDirectories = new List<string>();
        private DiffConfig _config = new DiffConfig();
		private ComparisonFilter _filter = new ComparisonFilter();

        [XmlIgnore]
        public string FileName
        {
            get; set;
        }

        [XmlElement]
        public ComparisonMode Mode
        {
            get; set;
        }

        [XmlElement]
        public bool RecurseSubdirectories
        {
            get; set;
        }

		[XmlArrayItem("Item")]
		public List<string> Items
		{
			get { return _items; }
			set { _items = value; }
		}

        [XmlArrayItem("Directory")]
        public List<string> ReferenceDirectories
        {
            get { return _referenceDirectories; }
            set { _referenceDirectories = value; }
        }

		[XmlElement]
		public DiffConfig Config
		{
			get { return _config; }
			set { _config = value; }
		}

		[XmlElement]
		public ComparisonFilter Filter
		{
			get { return _filter; }
			set { _filter = value; }
		}
	}
}
