using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using BitDiffer.Common.Misc;

namespace BitDiffer.Client.Controls
{
	public partial class AppHeader : UserControl
	{
		public AppHeader()
		{
			InitializeComponent();

			if (Program.IsRuntime)
			{
				lbVersion.Text = string.Format("{0} {1}", Constants.ProductName, Assembly.GetEntryAssembly().GetName().Version.ToString());
				lbSubtitle.Text = Constants.ProductSubTitle;
			}
		}
	}
}
