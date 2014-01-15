using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections;
using System.Xml;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;
using System.IO;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class RootDetail : AlignableBase, ICanCompare, IComparable<RootDetail>, IComparable
	{
		protected ChangeType _changeThisInstance = ChangeType.None;
		protected ChangeType _changeAllChildren = ChangeType.None;

		public RootDetail()
		{
		}

		public RootDetail(string name)
		{
			_name = name;
		}

		public RootDetail(RootDetail parent, string name)
		{
			_parent = parent;
			_name = name;
		}

		public ChangeType Change
		{
			get
			{
				if (ChangeTypeUtil.IsAddRemove(_changeThisInstance))
				{
					return _changeThisInstance;
				}

				return _changeThisInstance | _changeAllChildren;
			}
		}

		public AssemblyDetail DeclaringAssembly
		{
			get
			{
				ICanAlign check = this;

				while (check.Parent != null)
				{
					check = check.Parent;
				}

				return (AssemblyDetail)check;
			}
		}

		public override string ToString()
		{
			return _name;
		}

		public override bool Equals(object obj)
		{
			RootDetail other = obj as RootDetail;

			if (other == null)
			{
				return false;
			}

			return _name == other._name;
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

		public static bool operator ==(RootDetail r1, RootDetail r2)
		{
			if (object.ReferenceEquals(r1, null) && object.ReferenceEquals(r2, null))
			{
				return true;
			}
			else if (object.ReferenceEquals(r1, null) || object.ReferenceEquals(r2, null))
			{
				return false;
			}
			else
			{
				return r1._name == r2._name;
			}
		}

		public static bool operator !=(RootDetail r1, RootDetail r2)
		{
			return (!(r1 == r2));
		}

		public ChangeType PerformCompare(ICanCompare from)
		{
			_changeThisInstance = PerformCompareInternal(from, false);

			CalcInheritedChanges();

			return _changeThisInstance | _changeAllChildren;
		}

		protected virtual bool SuppressBreakingChangesInChildren
		{
			get { return true; }
		}

		protected ChangeType PerformCompareInternal(ICanCompare from, bool suppressBreakingChanges)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}

			if (from.Status == Status.Missing && _status == Status.Missing)
			{
				_changeThisInstance = ChangeType.None;
			}
			else if (from.Status == Status.Missing)
			{
				_changeThisInstance = ChangeType.Added;

				ForceChangeToAllDescendants();
			}
			else if (_status == Status.Missing)
			{
				if (suppressBreakingChanges || (((RootDetail)from).SuppressBreakingChangesInChildren))
				{
					_changeThisInstance = ChangeType.RemovedNonBreaking;
				}
				else
				{
					Visibility visibility = VisibilityUtil.GetMostVisible(from);

					if (visibility == Visibility.Public)
					{
						_changeThisInstance = ChangeType.RemovedBreaking;
					}
					else
					{
						_changeThisInstance = ChangeType.RemovedNonBreaking;
					}
				}

				ForceChangeToAllDescendants();
			}
			else
			{
				if (from.GetType() != GetType())
				{
					throw new InvalidOperationException("Cannot calculate changes between different types");
				}

				if (string.Compare(from.AlignmentIdentifier, this.AlignmentIdentifier) != 0)
				{
					throw new InvalidOperationException("Cannot calculate changes between objects with different identifiers. The identifier correlates the same objects in different lists.");
				}

				_changeThisInstance = CompareInstance(from, suppressBreakingChanges);
			}

			return _changeThisInstance;
		}

		protected virtual ChangeType CompareInstance(ICanCompare from, bool suppressBreakingChanges)
		{
			CompareChildren(from, suppressBreakingChanges);

			return ChangeType.None;
		}

		protected virtual void CompareChildren(ICanCompare from, bool suppressBreakingChanges)
		{
			ListOperations.CheckAlignment(from.Children, _children);

			for (int i = 0; i < from.Children.Count; i++)
			{
				((RootDetail)_children[i]).PerformCompareInternal((ICanCompare)from.Children[i], suppressBreakingChanges || this.SuppressBreakingChangesInChildren);
			}
		}

		protected virtual void CalcInheritedChanges()
		{
			_changeAllChildren = ChangeType.None;

			if (ChangeTypeUtil.IsAddRemove(_changeThisInstance))
			{
				_changeAllChildren = _changeThisInstance;
				return;
			}

			foreach (RootDetail child in _children)
			{
				child.CalcInheritedChanges();
			}

			for (int i = 0; i < _children.Count; i++)
			{
				ProcessChildChange(_children[i].GetType(), ((ICanCompare)_children[i]).Change);
			}
		}

		protected virtual void ProcessChildChange(Type childType, ChangeType change)
		{
			if (change != ChangeType.None)
			{
				if (childType == typeof(AttributeDetail))
				{
					_changeAllChildren |= ChangeType.AttributesChanged;
				}
				else
				{
					if (this.CollapseChildren)
					{
						CollapseChildChange(change);
					}
					else
					{
						if (ChangeTypeUtil.HasBreaking(change))
						{
							_changeAllChildren |= ChangeType.MembersChangedBreaking;
						}

						if (ChangeTypeUtil.HasNonBreaking(change))
						{
							_changeAllChildren |= ChangeType.MembersChangedNonBreaking;
						}
					}
				}
			}
		}

		protected virtual void CollapseChildChange(ChangeType change)
		{
			if (!ChangeTypeUtil.IsAddRemove(_changeThisInstance))
			{
				if (((change & ChangeType.Added) != 0) || ((change & ChangeType.RemovedNonBreaking) != 0) || ((change & ChangeType.DeclarationChangedNonBreaking) != 0) || ((change & ChangeType.ValueChangedNonBreaking) != 0))
				{
					_changeThisInstance |= ChangeType.DeclarationChangedNonBreaking;
				}

				if (((change & ChangeType.RemovedBreaking) != 0) || ((change & ChangeType.DeclarationChangedBreaking) != 0) || ((change & ChangeType.ValueChangedBreaking) != 0))
				{
					_changeThisInstance |= ChangeType.DeclarationChangedBreaking;
				}

				if ((change & ChangeType.VisibilityChangedBreaking) != 0)
				{
					_changeThisInstance |= ChangeType.VisibilityChangedBreaking;
				}
				
				if ((change & ChangeType.VisibilityChangedNonBreaking) != 0)
				{
					if ((_changeThisInstance & ChangeType.VisibilityChangedBreaking) == 0)
					{
						_changeThisInstance |= ChangeType.VisibilityChangedNonBreaking;
					}
				}

				if ((change & ChangeType.ImplementationChanged) != 0)
				{
					_changeThisInstance |= ChangeType.ImplementationChanged;
				}
			}
		}

		public virtual void ApplyFilter(ComparisonFilter filter)
		{
			bool allChildrenExcluded = true;

			_filterStatus = FilterStatus.DontCare;

			foreach (RootDetail child in _children)
			{
				child.ApplyFilter(filter);

				if (child._filterStatus > FilterStatus.Exclude)
				{
					allChildrenExcluded = false;
				}
			}

			ApplyFilterInstance(filter);

			if (filter.ChangedItemsOnly && _changeThisInstance == ChangeType.None && _filterStatus != FilterStatus.ExcludeBlock)
			{
				_filterStatus = FilterStatus.Exclude;
			}

			if (_filterStatus == FilterStatus.Exclude && !allChildrenExcluded)
			{
				_filterStatus = FilterStatus.ExcludedButIncludeForChildren;
			}
			else if (_filterStatus == FilterStatus.DontCare && allChildrenExcluded && _children.Count > 0)
			{
				_filterStatus = FilterStatus.Exclude;
			}

			CalcInheritedChanges();
		}

		protected virtual void ApplyFilterInstance(ComparisonFilter filter)
		{
			if (!string.IsNullOrEmpty(filter.TextFilter))
			{
				if ((_name == null) || (!_name.ToLower().Contains(filter.TextFilter.ToLower())))
				{
					SetFilterStatus(FilterStatus.Exclude);
				}
				else
				{
					SetFilterStatus(FilterStatus.Include);
				}
			}
		}

		protected void SetFilterStatus(FilterStatus status)
		{
			if (status == FilterStatus.Include)
			{
				if (_filterStatus > FilterStatus.Exclude)
				{
					_filterStatus = FilterStatus.Include;
				}
			}
			else
			{
				_filterStatus = status;
			}
		}

		public int CompareTo(RootDetail other)
		{
			if (this.RelativeSortOrder != other.RelativeSortOrder)
			{
				return this.RelativeSortOrder.CompareTo(other.RelativeSortOrder);
			}

			return _name.CompareTo(other._name);
		}

		public int CompareTo(object obj)
		{
			if (!(obj is RootDetail))
			{
				throw new InvalidOperationException("Cannot compare across types");
			}

			return CompareTo((RootDetail)obj);
		}

		protected virtual int RelativeSortOrder
		{
			get { return 0; }
		}

		public virtual bool CollapseChildren
		{
			get { return false; }
		}

		public virtual bool ExcludeFromReport
		{
			get { return false; }
		}

		public virtual string GetTextSummary()
		{
			if (this.Change == ChangeType.None)
			{
				return string.Empty;
			}

			StringBuilder sb = new StringBuilder();

			if ((this.Change & ChangeType.Added) != 0)
			{
				sb.Append("Added, ");
			}

			if ((this.Change & ChangeType.ImplementationChanged) != 0)
			{
				sb.Append("Implementation Changed, ");
			}

			if ((this.Change & ChangeType.ContentChanged) != 0)
			{
				sb.Append("Content Changed, ");
			}

			if (((this.Change & ChangeType.ValueChangedBreaking) != 0) || ((this.Change & ChangeType.ValueChangedNonBreaking) != 0))
			{
				sb.Append("Value Changed, ");
			}

			if ((this.Change & ChangeType.AttributesChanged) != 0)
			{
				sb.Append("Attributes Changed, ");
			}

			if (((this.Change & ChangeType.RemovedBreaking) != 0) || ((this.Change & ChangeType.RemovedNonBreaking) != 0))
			{
				sb.Append("Removed, ");
			}

			if (((this.Change & ChangeType.VisibilityChangedBreaking) != 0) || ((this.Change & ChangeType.VisibilityChangedNonBreaking) != 0))
			{
				sb.Append("Visibility Changed, ");
			}

			if (((this.Change & ChangeType.DeclarationChangedBreaking) != 0) || ((this.Change & ChangeType.DeclarationChangedNonBreaking) != 0))
			{
				sb.Append("Declaration Changed, ");
			}

			if (((this.Change & ChangeType.MembersChangedBreaking) != 0) || ((this.Change & ChangeType.MembersChangedNonBreaking) != 0))
			{
				sb.Append("Members Changed, ");
			}

			sb.Remove(sb.Length - 2, 2);

			return sb.ToString();
		}

		public virtual string GetTextChangeDescription()
		{
			StringBuilder sb = new StringBuilder();
			RootDetail previous = (RootDetail)_navigateBackward;

			if ((this.Change & ChangeType.Added) != 0)
			{
				AppendClause(sb, "Added");
			}

			if (((this.Change & ChangeType.RemovedBreaking) != 0) || ((this.Change & ChangeType.RemovedNonBreaking) != 0))
			{
				AppendClause(sb, "Removed");
			}

			if ((this.Change & ChangeType.ContentChanged) != 0)
			{
				AppendClause(sb, "Content changed");
			}

			if ((this.Change & ChangeType.ValueChangedBreaking) != 0)
			{
				AppendClause(sb, "Value has a breaking change");
			}
			else if ((this.Change & ChangeType.ValueChangedNonBreaking) != 0)
			{
				AppendClause(sb, "Value has a non-breaking change");
			}

			if (((this.Change & ChangeType.DeclarationChangedBreaking) != 0) || ((this.Change & ChangeType.DeclarationChangedNonBreaking) != 0))
			{
				AppendClause(sb, "Declaration changed");
			}

			if (((this.Change & ChangeType.VisibilityChangedBreaking) != 0) || ((this.Change & ChangeType.VisibilityChangedNonBreaking) != 0))
			{
				AppendClause(sb, GetVisibilityChangeText(previous));
			}

			if (((this.Change & ChangeType.MembersChangedBreaking) != 0) || ((this.Change & ChangeType.MembersChangedNonBreaking) != 0))
			{
				GetTextDescriptionBriefMembers(sb);
			}

			if ((this.Change & ChangeType.ImplementationChanged) != 0)
			{
				AppendClause(sb, "Implementation changed");
			}

			if ((this.Change & ChangeType.AttributesChanged) != 0)
			{
				AppendClause(sb, "Attributes changed");
			}

			return sb.ToString();
		}

		private string GetVisibilityChangeText(RootDetail previous)
		{
			// When collapsing properties the visibility of the property itself (as opposed to the child accessors) may actually
			// be the same....
			Visibility prev = ((IHaveVisibility)previous).Visibility;
			Visibility mine = ((IHaveVisibility)this).Visibility;
			
			if (prev == mine)
			{
				return "Visibility changed";
			}
			else
			{
				return string.Format("Visibility was changed from {0} to {1}", prev.ToString().ToLower(), mine.ToString().ToLower());
			}
		}

		public virtual string GetHtmlChangeDescription()
		{
			StringBuilder sb = new StringBuilder();
			RootDetail previous = (RootDetail)_navigateBackward;

			if (this.Change == ChangeType.None)
			{
				sb.Append("None");
			}

			if ((this.Change & ChangeType.Added) != 0)
			{
				AppendClauseHtml(sb, false, "Added");
			}

			if (((this.Change & ChangeType.RemovedBreaking) != 0) || ((this.Change & ChangeType.RemovedNonBreaking) != 0))
			{
				AppendClauseHtml(sb, ((this.Change & ChangeType.RemovedBreaking) != 0), "Removed");
			}

			if ((this.Change & ChangeType.AttributesChanged) != 0)
			{
				AppendClauseHtml(sb, false, "Attributes changed");
			}

			if ((this.Change & ChangeType.ImplementationChanged) != 0)
			{
				AppendClauseHtml(sb, false, "Implementation changed");
			}

			if ((this.Change & ChangeType.ContentChanged) != 0)
			{
				AppendClauseHtml(sb, ChangeTypeUtil.HasBreaking(this.Change), "Content changed");
			}

			if (((this.Change & ChangeType.ValueChangedBreaking) != 0) || ((this.Change & ChangeType.ValueChangedNonBreaking) != 0))
			{
				AppendClauseHtml(sb, ((this.Change & ChangeType.ValueChangedBreaking) != 0), "Value changed");
			}

			if (((this.Change & ChangeType.DeclarationChangedBreaking) != 0) || ((this.Change & ChangeType.DeclarationChangedNonBreaking) != 0))
			{
				AppendClauseHtml(sb, ((this.Change & ChangeType.DeclarationChangedBreaking) != 0), "Declaration changed");
			}

			if (((this.Change & ChangeType.VisibilityChangedBreaking) != 0) || ((this.Change & ChangeType.VisibilityChangedNonBreaking) != 0))
			{
				AppendClauseHtml(sb, ((this.Change & ChangeType.VisibilityChangedBreaking) != 0), GetVisibilityChangeText(previous));
			}

			if (((this.Change & ChangeType.MembersChangedBreaking) != 0) || ((this.Change & ChangeType.MembersChangedNonBreaking) != 0))
			{
				GetHtmlChangeDescriptionBriefMembers(sb);
			}

			return sb.ToString();
		}

		protected virtual void GetTextDescriptionBriefMembers(StringBuilder sb)
		{
			AppendClause(sb, "Members changed");
		}

		protected virtual void GetHtmlChangeDescriptionBriefMembers(StringBuilder sb)
		{
			AppendClauseHtml(sb, ((this.Change & ChangeType.MembersChangedBreaking) != 0), "Members changed", _name);
		}

		public virtual string GetTextTitle()
		{
			return this.FullName;
		}

		public virtual string GetTextDeclaration()
		{
			return _name;
		}

		public virtual string GetHtmlDeclaration()
		{
			return _name;
		}

		public virtual void WriteHtmlDescription(TextWriter tw, bool appendAllDeclarations, bool appendChildren)
		{
			if (!this.ExcludeFromReport)
			{
				FilterStatus filterThisInstance;

				if (appendAllDeclarations)
				{
					filterThisInstance = GetStrongestFilterStatus();

					if (this.CollapseChildren && filterThisInstance == FilterStatus.ExcludedButIncludeForChildren)
					{
						filterThisInstance = FilterStatus.Include;
					}
				}
				else
				{
					filterThisInstance = FilterStatus.Include;
				}

				if ((!appendChildren) || (filterThisInstance >= FilterStatus.DontCare))
				{
					tw.Write("<p class='hdr'>");

					RootDetail namedItem;
					if (appendAllDeclarations)
					{
						namedItem = FindItemWithStatusPresent();
					}
					else
					{
						namedItem = this;
					}

					if ((namedItem == null) || (namedItem.Status == Status.Missing))
					{
						tw.Write(this.Name);
					}
					else
					{
						tw.Write(HtmlUtility.HtmlEncode(namedItem.GetTextTitle()));
					}

					tw.Write("</p>");

					RootDetail eachItem = this;
					while (eachItem != null)
					{
						eachItem.WriteHtmlDeclaration(tw);
						eachItem.WriteHtmlSummaryForChange(tw);

						eachItem = appendAllDeclarations ? (RootDetail)eachItem.NavigateForward : null;
					}
				}
			}

			if (appendChildren && !this.CollapseChildren && !ChangeTypeUtil.IsAddRemove(CombineAllChangesThisInstanceGoingForward()))
			{
				foreach (RootDetail child in FilterChildrenInAll<RootDetail>())
				{
//					if ((child.FullNameRoot) || (child.GetStrongestFilterStatus() != FilterStatus.ExcludedButIncludeForChildren)) // Dont include public stuff inside internal classes for -publiconly 
					{
						child.WriteHtmlDescription(tw, appendAllDeclarations, appendChildren);
					}
				}
			}
		}

		private ChangeType CombineAllChangesThisInstanceGoingForward()
		{
			ChangeType change = ChangeType.None;

			RootDetail item = this;

			while (item != null)
			{
				change |= item._changeThisInstance;

				item = (RootDetail)item.NavigateForward;
			}

			return change;
		}

		private RootDetail FindItemWithStatusPresent()
		{
			RootDetail item = this;

			while (item.Status != Status.Present)
			{
				item = (RootDetail)item.NavigateForward;
			}

			return item;
		}

		private void ForceChangeToAllDescendants()
		{
			foreach (RootDetail child in _children)
			{
				child._changeThisInstance = _changeThisInstance;
				child.ForceChangeToAllDescendants();
			}
		}

		private void WriteHtmlDeclaration(TextWriter tw)
		{
			tw.Write("<p class='hdr2'>In assembly ");
			tw.Write(this.DeclaringAssembly.Location);
			tw.Write(":</p>");

			if (this.Status == Status.Present)
			{
				tw.Write("<p class='code'>");
				tw.Write(GetHtmlDeclaration());
				tw.Write("</p>");

				//tw.Write("<p>Debug: Filter Status={0}</p>", this.FilterStatus);
			}
			else
			{
				tw.Write("<p>Not Defined</p>");
			}
		}

		private void WriteHtmlSummaryForChange(TextWriter tw)
		{
			if (this.Change != ChangeType.None)
			{
				tw.Write("<p>Changes Found : ");
				tw.Write(GetHtmlChangeDescription());
				tw.Write("</p>");
			}
		}

		protected static void AppendClause(StringBuilder sb, string format, params string[] args)
		{
			if (sb.Length > 0)
			{
				sb.Append(Environment.NewLine);
				sb.Append(Environment.NewLine);
			}

			sb.AppendFormat(format, args);
		}

		protected static void AppendClauseHtml(StringBuilder sb, bool breaking, string format, params string[] args)
		{
			if (sb.Length > 0)
			{
				sb.Append(", ");
			}

			if (breaking)
			{
				sb.Append("<span class='brkchg'>");
			}

			sb.AppendFormat(format, args);

			if (breaking)
			{
				sb.Append(" (Breaking)</span>");
			}
		}

		internal virtual void SerializeWriteRawXml(XmlWriter writer)
		{
			writer.WriteStartElement(SerializeGetElementName());

			SerializeWriteRawContent(writer);

			if (SerializeShouldWriteChildren())
			{
				foreach (RootDetail child in _children)
				{
					child.SerializeWriteRawXml(writer);
				}
			}

			writer.WriteEndElement();
		}

		internal virtual void SerializeWriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(SerializeGetElementName());

			SerializeWriteContent(writer);

			if (SerializeShouldWriteChildren())
			{
				foreach (RootDetail child in FilterChildren<RootDetail>())
				{
//					if ((child.FullNameRoot) || (child.FilterStatus != FilterStatus.ExcludedButIncludeForChildren)) // Dont include public stuff inside internal classes for -publiconly 
					{
						child.SerializeWriteXml(writer);
					} 
				}
			}

			writer.WriteEndElement();
		}

		private bool SerializeShouldWriteChildren()
		{
			return (_status == Status.Present) && (_changeThisInstance != ChangeType.Added);
		}

		protected virtual void SerializeWriteRawContent(XmlWriter writer)
		{
			if (SerializeShouldWriteName())
			{
				writer.WriteAttributeString("Name", _name);
			}

			if (_status != Status.Present)
			{
				writer.WriteAttributeString("Status", _status.ToString());
			}

			if (this.Change != ChangeType.None)
			{
				writer.WriteAttributeString("Change", this.Change.ToString());
			}
		}

		protected virtual void SerializeWriteContent(XmlWriter writer)
		{
			if (SerializeShouldWriteName())
			{
				writer.WriteAttributeString("Name", _name);
			}

			writer.WriteAttributeString("WhatChanged", SerializeGetWhatChangedName());

			bool breaking = ChangeTypeUtil.HasBreaking(this.Change);
			if (breaking)
			{
				writer.WriteAttributeString("Breaking", breaking.ToString());
			}

			RootDetail previous = (RootDetail)this.NavigateBackward;

			if (previous != null)
			{
				string from = null;
				string to = SerializeGetWhatChangedValue(this.Change);

				if (this.Change != ChangeType.Added)
				{
					from = previous.SerializeGetWhatChangedValue(this.Change);
				}

				if (string.Compare(from, to, false) != 0)
				{
					if (from != null)
					{
						writer.WriteAttributeString("Previous", from);
					}

					if (to != null)
					{
						writer.WriteAttributeString("Current", to);
					}
				}
			}
		}

		protected virtual bool SerializeShouldWriteName()
		{
			return true;
		}

		protected virtual string SerializeGetElementName()
		{
			return "Unknown";
		}

		protected virtual string SerializeGetWhatChangedName()
		{
			StringBuilder sb = new StringBuilder();

			if ((this.Change & ChangeType.Added) != 0)
			{
				sb.Append("Added, ");
			}

			if ((this.Change & ChangeType.ImplementationChanged) != 0)
			{
				sb.Append("Implementation, ");
			}

			if ((this.Change & ChangeType.ContentChanged) != 0)
			{
				sb.Append("Content, ");
			}

			if ((this.Change & ChangeType.ValueChangedBreaking) != 0)
			{
				sb.Append("Value, ");
			}
			else if ((this.Change & ChangeType.ValueChangedNonBreaking) != 0)
			{
				sb.Append("Value, ");
			}

			if ((this.Change & ChangeType.AttributesChanged) != 0)
			{
				sb.Append("Attributes, ");
			}

			if ((this.Change & ChangeType.RemovedBreaking) != 0)
			{
				sb.Append("Removed, ");
			}
			else if ((this.Change & ChangeType.RemovedNonBreaking) != 0)
			{
				sb.Append("Removed, ");
			}

			if ((this.Change & ChangeType.VisibilityChangedBreaking) != 0)
			{
				sb.Append("Visibility, ");
			}
			else if ((this.Change & ChangeType.VisibilityChangedNonBreaking) != 0)
			{
				sb.Append("Visibility, ");
			}

			if ((this.Change & ChangeType.DeclarationChangedBreaking) != 0)
			{
				sb.Append("Declaration, ");
			}

			if ((this.Change & ChangeType.DeclarationChangedNonBreaking) != 0)
			{
				sb.Append("Declaration, ");
			}

			if ((this.Change & ChangeType.MembersChangedBreaking) != 0)
			{
				sb.Append("Members, ");
			}
			else if ((this.Change & ChangeType.MembersChangedNonBreaking) != 0)
			{
				sb.Append("Members, ");
			}

			if (sb.Length == 0)
			{
				sb.Append("Nothing");
			}
			else
			{
				sb.Remove(sb.Length - 2, 2);
			}

			return sb.ToString();
		}

		protected virtual string SerializeGetWhatChangedValue(ChangeType changeType)
		{
			if (((changeType & ChangeType.DeclarationChangedNonBreaking) != 0) || ((changeType & ChangeType.DeclarationChangedBreaking) != 0) || (changeType == ChangeType.Added) || (changeType == ChangeType.ValueChangedBreaking) || (changeType == ChangeType.ValueChangedNonBreaking))
			{
				return ToString();
			}
			else if ((this is IHaveVisibility) && (((changeType & ChangeType.VisibilityChangedBreaking) != 0) || ((changeType & ChangeType.VisibilityChangedNonBreaking) != 0)))
			{
				IHaveVisibility hv = (IHaveVisibility)this;
				return hv.Visibility.ToString().ToLower();
			}
			else
			{
				return null;
			}
		}
	}
}
