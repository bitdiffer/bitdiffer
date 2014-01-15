using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using BitDiffer.Common.Misc;
using BitDiffer.Client.Properties;
using System.Diagnostics;

namespace BitDiffer.Client.Forms
{
	public partial class AboutBox : Form
	{
		public AboutBox()
		{
			InitializeComponent();

			if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
			{
				this.Icon = Resources.App;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}

