using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class AttributeDetail : MemberDetail
	{
		public AttributeDetail()
		{
		}

		public AttributeDetail(RootDetail parent, CustomAttributeData cad)
			: base(parent, cad.Constructor.DeclaringType.FullName)
		{
			_declaration = cad.ToString();

			CodeStringBuilder csb = new CodeStringBuilder();

			AppendAttributesDeclaration(csb);

			csb.AppendType(cad.Constructor.DeclaringType);

			if (cad.ConstructorArguments.Count > 0)
			{
				csb.AppendText("(");
				csb.AppendQuotedValue(cad.ConstructorArguments[0].Value);
				csb.AppendText(")");
			}

			_declaration = csb.ToString();
			_declarationHtml = csb.ToHtmlString();
		}

		protected override bool FullNameRoot
		{
			get { return true; }
		}

		public override string GetTextTitle()
		{
			return "Attribute " + _name;
		}

		protected override void ApplyFilterInstance(ComparisonFilter filter)
		{
			base.ApplyFilterInstance(filter);

			if ((filter.IgnoreAssemblyAttributeChanges) && (this.Parent.GetType() == typeof(AttributesDetail)) && (this.Parent.Parent.GetType() == typeof(AssemblyDetail)))
			{
				_changeThisInstance = ChangeType.None;
			}
		}

		protected override string SerializeGetElementName()
		{
			return "Attribute";
		}
	}
}
