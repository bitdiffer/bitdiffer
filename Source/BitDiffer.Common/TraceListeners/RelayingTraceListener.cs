using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace BitDiffer.Common.TraceListeners
{
	public class RelayingTraceListener : TraceListener
	{
		public event EventHandler<TraceEventArgs> Message;

		public override void Write(string message, string category)
		{
			if (Message != null)
			{
				Message(this, new TraceEventArgs((TraceLevel)Enum.Parse(typeof(TraceLevel), category), message));
			}
		}

		public override void WriteLine(string message, string category)
		{
			Write(message, category);
		}

		public override void Write(string message)
		{
			Write(message, "Information");
		}

		public override void WriteLine(string message)
		{
			Write(message, "Information");
		}
	}
}
