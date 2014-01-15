using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace BitDiffer.Client.MailSender
{
	public static class MailSenderController
	{
		public static void SendEmail(IWin32Window parent, string subject, string body)
		{
			if (!DoSendEmail(subject, body))
			{
				MessageBox.Show(parent, "Unable to send email using your default mail client.\n\nYou may send email by using the 'Export' feature and manually attaching the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		private static bool DoSendEmail(string subject, string body)
		{
			try
			{
				RegistryKey key = Registry.ClassesRoot.OpenSubKey("mailto\\shell\\open\\command");

				if (key != null)
				{
					object value = key.GetValue(null);

					if (value != null)
					{
						if (value.ToString().ToLower().Contains("outlook.exe"))
						{
							new OutlookMailSender().SendMail(subject, body);
							key.Close();
							return true;
						}
					}

					key.Close();
				}
			}
			catch
			{
			}

			return false;
		}
	}
}

