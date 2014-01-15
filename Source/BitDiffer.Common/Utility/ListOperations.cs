using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Exceptions;
using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Utility
{
	public static class ListOperations
	{
		public static bool CompareLists<T>(List<T> list1, List<T> list2, bool requireSameOrder) where T : ICanAlign
		{
			if ((list1 == null) || (list1.Count == 0))
			{
				return ((list2 == null) || (list2.Count == 0)) ? true : false;
			}

			if ((list2 == null) || (list2.Count == 0))
			{
				return false;
			}

			if (list1.Count != list2.Count)
			{
				return false;
			}

			if (requireSameOrder)
			{
				for (int i = 0; i < list1.Count; i++)
				{
					if (!(list1[i].Equals(list2[i])))
					{
						return false;
					}
				}
			}
			else
			{
				Hashtable verifiedTypes = new Hashtable();

				foreach (T o1 in list1)
				{
					if (!list2.Contains(o1))
					{
						return false;
					}

					verifiedTypes.Add(o1.Name, "");
				}

				foreach (T o2 in list2)
				{
					if (!verifiedTypes.ContainsKey(o2.Name))
					{
						return false;
					}
				}
			}

			return true;
		}

		public static void AlignLists(IList parents)
		{
			List<IList<ICanAlign>> allChildren = new List<IList<ICanAlign>>();

			foreach (ICanAlign parent in parents)
			{
				allChildren.Add(parent.Children);
			}

			AlignListsInternal(parents, allChildren.ToArray());
		}

		public static void AlignListsNoParent(params IList<ICanAlign>[] lists)
		{
			AlignListsInternal(null, lists);
		}

		private static void AlignListsInternal(IList parents, params IList<ICanAlign>[] lists)
		{
			if (lists.Length == 0)
			{
				return;
			}

			Prelign(parents);

			SortedList<string, Type> allIds = new SortedList<string, Type>();

			foreach (IList list in lists)
			{
				foreach (ICanAlign item in list)
				{
					if (item.Status == Status.Missing)
					{
						// Commented out - When the unit tests call ExtractItem() directly, they put missing items in the list
						// throw new ApplicationException(string.Format("The list has missing items in it. Has it already been aligned?"));
					}
					else
					{
						if (item.AlignmentIdentifier == null)
						{
							throw new ApplicationException(string.Format("The identifier of {0} is null.", GetItemAncestry(item)));
						}

						if (!allIds.ContainsKey(item.AlignmentIdentifier))
						{
							allIds.Add(item.AlignmentIdentifier, item.GetType());
						}
					}
				}
			}

			for (int i = 0; i < lists.Length; i++)
			{
				List<ICanAlign> result = new List<ICanAlign>();

				foreach (string id in allIds.Keys)
				{
					result.Add(FindOrReturnMissing(parents == null ? null : (ICanAlign)parents[i], lists[i], id, allIds[id]));
				}

				lists[i].Clear();

				foreach (ICanAlign item in result)
				{
					lists[i].Add(item);
				}
			}

			foreach (List<ICanAlign> list in lists)
			{
				list.Sort();
			}

			for (int i = 0; i < lists.Length; i++)
			{
				for (int j = 0; j < lists[0].Count; j++)
				{
					if (i < lists.Length - 1)
					{
						lists[i][j].NavigateForward = lists[i + 1][j];
					}

					if (i > 0)
					{
						lists[i][j].NavigateBackward = lists[i - 1][j];
					}
				}
			}

			for (int j = 0; j < lists[0].Count; j++)
			{
				List<ICanAlign> listParents = new List<ICanAlign>();

				for (int i = 0; i < lists.Length; i++)
				{
					listParents.Add(lists[i][j]);
				}

				AlignLists(listParents.ToArray());
			}
		}

		private static void Prelign(IList parents)
		{
			// When the name alone is not sufficient to correlate items across lists, (i.e., method overloads) this
			// is where the AlignmentIdentifier can be replaced with something that works better
			if ((parents == null) || (parents.Count == 0) || (! (typeof(EntityDetail).IsAssignableFrom(parents[0].GetType()))))
			{
				return;
			}

			// Get list of all unique names
			List<string> names = new List<string>();
			foreach (EntityDetail cd in parents)
			{
				foreach (CodeDetail md in cd.FilterChildren<CodeDetail>())
				{
					if ((md.Status == Status.Missing) || (md.Name.Contains("#")))
					{
						// Alignment and prelignment has already happened
						return;
					}

					if (!names.Contains(md.Name))
					{
						names.Add(md.Name);
					}
				}
			}

			// Prelign each code name as a group one at a time
			foreach (string name in names)
			{
				Prelign(parents, name);
			}
		}

		private static void Prelign(IList classes, string name)
		{
			List<List<CodeDetail>> codeGroups = new List<List<CodeDetail>>();

			foreach (EntityDetail cd in classes)
			{
				List<CodeDetail> list = new List<CodeDetail>();

				foreach (CodeDetail md in cd.FilterChildren<CodeDetail>())
				{
					if (md.Name == name)
					{
						md.AlignMatchStatus = AlignMatchStatus.None;
						list.Add(md);
					}
				}

				if (list.Count > 0)
				{
					codeGroups.Add(list);
				}
			}

			if (codeGroups.Count > 0)
			{
				Prelign(codeGroups);
			}
		}

		private static void Prelign(List<List<CodeDetail>> codeGroups)
		{
			// Chain together codes with exact parameter type list matches
			foreach (List<CodeDetail> codesInAssembly in codeGroups)
			{
				foreach (CodeDetail code in codesInAssembly)
				{
					if (code.AlignMatchStatus == AlignMatchStatus.None)
					{
						code.AlignmentIdentifier = code.Name + NextTag();

						AssignMatchingCodesByParameterTypes(code, codesInAssembly, codeGroups);
					}
				}
			}

			// Chain together codes with the same number of parameters
			foreach (List<CodeDetail> codesInAssembly in codeGroups)
			{
				foreach (CodeDetail code in codesInAssembly)
				{
					if (code.AlignMatchStatus == AlignMatchStatus.None)
					{
						code.AlignmentIdentifier = code.Name + NextTag();

						AssignMatchingCodesByParameterCount(code, codesInAssembly, codeGroups);
					}
				}
			}

			// Chain together any remaining codes
			foreach (List<CodeDetail> codesInAssembly in codeGroups)
			{
				foreach (CodeDetail code in codesInAssembly)
				{
					if (code.AlignMatchStatus == AlignMatchStatus.None)
					{
						code.AlignmentIdentifier = code.Name + NextTag();

						AssignAnyCode(code, codesInAssembly, codeGroups);
					}
				}
			}
		}

		private static void AssignMatchingCodesByParameterTypes(CodeDetail code, List<CodeDetail> codesInAssembly, List<List<CodeDetail>> codeGroups)
		{
			foreach (List<CodeDetail> codesInAssemblyTest in codeGroups)
			{
				if (!object.ReferenceEquals(codesInAssembly, codesInAssemblyTest))
				{
					foreach (CodeDetail codeTest in codesInAssemblyTest)
					{
						if (!object.ReferenceEquals(code, codeTest))
						{
							if (codeTest.AlignMatchStatus < AlignMatchStatus.Exact)
							{
								if (code.ParameterTypesList == codeTest.ParameterTypesList)
								{
									codeTest.AlignmentIdentifier = code.AlignmentIdentifier;
									codeTest.AlignMatchStatus = AlignMatchStatus.Exact;
									code.AlignMatchStatus = AlignMatchStatus.Exact;

									break;
								}
							}
						}
					}
				}
			}
		}

		private static void AssignMatchingCodesByParameterCount(CodeDetail code, List<CodeDetail> codesInAssembly, List<List<CodeDetail>> codeGroups)
		{
			foreach (List<CodeDetail> codesInAssemblyTest in codeGroups)
			{
				if (!object.ReferenceEquals(codesInAssembly, codesInAssemblyTest))
				{
					foreach (CodeDetail codeTest in codesInAssemblyTest)
					{
						if (!object.ReferenceEquals(code, codeTest))
						{
							if (codeTest.AlignMatchStatus < AlignMatchStatus.Strong)
							{
								if (code.ParameterCount == codeTest.ParameterCount)
								{
									if (code.AlignMatchStatus > AlignMatchStatus.Strong)
									{
										throw new WeakerMatchException(); // attempting to rematch at a lower strength?
									}

									codeTest.AlignmentIdentifier = code.AlignmentIdentifier;
									codeTest.AlignMatchStatus = AlignMatchStatus.Strong;
									code.AlignMatchStatus = AlignMatchStatus.Strong;

									break;
								}
							}
						}
					}
				}
			}
		}

		private static void AssignAnyCode(CodeDetail code, List<CodeDetail> codesInAssembly, List<List<CodeDetail>> codeGroups)
		{
			foreach (List<CodeDetail> codesInAssemblyTest in codeGroups)
			{
				if (!object.ReferenceEquals(codesInAssembly, codesInAssemblyTest))
				{
					foreach (CodeDetail codeTest in codesInAssemblyTest)
					{
						if (!object.ReferenceEquals(code, codeTest))
						{
							if (codeTest.AlignMatchStatus < AlignMatchStatus.Weak)
							{
								if (code.AlignMatchStatus > AlignMatchStatus.Weak)
								{
									throw new WeakerMatchException(); // attempting to rematch at a lower strength?
								}

								codeTest.AlignmentIdentifier = code.AlignmentIdentifier;
								codeTest.AlignMatchStatus = AlignMatchStatus.Weak;
								code.AlignMatchStatus = AlignMatchStatus.Weak;

								break;
							}
						}
					}
				}
			}
		}


		private static int _tag = 1;
		private static string NextTag()
		{
			return "#" + _tag++;
		}

		private static string GetItemAncestry(ICanAlign item)
		{
			ICanAlign parent = GetParent(item);
			ICanAlign root = GetRoot(item);

			if (object.ReferenceEquals(parent, root))
			{
				return string.Format("'{0}' type '{1}' in '{2}'", item, item.GetType(), parent);
			}
			else
			{
				return string.Format("'{0}' type '{1}' of '{2}' in '{3}'", item, item.GetType(), parent, root);
			}
		}

		private static ICanAlign GetParent(ICanAlign item)
		{
			return (item == null) ? null : item.Parent;
		}

		private static ICanAlign GetRoot(ICanAlign item)
		{
			if (item == null)
			{
				return null;
			}

			while (item.Parent != null)
			{
				item = item.Parent;
			}

			return item;
		}

		public static void CheckAlignment<T>(IEnumerable<T> list1, IEnumerable<T> list2) where T : ICanAlign
		{
			IEnumerator<T> e1 = list1.GetEnumerator();
			IEnumerator<T> e2 = list2.GetEnumerator();

			while (e1.MoveNext())
			{
				if (!e2.MoveNext())
				{
					throw new UnalignedListException();
				}

				if ((e1.Current == null) || (e2.Current == null))
				{
					throw new UnalignedListException();
				}

				if (e1.Current.Status == Status.Present && e2.Current.Status == Status.Present)
				{
					if (string.Compare(e1.Current.AlignmentIdentifier, e2.Current.AlignmentIdentifier) != 0)
					{
						throw new UnalignedListException();
					}
				}
			}

			if (e2.MoveNext())
			{
				throw new UnalignedListException();
			}
		}

		public static T FindOrReturnMissing<T>(IEnumerable<T> items, string identifier) where T : ICanAlign, new()
		{
			foreach (T item in items)
			{
				if (item.AlignmentIdentifier == identifier)
				{
					return item;
				}
			}

			T missing = new T();
			missing.Name = GetNameFromIdentifier(identifier);
			missing.AlignmentIdentifier = identifier;
			missing.Status = Status.Missing;
			return missing;
		}

		public static ICanAlign FindOrReturnMissing(ICanAlign parent, IEnumerable<ICanAlign> items, string identifier, Type type)
		{
			foreach (ICanAlign item in items)
			{
				if ((item.AlignmentIdentifier == identifier) && (item.GetType() == type))
				{
					return item;
				}
			}

			ICanAlign missing = (ICanAlign)Activator.CreateInstance(type);
			missing.Name = GetNameFromIdentifier(identifier);
			missing.AlignmentIdentifier = identifier;
			missing.Status = Status.Missing;
			missing.Parent = parent;
			return missing;
		}

		private static string GetNameFromIdentifier(string identifier)
		{
			int mark = identifier.IndexOf('#');

			if (mark < 0)
			{
				return identifier;
			}

			return identifier.Substring(0, mark);
		}
	}
}
