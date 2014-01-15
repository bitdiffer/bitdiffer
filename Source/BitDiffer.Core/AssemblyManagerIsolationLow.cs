using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Security.Policy;
using System.Diagnostics;

using BitDiffer.Common.Model;
using BitDiffer.Extractor;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using System.Threading;
using System.Collections;

namespace BitDiffer.Core
{
	[Serializable]
	public class AssemblyManagerIsolationLow : AssemblyManager
	{
		private DomainExtractorPair _pair;

		protected override DomainExtractorPair GetExtractor(string path)
		{
			if (_pair == null)
			{
				lock (typeof(AssemblyManager))
				{
					if (_pair == null)
					{
						_pair = GetExtractorInTempAppDomain(path);
					}
				}
			}

			return _pair;
		}

		internal override void AllExtractionsComplete()
		{
			if (_pair != null)
			{
				Log.Verbose("Unloading " + _pair.Domain.FriendlyName);
				AppDomain.Unload(_pair.Domain);
			}
		}
	}
}
