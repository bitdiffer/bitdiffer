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
	public class AssemblyManagerIsolationMedium : AssemblyManager
	{
		private SortedList<string, DomainExtractorPair> _pairs = new SortedList<string, DomainExtractorPair>();

		protected override DomainExtractorPair GetExtractor(string path)
		{
			string dir = Path.GetDirectoryName(path);

			if (!(_pairs.ContainsKey(dir)))
			{
				lock (typeof(AssemblyManagerIsolationMedium))
				{
					if (!(_pairs.ContainsKey(dir)))
					{
						_pairs.Add(dir, GetExtractorInTempAppDomain(path));
					}
				}
			}

			return _pairs[dir];
		}

		internal override void AllExtractionsComplete()
		{
			foreach (DomainExtractorPair pair in _pairs.Values)
			{
				Log.Verbose("Unloading " + pair.Domain.FriendlyName);
				AppDomain.Unload(pair.Domain);
			}
		}
	}
}
