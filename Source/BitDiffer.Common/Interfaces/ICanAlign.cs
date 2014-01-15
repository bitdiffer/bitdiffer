using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Interfaces
{
	public interface ICanAlign : ICanNavigate
	{
		string AlignmentIdentifier { get; set; }
		string Name { get; set; }
		Status Status { get; set; }
		ICanAlign Parent { get; set; }
		AlignMatchStatus AlignMatchStatus { get; set; }

		IList<ICanAlign> Children { get; }

		FilterStatus FilterStatus { get; }
		FilterStatus GetStrongestFilterStatus();

		IEnumerable<T> FilterChildren<T>() where T : ICanAlign;
		IEnumerable<T> FilterChildrenInAll<T>() where T : ICanAlign;
	}
}
