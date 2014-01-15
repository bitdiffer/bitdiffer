using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace BitDiffer.Common.TraceListeners
{
	[Serializable]
	public class TraceEventArgs : EventArgs
	{
		private TraceLevel _level;
		private string _message;

		public TraceEventArgs(TraceLevel level, string message)
		{
			_level = level;
			_message = message;
		}

		public TraceLevel Level
		{
			get { return _level; }
			set { _level = value; }
		}

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}
	}
}
