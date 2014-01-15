using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using BitDiffer.Common.Utility;
using BitDiffer.Common.TraceListeners;
using BitDiffer.Client.Forms;

namespace BitDiffer.Client.Controls
{
	public partial class TraceViewer : UserControl
	{
		public TraceViewer()
		{
			InitializeComponent();

			if (Program.IsRuntime)
			{
				foreach (TraceListener tl in Trace.Listeners)
				{
					if (tl is RelayingTraceListener)
					{
						((RelayingTraceListener)tl).Message += new EventHandler<TraceEventArgs>(_listener_Message);
					}
				}
			}
		}

		private delegate void listenerMessageDelegate(object sender, TraceEventArgs e);
		void _listener_Message(object sender, TraceEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new listenerMessageDelegate(_listener_Message), sender, e);
			}
			else
			{
				ListViewItem item = new ListViewItem();
				item.ImageIndex = (e.Level == TraceLevel.Error) ? 3 : (e.Level == TraceLevel.Warning) ? 2 : (e.Level == TraceLevel.Info) ? 1 : 0;
				item.Text = DateTime.Now.ToString();
				item.SubItems.Add(e.Message);
				listView1.Items.Add(item);

				listView1.EnsureVisible(listView1.Items.Count - 1);
			}
		}

		private void listView1_SizeChanged(object sender, EventArgs e)
		{
			if (Program.IsRuntime)
			{
				int remaining = listView1.ClientRectangle.Width;

				for (int i = 0; i < listView1.Columns.Count - 1; i++)
				{
					remaining -= listView1.Columns[i].Width;
				}

				listView1.Columns[listView1.Columns.Count - 1].Width = remaining - 50;
			}
		}

		internal void Clear()
		{
			listView1.Items.Clear();
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				LogDetailView ldv = new LogDetailView(listView1.SelectedItems[0].SubItems[1].Text);
				ldv.ShowDialog();
			}
		}
	}
}
