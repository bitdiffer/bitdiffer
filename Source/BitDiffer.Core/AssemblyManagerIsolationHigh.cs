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
	public class AssemblyManagerIsolationHigh : AssemblyManager
	{
		protected override DomainExtractorPair GetExtractor(string path)
		{
			return GetExtractorInTempAppDomain(path);
		}

		protected override void OneExtractionComplete(DomainExtractorPair pair)
		{
			Log.Verbose("Unloading " + pair.Domain.FriendlyName);
			AppDomain.Unload(pair.Domain);
		}
	}
}
