using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using BitDiffer.Common.Configuration;

namespace BitDiffer.Client.Controls
{
	public partial class Configuration : UserControl
	{
		public Configuration()
		{
			InitializeComponent();
		}

		internal void LoadOptions(DiffConfig diffConfig)
		{
			cbIsolationLevel.SelectedIndex = (diffConfig.IsolationLevel == AppDomainIsolationLevel.Medium) ? 2 : ((diffConfig.IsolationLevel == AppDomainIsolationLevel.High) ? 1 : 0);
			cbResolvePref.SelectedIndex = diffConfig.TryResolveGACFirst ? 1 : 0;
			cbContext.SelectedIndex = diffConfig.UseReflectionOnlyContext ? 0 : 1;
			cbThreading.SelectedIndex = diffConfig.Multithread ? 0 : 1;
		}

		internal void SaveOptions(DiffConfig diffConfig)
		{
			diffConfig.IsolationLevel = (cbIsolationLevel.SelectedIndex == 2) ? AppDomainIsolationLevel.Medium : ((cbIsolationLevel.SelectedIndex == 1) ? AppDomainIsolationLevel.High : AppDomainIsolationLevel.AutoDetect);
			diffConfig.TryResolveGACFirst = cbResolvePref.SelectedIndex == 1;
			diffConfig.UseReflectionOnlyContext = cbContext.SelectedIndex == 0;
			diffConfig.Multithread = cbThreading.SelectedIndex == 0;
		}
	}
}
