using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

using BitDiffer.Common.Model;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Common.Utility
{
	public static class VisibilityUtil
	{
		public static string GetVisibilityString(Visibility visibility)
		{
			switch (visibility)
			{
				case Visibility.Private:			return "private";
				case Visibility.Public:				return "public";
				case Visibility.Internal:			return "internal";
				case Visibility.Protected:			return "protected";
				case Visibility.ProtectedInternal:	return "protected internal";
				case Visibility.ProtectedPrivate:	return "protected private";
				default:							return "?";
			}
		}

		public static Visibility GetVisibilityFor(Type type)
		{
			if (!type.IsNested)
			{
				if (type.IsPublic)
				{
					return Visibility.Public;
				}
				else if (type.IsNotPublic)
				{
					return Visibility.Internal;
				}
			}
			else
			{
				if (type.IsNestedPublic)
				{
					return Visibility.Public;
				}
				else if (type.IsNestedPrivate)
				{
					return Visibility.Private;
				}
				else if (type.IsNestedFamily)
				{
					return Visibility.Protected;
				}
				else if (type.IsNestedAssembly)
				{
					return Visibility.Internal;
				}
				else if (type.IsNestedFamORAssem)
				{
					return Visibility.ProtectedInternal;
				}
				else if (type.IsNestedFamANDAssem)
				{
					return Visibility.ProtectedPrivate;
				}
			}

			System.Diagnostics.Debug.Assert(false);
			return Visibility.Public;
		}

		public static Visibility GetVisibilityFor(MethodBase method)
		{
			if (method.IsPublic)
			{
				return Visibility.Public;
			}
			else if (method.IsPrivate)
			{
				return Visibility.Private;
			}
			else if (method.IsFamily)
			{
				return Visibility.Protected;
			}
			else if (method.IsAssembly)
			{
				return Visibility.Internal;
			}
			else if (method.IsFamilyOrAssembly)
			{
				return Visibility.ProtectedInternal;
			}
			else if (method.IsFamilyAndAssembly)
			{
				return Visibility.ProtectedPrivate;
			}

			System.Diagnostics.Debug.Assert(false);
			return Visibility.Public;
		}

		public static Visibility GetVisibilityFor(FieldInfo fi)
		{
			if (fi.IsPublic)
			{
				return Visibility.Public;
			}
			else if (fi.IsPrivate)
			{
				return Visibility.Private;
			}
			else if (fi.IsFamily)
			{
				return Visibility.Protected;
			}
			else if (fi.IsAssembly)
			{
				return Visibility.Internal;
			}
			else if (fi.IsFamilyOrAssembly)
			{
				return Visibility.ProtectedInternal;
			}
			else if (fi.IsFamilyAndAssembly)
			{
				return Visibility.ProtectedPrivate;
			}

			System.Diagnostics.Debug.Assert(false);
			return Visibility.Public;
		}

		public static ChangeType GetVisibilityChange(Visibility from, Visibility to, bool suppressBreakingChanges)
		{
			if (from == to)
			{
				return ChangeType.None;
			}

			if ((!suppressBreakingChanges) && (from == Visibility.Public))
			{
				return ChangeType.VisibilityChangedBreaking;
			}
			else
			{
				return ChangeType.VisibilityChangedNonBreaking;
			}
		}

		public static Visibility GetMostVisible(ICanCompare item)
		{
			Visibility visibility = (item is IHaveVisibility) ? ((IHaveVisibility)item).Visibility : Visibility.Private;

			Visibility children = GetMostVisible(item.Children);

			return (visibility > children) ? visibility : children;
		}

		public static Visibility GetMostVisible(IEnumerable methods)
		{
			Visibility visibility = Visibility.Private;

			foreach (IHaveVisibility mi in methods)
			{
				if (mi.Visibility > visibility)
				{
					visibility = mi.Visibility;
				}
			}

			return visibility;
		}
    }
}
