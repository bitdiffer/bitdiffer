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
	public class ResourceDetail : RootDetail
	{
		private string _contentHash;

		public ResourceDetail()
		{
		}

		public ResourceDetail(RootDetail parent, string name, byte[] content)
			: base(parent, name)
		{
			_contentHash = GenericUtility.GetHashText(content);
		}

		public override string GetTextTitle()
		{
			return "Embedded resource " + _name;
		}

		public override string ToString()
		{
			return _contentHash;
		}

		protected override bool FullNameRoot
		{
			get { return true; }
		}

		protected override ChangeType CompareInstance(ICanCompare previous, bool suppressBreakingChanges)
		{
			ChangeType change = base.CompareInstance(previous, suppressBreakingChanges);

			ResourceDetail other = (ResourceDetail)previous;

			if (string.Compare(_contentHash, other._contentHash) != 0)
			{
				change |= ChangeType.ContentChanged;
			}

			return change;
		}

		public string ContentHash
		{
			get { return _contentHash; }
			set { _contentHash = value; }
		}

		protected override void SerializeWriteRawContent(XmlWriter writer)
		{
			base.SerializeWriteRawContent(writer);

			writer.WriteAttributeString("ContentHash", _contentHash);
		}

		protected override string SerializeGetElementName()
		{
			return "Resource";
		}
	}
}
