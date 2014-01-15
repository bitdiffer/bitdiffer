using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BitDiffer.Common.Configuration
{
	[Serializable]
	public class DiffConfig
	{
		private bool _useReflectionOnlyContext = true;
		private bool _tryResolveGACFirst = false;
		private bool _multithread = true;
        private string _referenceDirectories = null;
		private AppDomainIsolationLevel _isolationLevel = AppDomainIsolationLevel.AutoDetect;
		public static DiffConfig Default = new DiffConfig();

		public DiffConfig()
		{
		}

		[XmlAttribute]
		public AppDomainIsolationLevel IsolationLevel
		{
			get { return _isolationLevel; }
			set { _isolationLevel = value; }
		}

		[XmlAttribute]
		public bool UseReflectionOnlyContext
		{
			get { return _useReflectionOnlyContext; }
			set { _useReflectionOnlyContext = value; }
		}

		[XmlAttribute]
		public bool TryResolveGACFirst
		{
			get { return _tryResolveGACFirst; }
			set { _tryResolveGACFirst = value; }
		}

		[XmlAttribute]
		public bool Multithread
		{
			get { return _multithread; }
			set { _multithread = value; }
		}

        [XmlAttribute]
        public string ReferenceDirectories
        {
            get { return _referenceDirectories; }
            set { _referenceDirectories = value; }
        }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Config=");
			sb.Append(_useReflectionOnlyContext);
			sb.Append(";");
			sb.Append(_tryResolveGACFirst);
			sb.Append(";");
			sb.Append(_multithread);
			sb.Append(";");
			sb.Append(_isolationLevel.ToString());
			sb.Append(";");

            if (_referenceDirectories != null)
            {
                sb.Append(_referenceDirectories);
            }

			return sb.ToString();
		}
	}
}
