using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

using BitDiffer.Common.Configuration;
using BitDiffer.Common.Utility;
using System.Reflection;

namespace BitDiffer.Core
{
	public static class IsolationDetector
	{
		public static AppDomainIsolationLevel AutoDetectIsolationLevelDirs(params string[] allDirs)
		{
			List<string> allFiles = new List<string>();

			foreach (string dir in allDirs)
			{
				allFiles.AddRange(Directory.GetFiles(dir, "*.dll"));
				allFiles.AddRange(Directory.GetFiles(dir, "*.exe"));
			}

			return AutoDetectIsolationLevelFiles(allFiles.ToArray());
		}

		public static AppDomainIsolationLevel AutoDetectIsolationLevelFiles(params string[] allFiles)
		{
			Hashtable ht = new Hashtable();

			Log.Info("Detecting isolation level");

			foreach (string file in allFiles)
			{
				string dir = Path.GetDirectoryName(file);
				string assemblyName = GetFullName(file);
				string key =  dir + "?" + assemblyName;

				if (ht.Contains(key))
				{
					Log.Warn("Setting isolation level to High because the full name {0} was found in at least two assemblies in directory {1}.", assemblyName, dir);
					Log.Warn("Consider placing assemblies with identical full names in different directories to improve performance.");
					return AppDomainIsolationLevel.High;
				}

				ht.Add(key, "");
			}

			return AppDomainIsolationLevel.Medium;
		}

		private static string GetFullName(string file)
		{
			try
			{
				return AssemblyName.GetAssemblyName(file).ToString();
			}
			catch (BadImageFormatException)
			{
				return Path.GetFileName(file);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("Unable to evaulate file: {0}. {1}{1}Make sure the file is a valid .NET assembly", Path.GetFileName(file), Environment.NewLine), ex);
			}
		}
	}
}
