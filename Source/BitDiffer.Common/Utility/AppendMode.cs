using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Utility
{
	[Flags]
	public enum AppendMode
	{
		Text = 1,
		Html = 2,
		Both = AppendMode.Text | AppendMode.Html
	};
}
