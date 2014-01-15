using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;

namespace BitDiffer.Common.Utility
{
	public class CodeStringBuilder
	{
		private AppendMode _mode = AppendMode.Both;
		private StringBuilder _sbText = new StringBuilder(25);
		private StringBuilder _sbHtml = new StringBuilder(75);

		public CodeStringBuilder()
		{
		}

		public CodeStringBuilder(AppendMode mode)
		{
			_mode = mode;
		}

		public AppendMode Mode
		{
			get { return _mode; }
			set { _mode = value; }
		}

		public void AppendText(string text)
		{
			AppendText(text, null);
		}

		public void AppendKeyword(string keyword)
		{
			AppendText(keyword, "keyword");
		}

		public void AppendVisibility(Visibility visibility)
		{
			AppendText(VisibilityUtil.GetVisibilityString(visibility), "visibility");
		}

		public void AppendType(Type type)
		{
			AppendType(type, true);
		}

		public void AppendType(Type type, bool includeNamespace)
		{
			string typeAsKeyword = GetTypeNameAsKeyword(type);

			if (typeAsKeyword != null)
			{
				AppendKeyword(typeAsKeyword);
				return;
			}

			if (type.IsGenericParameter && !type.IsGenericType)
			{
				AppendText(type.Name);
				return;
			}

			// Dont show the namespaces on user types in the UI - but keep them in text, for comparison and reports
			if (includeNamespace)
			{
				AppendMode restore = _mode;
				_mode &= ~AppendMode.Html;
				AppendText(type.Namespace);
				AppendText(".");
				_mode = restore;
			}

			if (type.IsGenericType)
			{
				AppendGeneric(type.Name, type.GetGenericArguments(), "usertype");
			}
			else
			{
				AppendText(type.Name, "usertype");
			}
		}

		public void AppendText(string word, string css)
		{
			if ((_mode & AppendMode.Text) != 0)
			{
				_sbText.Append(word);
			}

			if ((_mode & AppendMode.Html) != 0)
			{
				if (css != null)
				{
					_sbHtml.Append("<span class='");
					_sbHtml.Append(css);
					_sbHtml.Append("'>");
				}

				_sbHtml.Append(HtmlEncode(word));

				if (css != null)
				{
					_sbHtml.Append("</span>");
				}
			}
		}

		public void AppendNewline()
		{
			if ((_mode & AppendMode.Text) != 0)
			{
				_sbText.Append(Environment.NewLine);
			}

			if ((_mode & AppendMode.Html) != 0)
			{
				_sbHtml.Append("<br>");
			}
		}

		public void AppendIndent()
		{
			if ((_mode & AppendMode.Text) != 0)
			{
				_sbText.Append("     ");
			}

			if ((_mode & AppendMode.Html) != 0)
			{
				_sbHtml.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
			}
		}

		public void AppendParameter(ParameterInfo pi)
		{
			AppendParameterType(pi);

			// Dont use parameter names in comparing declarations
			AppendMode restore = _mode;
			_mode &= ~AppendMode.Text;

			if (pi.Name != null)
			{
				AppendText(" ");
				AppendText(pi.Name);
			}

			AppendParameterValue(pi.RawDefaultValue);

			_mode = restore;
		}

		public void AppendParameterType(ParameterInfo pi)
		{
			if (pi.IsIn && pi.IsOut)
			{
				AppendKeyword("ref ");
			}
			else if (pi.IsOut)
			{
				AppendKeyword("out ");
			}

			AppendType(pi.ParameterType);
		}

		public void AppendRawHtml(string html)
		{
			_sbHtml.Append(html);
		}

		public void AppendGenericRestrictions(Type type)
		{
			if (!type.IsGenericTypeDefinition)
			{
				return;
			}

			AppendGenericRestrictions(type.GetGenericArguments());
		}

		public void AppendGenericRestrictions(MethodBase mi)
		{
			AppendGenericRestrictions(mi.GetGenericArguments());
		}

