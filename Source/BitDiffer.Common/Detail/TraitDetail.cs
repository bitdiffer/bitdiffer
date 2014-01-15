using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Xml;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public partial class TraitDetail : RootDetail
	{
		private string _value;

		public TraitDetail()
		{
		}

		public TraitDetail(RootDetail parent, string name, string value)
			: base(parent, name)
		{
			_value = value;
		}

		protected override bool FullNameRoot
		{
			get { return true; }
		}

		public override string ToString()
		{
			return _value;
		}

		public override string GetTextSummary()
		{
			return _value;
		}

		public override string GetHtmlDeclaration()
		{
			return _value;
		}

		protected override ChangeType CompareInstance(ICanCompare previous, bool suppressBreakingChanges)
		{
			ChangeType change = base.CompareInstance(previous, suppressBreakingChanges);

			TraitDetail other = (TraitDetail)previous;

			if (string.Compare(_value, other._value) != 0)
			{
				change |= ChangeType.ValueChangedNonBreaking;
			}

			return change;
		}

		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		protected override void SerializeWriteRawContent(XmlWriter writer)
		{
			base.SerializeWriteRawContent(writer);

			writer.WriteAttributeString("Value", _value);
		}

		protected override string SerializeGetElementName()
		{
			return "AssemblyProperty";
		}

		protected override int RelativeSortOrder
		{
			get { return -10; }
		}
	}
}
