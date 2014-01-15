using System;
using System.Collections.Generic;
using System.Text;

using OutlookApp = Microsoft.Office.Interop.Outlook;

namespace BitDiffer.Client.MailSender
{
	public class OutlookMailSender
	{
		public void SendMail(string subject, string body)
		{
			OutlookApp.Application outlook = new Microsoft.Office.Interop.Outlook.Application();
			OutlookApp.MailItem email = (OutlookApp.MailItem)outlook.CreateItem(OutlookApp.OlItemType.olMailItem);
			email.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
			email.Subject = subject;
			email.HTMLBody = body;
			email.Display(false);
		}
	}
}

