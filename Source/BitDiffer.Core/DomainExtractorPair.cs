using System;
using System.Collections.Generic;
using System.Text;
using BitDiffer.Extractor;

namespace BitDiffer.Core
{
	public class DomainExtractorPair
	{
		AppDomain _domain;
		AssemblyExtractor _extractor;

		public DomainExtractorPair(AppDomain domain, AssemblyExtractor extractor)
		{
			_domain = domain;
			_extractor = extractor;
		}

		public AppDomain Domain
		{
			get { return _domain; }
		}

		public AssemblyExtractor Extractor
		{
			get { return _extractor; }
		}
	}
}
