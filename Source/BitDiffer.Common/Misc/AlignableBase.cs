using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Collections;

using BitDiffer.Common.Interfaces;

namespace BitDiffer.Common.Misc
{
	[Serializable]
	public abstract class AlignableBase : ICanAlign
	{
		protected string _name;
		protected string _alignmentIdentifier;
		protected ICanAlign _parent;
		protected ICanNavigate _navigateForward;
		protected ICanNavigate _navigateBackward;
		protected Status _status = Status.Present;
		protected FilterStatus _filterStatus = FilterStatus.DontCare;
		protected List<ICanAlign> _children = new List<ICanAlign>();
		private AlignMatchStatus _alignMatchStatus;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public string AlignmentIdentifier 
		{ 
			get { return _alignmentIdentifier ?? _name; }
			set { _alignmentIdentifier = value; } 
		}

		public AlignMatchStatus AlignMatchStatus
		{
			get { return _alignMatchStatus; }
			set { _alignMatchStatus = value; }
		}

		public string FullName
		{
			get
			{
				if (_parent == null || this.FullNameRoot)
				{
					return _name;
				}

				return ((AlignableBase)_parent).FullName + "." + _name;
			}
		}

		protected virtual bool FullNameRoot
		{
			get { return false; }
		}

		[DefaultValue(Status.Present)]
		public Status Status
		{
			get { return _status; }
			set { _status = value; }
		}

		public ICanNavigate NavigateForward
		{
			get { return _navigateForward; }
			set { _navigateForward = value; }
		}

		public ICanNavigate NavigateBackward
		{
			get { return _navigateBackward; }
			set { _navigateBackward = value; }
		}

		public ICanAlign Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		public IList<ICanAlign> Children
		{
			get { return _children; }
		}

		public ICanNavigate NavigateForwardTo(int index)
		{
			ICanNavigate target = this;

			while (index > 0)
			{
				target = target.NavigateForward;
				index--;
			}

			return target;
		}

		public FilterStatus FilterStatus
		{
			get { return _filterStatus; }
		}

		public FilterStatus GetStrongestFilterStatus()
		{
			if (_navigateBackward != null)
			{
				throw new InvalidOperationException();
			}

			if (_navigateForward == null)
			{
				return _filterStatus;
			}

			FilterStatus strongestStatus = FilterStatus.ExcludeBlock;
			ICanAlign next = (ICanAlign)_navigateForward;

			while (next != null)
			{
				if (next.FilterStatus > strongestStatus)
				{
					strongestStatus = next.FilterStatus;
				}

				next = (ICanAlign)next.NavigateForward;
			}

			return strongestStatus;
		}

		public virtual IEnumerable<T> FilterChildren<T>() where T : ICanAlign
		{
			foreach (ICanAlign child in _children)
			{
				if (typeof(T).IsAssignableFrom(child.GetType()))
				{
					if (child.FilterStatus > FilterStatus.Exclude)
					{
						yield return (T)child;
					}
				}
			}
		}

		public virtual IEnumerable<T> FilterChildrenInAll<T>() where T : ICanAlign
		{
			foreach (ICanAlign child in _children)
			{
				if (typeof(T).IsAssignableFrom(child.GetType()))
				{
					if (child.GetStrongestFilterStatus() > FilterStatus.Exclude)
					{
						yield return (T)child;
					}
				}
			}
		}
	}
}
