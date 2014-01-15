using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;

using BitDiffer.Common.Interfaces;

namespace BitDiffer.Common.Misc
{
	public class AssemblyDirectoryEntry : AlignableBase, IComparable
	{
		private string _path = string.Empty;

		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}

		public static AssemblyDirectoryEntry[] From(string root, string[] files)
		{
			AssemblyDirectoryEntry[] results = new AssemblyDirectoryEntry[files.Length];
            int clipFrom = root.Length;
            if (!root.EndsWith("\\") && !root.EndsWith("/"))
            {
                clipFrom++;
            }

			for (int i = 0; i < files.Length; i++)
			{
				AssemblyDirectoryEntry entry = new AssemblyDirectoryEntry();

				entry._name = files[i].Substring(clipFrom);
				entry._path = files[i];

				results[i] = entry;
			}

			return results;
		}

		public override string ToString()
		{
			return _status + ":" + _name;
		}

		public int CompareTo(object obj)
		{
			return _name.CompareTo(((AssemblyDirectoryEntry)obj)._name);
		}
	}
}
