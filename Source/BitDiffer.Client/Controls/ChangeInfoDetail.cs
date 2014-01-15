using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using BitDiffer.Common.Interfaces;
using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Utility;
using BitDiffer.Client.MailSender;

namespace BitDiffer.Client.Controls
{
	public partial class ChangeInfoDetail : UserControl
	{
		public ChangeInfoDetail()
		{
			InitializeComponent();
		}

		internal void LoadFrom(AssemblyGroup grp)
		{
			if (grp == null)
			{
				LoadHtml("");
				return;
			}

			StringBuilder sb = new StringBuilder();
			using (StringWriter sw = new StringWriter(sb))
			{
				HtmlUtility.WriteHtmlStart(sw);
				grp.WriteHtmlDescription(sw);
				HtmlUtility.WriteHtmlEnd(sw);
			}

			LoadHtml(sb.ToString());
		}

		internal void LoadFromItem(ICanCompare item, bool showAllDeclarations)
		{
			if (item == null)
			{
				LoadHtml("");
				return;
			}

			if (!(item is RootDetail))
			{
				LoadHtml(item.ToString());
				return;
			}

			StringBuilder sb = new StringBuilder();
			using (StringWriter sw = new StringWriter(sb))
			{
				HtmlUtility.WriteHtmlStart(sw);
				((RootDetail)item).WriteHtmlDescription(sw, showAllDeclarations, false);
				HtmlUtility.WriteHtmlEnd(sw);
			}

			LoadHtml(sb.ToString());
		}

		internal void Clear()
		{
			LoadHtml("<html><body></body></html>");
		}

		private void LoadHtml(string html)
		{
			webBrowser1.DocumentText = html;
		}

		internal void Copy()
		{
			webBrowser1.Document.ExecCommand("SelectAll", true, null);
			webBrowser1.Document.ExecCommand("Copy", true, null);
			webBrowser1.Document.ExecCommand("Unselect", true, null);
		}

		internal void Print()
		{
			webBrowser1.ShowPrintDialog();
		}

		internal void PrintPreview()
		{
			webBrowser1.ShowPrintPreviewDialog();
		}

		internal void SendTo()
		{
			MailSenderController.SendEmail(this.ParentForm, Constants.ComparisonEmailSubject, webBrowser1.DocumentText);
		}
	}
}
