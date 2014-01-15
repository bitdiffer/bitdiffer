using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Misc;

namespace BitDiffer.Client.Forms
{
	public partial class Progress : Form, IHandleProgress
	{
		private bool _cancelRequested;

		public Progress()
		{
			InitializeComponent();
		}

		public void SetMaxRange(int max)
		{
			progressBar1.Maximum = max;
		}

		public bool CancelRequested
		{
			get { return _cancelRequested; }
		}

		public void UpdateProgress(ProgressStatus progress)
		{
			if ((progress.Status != null) && (!_cancelRequested))
			{
				label1.Text = progress.Status;
			}

			if (progress.Step)
			{
				progressBar1.PerformStep();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			_cancelRequested = true;

			label1.Text = "Canceling...";
		}
	}
}