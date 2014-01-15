using System;
using System.Collections.Generic;
using System.Text;

using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Common.Interfaces
{
	public interface ICanCompare : ICanAlign
	{
		ChangeType Change
		{
			get;
		}

		ChangeType PerformCompare(ICanCompare previous);
	}
}
