using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BitDiffer.Common.Configuration
{
	public class ComparisonFilter
	{
	    private bool _includePublic = true;
        private bool _includePrivate = true;
        private bool _includeProtected = true;
        private bool _includeInternal = true;
        private bool _changedItemsOnly;
		private bool _ignoreAssemblyAttributeChanges;
		private bool _compareMethodImplementations = true;
		private string _textFilter;
        private bool _ignoreAssemblyFileVersionAttributeChanges = false;
        public static ComparisonFilter Default = new ComparisonFilter();

		[XmlAttribute("PublicTypesOnly")]
		public bool PublicTypesOnly_Obsolete
		{
			get { return false; }
            set
            {
                if (value)
                {
                    _includePublic = true;
                    _includePrivate = false;
                    _includeProtected = false;
                    _includeInternal = false;
                }
            }
		}

        [XmlAttribute]
        public bool IncludePublic
	    {
	        get { return _includePublic;}
            set { _includePublic = value; }
	    }

        [XmlAttribute]
        public bool IncludeProtected
        {
            get { return _includeProtected; }
            set { _includeProtected = value; }
        }

        [XmlAttribute]
        public bool IncludePrivate
        {
            get { return _includePrivate; }
            set { _includePrivate = value; }
        }

        [XmlAttribute]
        public bool IncludeInternal
        {
            get { return _includeInternal; }
            set { _includeInternal = value; }
        }

        [XmlAttribute]
		public bool ChangedItemsOnly
		{
			get { return _changedItemsOnly; }
			set { _changedItemsOnly = value; }
		}

		[XmlAttribute]
		public bool IgnoreAssemblyAttributeChanges
		{
			get { return _ignoreAssemblyAttributeChanges; }
			set { _ignoreAssemblyAttributeChanges = value; }
		}

		[XmlAttribute]
		public bool CompareMethodImplementations
		{
			get { return _compareMethodImplementations; }
			set { _compareMethodImplementations = value; }
		}

		[XmlAttribute]
		public string TextFilter
		{
			get { return _textFilter; }
			set { _textFilter = value; }
		}

        [XmlAttribute]
        public bool IgnoreAssemblyFileVersionAttributeChanges
        {
            get { return _ignoreAssemblyFileVersionAttributeChanges; }
            set { _ignoreAssemblyFileVersionAttributeChanges = value; }
        }
    }
}
