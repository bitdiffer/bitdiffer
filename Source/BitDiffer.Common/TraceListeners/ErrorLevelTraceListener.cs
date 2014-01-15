using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace BitDiffer.Common.TraceListeners
{
	public class ErrorLevelTraceListener : TraceListener
	{
		private TraceLevel _highestLoggedLevel = TraceLevel.Verbose;

		public TraceLevel HighestLoggedLevel
		{
			get { return _highestLoggedLevel; }
		}

		public void Reset()
		{
			_highestLoggedLevel = TraceLevel.Verbose;
		}

		public override void Write(string message, string category)
		{
			CheckLevel(category);
		}

		public override void WriteLine(string message, string category)
		{
			CheckLevel(category);
		}

		public override void Write(string message)
		{
			Write(message, "Information");
		}

		public override void WriteLine(string message)
		{
			Write(message, "Information");
		}

		private void CheckLevel(string category)
		{
			TraceLevel level = (TraceLevel)Enum.Parse(typeof(TraceLevel), category);

			if (level < _highestLoggedLevel)
			{
				_highestLoggedLevel = level;
			}
		}
	}
}
