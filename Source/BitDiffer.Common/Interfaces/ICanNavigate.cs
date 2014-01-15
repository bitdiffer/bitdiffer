using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Interfaces
{
	public interface ICanNavigate
	{
		ICanNavigate NavigateForward
		{
			get;
			set;
		}

		ICanNavigate NavigateBackward
		{
			get;
			set;
		}

		ICanNavigate NavigateForwardTo(int index);
	}
}
