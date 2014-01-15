using System;
using System.Collections.Generic;
using System.Text;

using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Interfaces;

namespace BitDiffer.Common.Utility
{
	public static class ChangeTypeUtil
	{
		private static ChangeType breakingChangesMask = ChangeType.DeclarationChangedBreaking | ChangeType.MembersChangedBreaking | ChangeType.RemovedBreaking | ChangeType.VisibilityChangedBreaking | ChangeType.ValueChangedBreaking;
		private static ChangeType nonBreakingChangesMask = ~breakingChangesMask;
		private static ChangeType childChangesMask = ChangeType.AttributesChanged | ChangeType.MembersChangedBreaking | ChangeType.MembersChangedNonBreaking | ChangeType.ContentChanged;

		public static bool IsAddRemove(ChangeType change)
		{
			if (change == ChangeType.Added || change == ChangeType.RemovedBreaking || change == ChangeType.RemovedNonBreaking)
			{
				return true;
			}

			return false;
		}

		public static bool HasBreaking(ChangeType change)
		{
			return (change != ChangeType.None) && ((change & breakingChangesMask) != 0);
		}

		public static bool HasNonBreaking(ChangeType change)
		{
			return (change != ChangeType.None) && ((change & nonBreakingChangesMask) != 0);
		}

		public static bool IsChildChangeOnly(ChangeType change)
		{
			return (change != ChangeType.None) && ((change & childChangesMask) == change);
		}
	}
}