		internal void AppendGenericRestrictions(Type[] arguments)
		{
			if (arguments == null || arguments.Length == 0)
			{
				return;
			}

			foreach (Type arg in arguments)
			{
				Type[] constraints = arg.GetGenericParameterConstraints();

				if (constraints == null || constraints.Length == 0)
				{
					return;
				}

				AppendKeyword(" where ");
				AppendText(arg.Name);
				AppendText(" : ");

				if ((arg.GenericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) != 0)
				{
					AppendKeyword("class");
					AppendText(", ");
				}

				if ((arg.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
				{
					AppendKeyword("struct");
					AppendText(", ");
				}

				if ((arg.GenericParameterAttributes & GenericParameterAttributes.DefaultConstructorConstraint) != 0)
				{
					AppendKeyword("new");
					AppendText("(), ");
				}

				foreach (Type constraint in constraints)
				{
					AppendType(constraint);
					AppendText(", ");
				}

				RemoveCharsFromEnd(2);
			}
		}

		public void AppendParameterValue(object value)
		{
			if ((value == null) || (value.GetType() == typeof(DBNull)))
			{
				return;
			}

			AppendText(" = ");

			AppendQuotedValue(value);
		}

		public void AppendQuotedValue(object value)
		{
			if (value == null)
			{
				AppendText("null");
			}
			else if (value is string)
			{
				AppendText("\"" + value.ToString() + "\"", "string");
			}
			else if (value is char)
			{
				AppendText("'" + value.ToString() + "'", "string");
			}
			else
			{
				AppendText(value.ToString());
			}
		}

		public void AppendBaseClasses(Type type)
		{
			if (((type.BaseType == null) || (type.BaseType == typeof(object))) && (type.GetInterfaces().Length == 0))
			{
				return;
			}

			// Dont use base types in comparing declarations.. someday, would be good to do a more intelligent compare (implemented interfaces removed is possibly a breaking change?)
			AppendMode restore = _mode;
			_mode &= ~AppendMode.Text;

			AppendText(" : ");

			if ((type.BaseType != null) && (type.BaseType != typeof(object)))
			{
				AppendType(type.BaseType);
				AppendText(", ");
			}

			foreach (Type intf in type.GetInterfaces())
			{
				AppendType(intf);
				AppendText(", ");
			}

			RemoveCharsFromEnd(2);

			_mode = restore;
		}

		public void RemoveCharsFromEnd(int count)
		{
			if ((_mode & AppendMode.Text) != 0)
			{
				_sbText.Remove(_sbText.Length - count, count);
			}

			if ((_mode & AppendMode.Html) != 0)
			{
				_sbHtml.Remove(_sbHtml.Length - count, count);
			}
		}

		private string HtmlEncode(string text)
		{
			text = text.Replace("<", "&lt;");
			text = text.Replace(">", "&gt;");
			return text;
		}

		private string GetTypeNameAsKeyword(Type type)
		{
			if (type.IsGenericType)
			{
				if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					string baseType = GetTypeNameAsKeyword(Nullable.GetUnderlyingType(type));

					if (baseType != null)
					{
						return baseType + "?";
					}
				}
			}
			else
			{
				if (type == typeof(void))
				{
					return "void";
				}
				if (type == typeof(string))
				{
					return "string";
				}
				else if (type == typeof(int))
				{
					return "int";
				}
				else if (type == typeof(long))
				{
					return "long";
				}
				else if (type == typeof(char))
				{
					return "char";
				}
				else if (type == typeof(bool))
				{
					return "bool";
				}
				else if (type == typeof(byte))
				{
					return "byte";
				}
				else if (type == typeof(DateTime))
				{
					return "DateTime";
				}
				else if (type == typeof(decimal))
				{
					return "decimal";
				}
				else if (type == typeof(double))
				{
					return "double";
				}
				else if (type == typeof(uint))
				{
					return "uint";
				}
				else if (type == typeof(ulong))
				{
					return "ulong";
				}
				else if (type == typeof(object))
				{
					return "object";
				}
			}

			return null;
		}

		private void AppendGeneric(string name, Type[] genericArguments, string css)
		{
			if ((genericArguments == null) || (genericArguments.Length == 0))
			{
				System.Diagnostics.Debug.Assert(false);
				return;
			}

			int apos = name.IndexOf('`');
			if (apos > 0)
			{
				AppendText(name.Substring(0, apos), css);
			}
			else
			{
				AppendText(name, css);
			}

			AppendText("<");

			foreach (Type gentype in genericArguments)
			{
				AppendType(gentype);
				AppendText(", ");
			}

			RemoveCharsFromEnd(2);
			AppendText(">");
		}

		public void AppendMethodName(MethodBase mi)
		{
			if (!mi.IsGenericMethod)
			{
				AppendText(mi.Name);
				return;
			}

			AppendGeneric(mi.Name, mi.GetGenericArguments(), null);
		}

		public override string ToString()
		{
			return _sbText.ToString();
		}

		public virtual string ToHtmlString()
		{
			return _sbHtml.ToString();
		}
	}
}
