using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Misc
{
	public class ProgressStatus
	{
		private string _status;
		private bool _step;

		public ProgressStatus(string status, bool step)
		{
			_status = status;
			_step = step;
		}

		public string Status
		{
			get { return _status; }
		}

		public bool Step
		{
			get { return _step; }
		}
	}
}
