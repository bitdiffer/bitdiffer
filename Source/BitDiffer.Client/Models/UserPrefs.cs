using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace BitDiffer.Client.Models
{
	public static class UserPrefs
	{
		public static string LastSelectedAssemblyFolder
		{
			get
			{
				UserPrefSettings settings = GetSettings();
				return settings.LastSelectedAssemblyFolder;
			}

			set
			{
				UserPrefSettings settings = GetSettings();
				settings.LastSelectedAssemblyFolder = value;
				settings.Save();
			}
		}

		public static string LastSelectedComparisonSet
		{
			get
			{
				UserPrefSettings settings = GetSettings();
				return settings.LastSelectedComparisonSet;
			}

			set
			{
				UserPrefSettings settings = GetSettings();
				settings.LastSelectedComparisonSet = value;
				settings.Save();
			}
		}

		private static UserPrefSettings GetSettings()
		{
			UserPrefSettings settings = new UserPrefSettings();

			if (settings.CallUpgrade)
			{
				settings.Upgrade();
				settings.CallUpgrade = false;
				settings.Save();
			}

			return settings;
		}
	}

	public class UserPrefSettings : ApplicationSettingsBase
	{
		[UserScopedSetting]
		[DefaultSettingValue("True")]
		public bool CallUpgrade
		{
			get { return (bool)this["CallUpgrade"]; }
			set { this["CallUpgrade"] = value; }
		}

		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string LastSelectedAssemblyFolder
		{
			get { return (string)this["LastSelectedAssemblyFolder"]; }
			set { this["LastSelectedAssemblyFolder"] = value; }
		}

		[UserScopedSetting]
		[DefaultSettingValue("")]
		public string LastSelectedComparisonSet
		{
			get { return (string)this["LastSelectedComparisonSet"]; }
			set { this["LastSelectedComparisonSet"] = value; }
		}
	}
}
