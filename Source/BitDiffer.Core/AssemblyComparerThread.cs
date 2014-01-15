using System;
using System.Collections.Generic;
using System.Text;

using BitDiffer.Common.Misc;
using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;

namespace BitDiffer.Core
{
	internal class AssemblyComparerThread
	{
		private AssemblyManager _manager;
		private AssemblyGroup _group;
		private string _fileName;
		private IHandleProgress _progress;

		public AssemblyComparerThread(AssemblyManager manager, AssemblyGroup group, string fileName, IHandleProgress progress)
		{
			_manager = manager;
			_group = group;
			_fileName = fileName;
			_progress = progress;
		}

		public AssemblyManager Manager
		{
			get { return _manager; }
		}

		public AssemblyGroup Group
		{
			get { return _group; }
		}

		public string FileName
		{
			get { return _fileName; }
		}

		public IHandleProgress Progress
		{
			get { return _progress; }
		}
	}
}
