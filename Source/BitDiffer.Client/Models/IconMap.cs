using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Misc;
using BitDiffer.Client.Properties;
using BitDiffer.Common.Model;
using System.Windows.Forms;

namespace BitDiffer.Client.Models
{
	internal class IconMap
	{
		internal static Bitmap GetIconForItem(ICanAlign item)
		{
			Visibility visibility = Visibility.Public;

			if (item is IHaveVisibility)
			{
				visibility = ((IHaveVisibility)item).Visibility;
			}

			return (GetIconForItem(item.GetType(), item.Name, visibility));
		}

		internal static Bitmap GetIconForItem(Type type, string name, Visibility visibility)
		{
			if (type == typeof(AttributeDetail) || type == typeof(AttributesDetail))
			{
				return Resources.VSObject_Structure;
			}
			else if (type == typeof(ResourceDetail) || type == typeof(ResourcesDetail))
			{
				return Resources.VSProject_genericfile;
			}
			else if (type == typeof(ReferenceDetail) || type == typeof(ReferencesDetail))
			{
				return Resources.VSObject_Assembly;
			}
			else if (type == typeof(NamespaceDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
					case Visibility.Internal:
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Namespace_Friend;
					case Visibility.Public:
					default:
						return Resources.VSObject_Namespace;
				}
			}
			else if (type == typeof(TraitDetail))
			{
				switch (name)
				{
					case "Flags":
						return Resources.Flag_redHS;
					case "FullName":
						return Resources.Control_TextBox;
					case "PublicKeyToken":
						return Resources.Webcontrol_Loginstatus;
					case "Version":
						return Resources.VSProject_asa;
					case "RuntimeVersion":
						return Resources.VSProject_asp;
					default:
						return null;
				}
			}
			else if (type == typeof(ClassDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_Class_Private;
					case Visibility.Internal:
						return Resources.VSObject_Class_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Class_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_Class;
				}
			}
			else if (type == typeof(EnumDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_EnumPrivate;
					case Visibility.Internal:
						return Resources.VSObject_Enum_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Enum_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_Enum;
				}
			}
			else if (type == typeof(EnumItemDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_EnumItem_Private;
					case Visibility.Internal:
						return Resources.VSObject_EnumItem_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_EnumItem_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_EnumItem;
				}
			}
			else if (type == typeof(InterfaceDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_Interface_Private;
					case Visibility.Internal:
						return Resources.VSObject_Interface_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Interface_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_Interface;
				}
			}
			else if (type == typeof(EventDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_Event_Private;
					case Visibility.Internal:
						return Resources.VSObject_Event_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Event_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_Event;
				}
			}
			else if (type == typeof(MethodDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_Method_Private;
					case Visibility.Internal:
						return Resources.VSObject_Method_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Method_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_Method;
				}
			}
			else if (type == typeof(OperatorDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_Operator_Private;
					case Visibility.Internal:
						return Resources.VSObject_Operator_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Operator_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_Operator;
				}
			}
			else if (type == typeof(PropertyDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_Properties_Private;
					case Visibility.Internal:
						return Resources.VSObject_Properties_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Properties_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_Properties;
				}
			}
			else if (type == typeof(FieldDetail))
			{
				switch (visibility)
				{
					case Visibility.Private:
						return Resources.VSObject_Field_Private;
					case Visibility.Internal:
						return Resources.VSObject_Field_Friend;
					case Visibility.Protected:
					case Visibility.ProtectedInternal:
					case Visibility.ProtectedPrivate:
						return Resources.VSObject_Field_Protected;
					case Visibility.Public:
					default:
						return Resources.VSObject_Field;
				}
			}
			else
			{
				return null;
			}
		}
	}
}
