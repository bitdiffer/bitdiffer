using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Interfaces
{
	public interface IHandleProgress
	{
		void SetMaxRange(int max);

		void UpdateProgress(ProgressStatus progress);

		bool CancelRequested { get; }
	}
}
