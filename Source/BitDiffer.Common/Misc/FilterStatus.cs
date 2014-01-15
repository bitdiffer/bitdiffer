using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Misc
{
	public enum FilterStatus
	{
		/// <summary>
		/// Exclude and do not allow changing to include to include children
		/// </summary>
		ExcludeBlock,

		/// <summary>
		/// Conflicts with the filter
		/// </summary>
		Exclude,

		/// <summary>
		/// Excluded by itself, but include in filter to include children
		/// </summary>
		ExcludedButIncludeForChildren,

		/// <summary>
		/// Neither matches or conflicts with the filter
		/// </summary>
		DontCare,

		/// <summary>
		/// Matches the filter
		/// </summary>
		Include
	};
}
