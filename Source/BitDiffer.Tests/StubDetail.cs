using System;
using System.Collections.Generic;
using System.Text;

using BitDiffer.Common.Model;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	internal class StubDetail : RootDetail, IHaveVisibility
	{
		private string _content;
		private Visibility _visibility;

		public StubDetail()
		{ 
		}

		internal StubDetail(string name, string content, Visibility visibility)
			: base(null, name)
		{
			_visibility = visibility;
			_content = content;
		}

		internal StubDetail(string name)
			: base(null, name)
		{
		}

		protected override bool SuppressBreakingChangesInChildren
		{
			get { return _visibility != Visibility.Public; }
		}

		public Visibility Visibility
		{
			get { return _visibility; }
			set { _visibility = value; }
		}

		protected override ChangeType CompareInstance(ICanCompare previous, bool suppressBreakingChanges)
		{
			ChangeType change = base.CompareInstance(previous, suppressBreakingChanges);

			StubDetail other = (StubDetail)previous;

			change |= VisibilityUtil.GetVisibilityChange(other._visibility, _visibility, suppressBreakingChanges);

			if (_content != other._content)
			{
				change |= ChangeType.ContentChanged;
			}

			return change;
		}
	}
}
