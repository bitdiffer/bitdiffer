using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BitDiffer.Client.Controls
{
	public partial class Header : UserControl
	{
		public Header()
		{
			InitializeComponent();
		}

		public string TitleText
		{
			get { return label2.Text; }
			set { label2.Text = value; }
		}

		public string SubtitleText
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
	}
}
