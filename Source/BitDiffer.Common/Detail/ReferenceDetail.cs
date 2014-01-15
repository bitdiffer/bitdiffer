using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Common.Model
{
	[Serializable]
	public class ReferenceDetail : RootDetail
	{
		private string _assemblyName;

		public ReferenceDetail()
		{
		}

		public ReferenceDetail(RootDetail parent, AssemblyName name)
		{
			_assemblyName = name.FullName;
			_parent = parent;

			if (name.FullName.IndexOf(',') > 0)
			{
				_name = name.FullName.Substring(0, name.FullName.IndexOf(','));
			}
			else
			{
				_name = name.FullName;
			}
		}

		public override string ToString()
		{
			return _assemblyName;
		}

		public override string GetTextTitle()
		{
			return "Reference to " + _name;
		}

		public override string GetHtmlDeclaration()
		{
			return _assemblyName;
		}

		protected override bool FullNameRoot
		{
			get { return true; }
		}

		protected override ChangeType CompareInstance(ICanCompare previous, bool suppressBreakingChanges)
		{
			ChangeType change = base.CompareInstance(previous, suppressBreakingChanges);

			ReferenceDetail other = (ReferenceDetail)previous;

			if (string.Compare(_assemblyName, other._assemblyName) != 0)
			{
				change |= ChangeType.ValueChangedNonBreaking;
			}

			return change;
		}

		public string AssemblyName
		{
			get { return _assemblyName; }
			set { _assemblyName = value; }
		}

		protected override void SerializeWriteRawContent(XmlWriter writer)
		{
			base.SerializeWriteRawContent(writer);

			writer.WriteAttributeString("AssemblyName", _assemblyName);
		}

		protected override string SerializeGetElementName()
		{
			return "Reference";
		}
	}
}
