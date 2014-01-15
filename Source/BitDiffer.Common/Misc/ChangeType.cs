using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Misc
{
	[Flags]
	public enum ChangeType
	{
		None = 0,
		Added = 1,
		ImplementationChanged = 2,
		ContentChanged = 4,
		ValueChangedBreaking = 8,
		ValueChangedNonBreaking = 16,
		AttributesChanged = 32,
		RemovedBreaking = 64,
		RemovedNonBreaking = 128,
		VisibilityChangedBreaking = 256,
		VisibilityChangedNonBreaking = 512,
		DeclarationChangedBreaking = 1024,
		DeclarationChangedNonBreaking = 2048,
		MembersChangedBreaking = 4096,
		MembersChangedNonBreaking = 8192
	};
}
