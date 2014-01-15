using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Reflection;

using BitDiffer.Common.Misc;
using System.Runtime.InteropServices;

namespace BitDiffer.Client
{
	// TestUser
	// 584514ce14ee40fd6a49b54dd73e9719e83e6e18

	static class Program
	{
		public static bool IsRuntime;
		public static string HelpFile;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			IsRuntime = LicenseManager.UsageMode == LicenseUsageMode.Runtime;

		    DisableWebBrowserClickSound();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Forms.MainFrm());
		}

        private static void DisableWebBrowserClickSound()
        {
            try
            {
                int feature = FEATURE_DISABLE_NAVIGATION_SOUNDS;
                CoInternetSetFeatureEnabled(feature, SET_FEATURE_ON_PROCESS, true);
            }
            catch
            {
            }
        }

        private const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        private const int SET_FEATURE_ON_THREAD = 0x00000001;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;
        private const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        private const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        private const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        private const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        private const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return:MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(int FeatureEntry, [MarshalAs(UnmanagedType.U4)] int dwFlags, bool fEnable);
	}
}