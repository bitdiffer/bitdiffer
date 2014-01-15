using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BitDiffer.Common.Misc;
using System.Reflection;
using BitDiffer.Client.Properties;

namespace BitDiffer.Client.Forms
{
	public partial class LogDetailView : Form
	{
		public LogDetailView()
		{
			InitializeComponent();

			this.Icon = Resources.App;
			this.Text = string.Format("{0} {1}", Constants.ProductName, Assembly.GetExecutingAssembly().GetName().Version);
		}

		public LogDetailView(string message)
			: this()
		{
			textBox1.Text = message;
		}
	}
}
