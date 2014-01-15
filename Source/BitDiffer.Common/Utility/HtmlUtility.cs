using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BitDiffer.Common.Utility
{
	public class HtmlUtility
	{
		public static void WriteHtmlStart(TextWriter tw)
		{
			tw.Write("<html><head><style type='text/css'><!-- ");
			tw.Write(".hdr { font-weight:bold;background-color:#F0F0FF;padding:5 2 5 2; } ");
			tw.Write(".hdr1 { font-weight:bold;background-color:#B5D2FF;padding:5 2 5 2; } ");
			tw.Write(".hdr2 { font-weight:bold; } ");
			tw.Write(".code { font-family:consolas, 'courier new'; } ");
			tw.Write(".keyword { color:blue; } ");
			tw.Write(".brkchg { color:red; } ");
			tw.Write(".usertype { color:#2B91AF; } ");
			tw.Write(".string { color:#A31515; } ");
			tw.Write(".visibility { color:blue; } ");
			tw.Write("--></style></head><body style='font-family:Tahoma;font-size:9pt'>");
		}

		public static void WriteHtmlEnd(TextWriter tw)
		{
			tw.Write("</body></html>");
		}

		public static string HtmlEncode(string text)
		{
			text = text.Replace("<", "&lt;");
			text = text.Replace(">", "&gt;");
			return text;
		}
	}
}
