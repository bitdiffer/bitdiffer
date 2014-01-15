using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;
using System.Security;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class MethodDetail : CodeDetail
	{
		private string _body;

		public MethodDetail()
		{
		}

		public MethodDetail(RootDetail parent, MethodBase mi)
			: base(parent, mi)
		{
			CodeStringBuilder csb = new CodeStringBuilder(AppendMode.Text);
			csb.AppendMethodName(mi);
			_name = csb.ToString();

			_visibility = VisibilityUtil.GetVisibilityFor(mi);
			_category = "method";

			MethodBody body = null;

			try
			{
				body = mi.GetMethodBody();
			}
			catch (VerificationException)
			{
				// "Operation could destabilize the runtime" on .NET 3.0 WPF PresentationCore.dll
			}

			if (body != null)
			{
				_body = GenericUtility.GetILAsHashedText(mi);
			}

			csb = new CodeStringBuilder();

			AppendAttributesDeclaration(csb);

			MethodInfo bi = null;			
			if (mi is MethodInfo)
			{
				bi = ((MethodInfo)mi).GetBaseDefinition();
			}

			csb.Mode = AppendMode.Html;
			csb.AppendVisibility(_visibility);
			csb.AppendText(" ");
			csb.Mode = AppendMode.Both;

			if (mi.IsAbstract)
			{
				if (!mi.DeclaringType.IsInterface)
				{
					csb.AppendKeyword("abstract ");
				}
			}
			else if (mi.IsVirtual && !mi.IsFinal)
			{
				if (!object.ReferenceEquals(mi, bi))
				{
					csb.AppendKeyword("override ");
				}
				else
				{
					csb.AppendKeyword("virtual ");
				}
			}
			else if (mi.IsStatic)
			{
				csb.AppendKeyword("static ");
			}

			if (mi is MethodInfo)
			{
				csb.AppendParameter(((MethodInfo)mi).ReturnParameter);
			}

			csb.AppendText(" ");
			csb.AppendText(_name);
			csb.AppendText("(");

			CodeStringBuilder csbParameters = new CodeStringBuilder(AppendMode.Text);

			foreach (ParameterInfo pi in mi.GetParameters())
			{
				csb.AppendParameter(pi);
				csb.AppendText(", ");

				csbParameters.AppendParameterType(pi);
				csbParameters.AppendText(", ");

				_parameterCount++;
			}

			if (mi.GetParameters().Length > 0)
			{
				csb.RemoveCharsFromEnd(2);
				csbParameters.RemoveCharsFromEnd(2);
			}

			csb.AppendText(")");

			if (mi is MethodInfo)
			{
				csb.AppendGenericRestrictions(mi);
			}

			_declaration = csb.ToString();
			_declarationHtml = csb.ToHtmlString();
			_parameterTypesList = csbParameters.ToString();
		}

		public string BodyHash
		{
			get { return _body; }
			set { _body = value; }
		}

		protected override void SerializeWriteRawContent(XmlWriter writer)
		{
			base.SerializeWriteRawContent(writer);

			if (_body != null)
			{
				writer.WriteAttributeString("BodyHash", _body);
			}
		}

		protected override ChangeType CompareInstance(ICanCompare previous, bool suppressBreakingChanges)
		{
			ChangeType change = base.CompareInstance(previous, suppressBreakingChanges);

			MethodDetail other = (MethodDetail)previous;

			if (string.Compare(_body, other._body) != 0)
			{
				change |= ChangeType.ImplementationChanged;
			}

			return change;
		}

		protected override string SerializeGetElementName()
		{
			return "Method";
		}
	}
}
