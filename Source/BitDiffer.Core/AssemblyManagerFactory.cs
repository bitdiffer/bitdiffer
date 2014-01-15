using System;
using System.Collections.Generic;
using System.Text;

using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Core
{
	public static class AssemblyManagerFactory
	{
		public static AssemblyManager Create(AppDomainIsolationLevel level)
		{
			switch (level)
			{
				case AppDomainIsolationLevel.None: return new AssemblyManagerIsolationNone();
				case AppDomainIsolationLevel.Low: return new AssemblyManagerIsolationLow();
				case AppDomainIsolationLevel.Medium: return new AssemblyManagerIsolationMedium();
				case AppDomainIsolationLevel.High: return new AssemblyManagerIsolationHigh();
				default: throw new ArgumentException("level");
			}
		}
	}
}
