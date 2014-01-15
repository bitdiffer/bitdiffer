using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace BitDiffer.Common.Utility
{
	public static class Log
	{
		private static TraceSwitch _traceSwitch = new TraceSwitch("GlobalTraceSwitch", "Global Trace Switch");

		public static void Verbose(string format, params object[] args)
		{
			TraceLine(TraceLevel.Verbose, format, args);
		}

		public static void Info(string format, params object[] args)
		{
			TraceLine(TraceLevel.Info, format, args);
		}

		public static void Warn(string format, params object[] args)
		{
			TraceLine(TraceLevel.Warning, format, args);
		}

		public static void Error(string format, params object[] args)
		{
			TraceLine(TraceLevel.Error, format, args);
		}

		private static void TraceLine(TraceLevel level, string format, object[] args)
		{
			if ((args == null) || (args.Length == 0))
			{
				format = format.Replace("{", "{{");
				format = format.Replace("}", "}}");
			}

			string message = /*DateTime.Now.ToString("HH:mm:ss") + " : " +*/ string.Format(format, args);

			Trace.WriteLineIf(DoTrace(level), message, level.ToString());
		}

		private static bool DoTrace(TraceLevel level)
		{
			return ((level == TraceLevel.Verbose) && (_traceSwitch.TraceVerbose)) ||
							((level == TraceLevel.Info) && (_traceSwitch.TraceInfo)) ||
							((level == TraceLevel.Warning) && (_traceSwitch.TraceWarning)) ||
							((level == TraceLevel.Error) && (_traceSwitch.TraceError));
		}
	}
}
