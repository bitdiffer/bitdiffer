using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class ClassDetail : EntityDetail
	{
		public ClassDetail()
		{
		}

		public ClassDetail(RootDetail parent, Type type)
			: base(parent, type, false)
		{
			BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			Type[] types = type.GetNestedTypes(flags);

			_category = "class";

			foreach (Type nested in types)
			{
				if (nested.IsEnum)
				{
					_children.Add(new EnumDetail(this, nested));
				}
				else if (nested.IsInterface)
				{
					_children.Add(new InterfaceDetail(this, nested));
				}
				else if (nested.IsClass)
				{
					_children.Add(new ClassDetail(this, nested));
				}
			}
		}

		protected override string SerializeGetElementName()
		{
			return "Class";
		}
	}
}
