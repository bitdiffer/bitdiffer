using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Configuration
{
	public enum AppDomainIsolationLevel
	{
		// Load everything in current appdomain
		None,

		// Load everything in one temporary appdomain
		Low,

		// Load each directory into a temporary appdomain
		Medium,

		// Load each file into a temporary appdomain
		High,

		// Auto detect
		AutoDetect
	};
}
