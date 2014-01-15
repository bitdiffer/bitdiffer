using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Utility
{
	public class Stopwatch : IDisposable
	{
		private string _operation;
		private decimal _start;

		public Stopwatch(string operation)
		{
			_start = Environment.TickCount;
			_operation = operation;
		}

		public void Dispose()
		{
			decimal end = Environment.TickCount;

			Log.Info(_operation + " completed in " + (end - _start).ToString());
		}
	}
}
