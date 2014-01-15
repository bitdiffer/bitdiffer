using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class PropertyDetail : CodeDetail
	{
		public PropertyDetail()
		{
		}

		public PropertyDetail(RootDetail parent, PropertyInfo pi)
			: base(parent, pi)
		{
			_name = pi.Name;
			_category = "property";

			MethodInfo[] methods = pi.GetAccessors(true);
			foreach (MethodInfo mi in methods)
			{
				MethodDetail m = new MethodDetail(this, mi);

				if ((m.Name.Length > 3) && (mi.IsSpecialName))
				{
					m.Name = m.Name.Substring(0, 3);
				}

				m.Declaration = null;
				_children.Add(m);
			}

			if (pi.GetIndexParameters().Length > 0)
			{
				CodeStringBuilder csbParameters = new CodeStringBuilder(AppendMode.Text);

				foreach (ParameterInfo ip in pi.GetIndexParameters())
				{
					csbParameters.AppendParameterType(ip);
					csbParameters.AppendText(", ");

					_parameterCount++;
				}

				csbParameters.RemoveCharsFromEnd(2);

				_parameterTypesList = csbParameters.ToString();
			}

			_visibility = VisibilityUtil.GetMostVisible(FilterChildren<MethodDetail>());

			CodeStringBuilder csb = new CodeStringBuilder();

			AppendAttributesDeclaration(csb);

			csb.Mode = AppendMode.Html;
			csb.AppendVisibility(_visibility);
			csb.AppendText(" ");
			csb.Mode = AppendMode.Both;

			csb.AppendType(pi.PropertyType);
			csb.AppendText(" ");
			csb.AppendText(pi.Name);

			if (this.ParameterCount > 0)
			{
				csb.AppendText("[");
				csb.AppendText(this.ParameterTypesList);
				csb.AppendText("]");
			}

			csb.Mode = AppendMode.Html;

			csb.AppendNewline();
			csb.AppendText("{");
			csb.AppendNewline();
			csb.AppendIndent();

			foreach (MethodDetail mi in FilterChildren<MethodDetail>())
			{
				if (mi.Visibility != _visibility)
				{
					csb.AppendVisibility(mi.Visibility);
					csb.AppendText(" ");
				}

				csb.AppendText(mi.Name);
				csb.AppendText("; ");
			}

			csb.AppendNewline();
			csb.AppendText("}");

			_declaration = csb.ToString();
			_declarationHtml = csb.ToHtmlString();
		}

		public override bool CollapseChildren
		{
			get { return true; }
		}

		protected override string SerializeGetElementName()
		{
			return "Property";
		}
	}
}
