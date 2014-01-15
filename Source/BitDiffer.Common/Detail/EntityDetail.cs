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
	public class EntityDetail : TypeDetail
	{
		public EntityDetail()
		{
		}

		public EntityDetail(RootDetail parent, Type type, bool takeVisibilityFromParent)
			: base(parent, type)
		{
			_visibility = VisibilityUtil.GetVisibilityFor(type);

			BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

			List<MethodBase> methods = FilterMethods(type.GetMethods(flags), type, false);
			foreach (MethodBase mi in methods)
			{
				MethodDetail md = new MethodDetail(this, mi);

				if (takeVisibilityFromParent)
				{
					md.Visibility = _visibility;
				}

				_children.Add(md);
			}

			List<MethodBase> constructors = FilterMethods(type.GetConstructors(flags), type, false);
			foreach (MethodBase mi in constructors)
			{
				MethodDetail md = new MethodDetail(this, mi);

				if (takeVisibilityFromParent)
				{
					md.Visibility = _visibility;
				}

				_children.Add(md);
			}

			List<MethodBase> operators = FilterMethods(type.GetMethods(flags), type, true);
			foreach (MethodBase mi in operators)
			{
				OperatorDetail od = new OperatorDetail(this, mi);

				if (takeVisibilityFromParent)
				{
					od.Visibility = _visibility;
				}

				_children.Add(od);
			}

			List<PropertyInfo> props = FilterProperties(type.GetProperties(flags), type);
			foreach (PropertyInfo pi in props)
			{
				PropertyDetail pd = new PropertyDetail(this, pi);

				if (takeVisibilityFromParent)
				{
					pd.Visibility = _visibility;
				}

				_children.Add(pd);
			}

			List<EventInfo> events = FilterEvents(type.GetEvents(flags), type);
			foreach (EventInfo ei in events)
			{
				EventDetail ed = new EventDetail(this, ei);

				if (takeVisibilityFromParent)
				{
					ed.Visibility = _visibility;
				}

				_children.Add(ed);
			}

			List<FieldInfo> fields = FilterFields(type.GetFields(flags), type);
			foreach (FieldInfo fi in fields)
			{
				FieldDetail fd = new FieldDetail(this, fi);

				if (takeVisibilityFromParent)
				{
					fd.Visibility = _visibility;
				}

				_children.Add(fd);
			}

			CodeStringBuilder csb = new CodeStringBuilder();

			AppendAttributesDeclaration(csb);

			csb.Mode = AppendMode.Html;
			csb.AppendVisibility(_visibility);
			csb.AppendText(" ");
			csb.Mode = AppendMode.Both;

			if (type.IsAbstract && type.IsSealed)
			{
				csb.AppendKeyword("static ");
			}
			else if (type.IsInterface)
			{
				csb.AppendKeyword("interface ");
			}
			else if (type.IsAbstract)
			{
				csb.AppendKeyword("abstract ");
			}
			else if (type.IsSealed)
			{
				csb.AppendKeyword("sealed ");
			}

			if (type.IsClass)
			{
				csb.AppendKeyword("class ");
			}

			csb.AppendText(_name);

			csb.AppendBaseClasses(type);

			csb.AppendGenericRestrictions(type);

			_declaration = csb.ToString();
			_declarationHtml = csb.ToHtmlString();
		}

		private static List<MethodBase> FilterMethods(MethodBase[] methods, Type type, bool operatorsOnly)
		{
			List<MethodBase> valid = new List<MethodBase>();

			foreach (MethodBase mi in methods)
			{
				if ((mi.IsSpecialName) && ((mi.Name.StartsWith("get_") || mi.Name.StartsWith("set_") || mi.Name.StartsWith("add_") || mi.Name.StartsWith("remove_"))))
				{
					continue;
				}

				if ((mi.IsSpecialName && mi.Name.StartsWith("op_")) != operatorsOnly)
				{
					continue;
				}

				if (mi.DeclaringType == type)
				{
					valid.Add(mi);
				}
			}

			return valid;
		}

		private static List<PropertyInfo> FilterProperties(PropertyInfo[] props, Type type)
		{
			List<PropertyInfo> valid = new List<PropertyInfo>();

			foreach (PropertyInfo pi in props)
			{
				if (pi.DeclaringType == type)
				{
					valid.Add(pi);
				}
			}

			return valid;
		}

		private List<FieldInfo> FilterFields(FieldInfo[] fields, Type type)
		{
			List<FieldInfo> valid = new List<FieldInfo>();

			foreach (FieldInfo fi in fields)
			{
				if (fi.DeclaringType == type)
				{
					if (FindEventNamed(fi.Name) == null)
					{
						valid.Add(fi);
					}
				}
			}

			return valid;
		}

		private EventDetail FindEventNamed(string name)
		{
			if (_children != null)
			{
				return (EventDetail)(_children.Find(delegate(ICanAlign test) { return (test.GetType() == typeof(EventDetail)) && (test.Name == name); }));
			}

			return null;
		}

		private static List<EventInfo> FilterEvents(EventInfo[] events, Type type)
		{
			List<EventInfo> valid = new List<EventInfo>();

			foreach (EventInfo ei in events)
			{
				if (ei.DeclaringType == type)
				{
					valid.Add(ei);
				}
			}

			return valid;
		}
	}
}
