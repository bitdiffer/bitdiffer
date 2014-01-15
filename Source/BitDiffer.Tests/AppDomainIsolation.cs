using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;

using BitDiffer.Core;
using BitDiffer.Common.Model;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class AppDomainIsolation : TestBase
	{
		[TestMethod]
		public void AppDomain_Isolation_Low()
		{
			DiffConfig config = new DiffConfig();
			config.IsolationLevel = AppDomainIsolationLevel.Low;
			config.UseReflectionOnlyContext = false;
			ExtractAll(Subjects.One, config);
			AssertAssemblyNotInCurrentAppDomain("BitDiffer.Tests.Subject");
		}

		[TestMethod]
		public void AppDomain_Isolation_Med()
		{
			DiffConfig config = new DiffConfig();
			config.IsolationLevel = AppDomainIsolationLevel.Medium;
			config.UseReflectionOnlyContext = false;
			ExtractAll(Subjects.One, config);
			AssertAssemblyNotInCurrentAppDomain("BitDiffer.Tests.Subject");
		}

		[TestMethod]
		public void AppDomain_Isolation_High()
		{
			DiffConfig config = new DiffConfig();
			config.IsolationLevel = AppDomainIsolationLevel.High;
			config.UseReflectionOnlyContext = false;
			ExtractAll(Subjects.One, config);
			AssertAssemblyNotInCurrentAppDomain("BitDiffer.Tests.Subject");
		}

		[TestMethod]
		public void AppDomain_Isolation_Low_ReflOnly()
		{
			DiffConfig config = new DiffConfig();
			config.IsolationLevel = AppDomainIsolationLevel.Low;
			config.UseReflectionOnlyContext = true;
			ExtractAll(Subjects.One, config);
			AssertAssemblyNotInCurrentAppDomain("BitDiffer.Tests.Subject");
		}

		[TestMethod]
		public void AppDomain_Isolation_Med_ReflOnly()
		{
			DiffConfig config = new DiffConfig();
			config.IsolationLevel = AppDomainIsolationLevel.Medium;
			config.UseReflectionOnlyContext = true;
			ExtractAll(Subjects.One, config);
			AssertAssemblyNotInCurrentAppDomain("BitDiffer.Tests.Subject");
		}

		[TestMethod]
		public void AppDomain_Isolation_High_ReflOnly()
		{
			DiffConfig config = new DiffConfig();
			config.IsolationLevel = AppDomainIsolationLevel.High;
			config.UseReflectionOnlyContext = true;
			ExtractAll(Subjects.One, config);
			AssertAssemblyNotInCurrentAppDomain("BitDiffer.Tests.Subject");
		}

		[TestMethod]
		public void AppDomain_NotIsolated()
		{
			// Cant really run this test. Once the appdoamin has loaded the subject assembly, there is no way to unload it, and so
			// the other tests will fail. Can't predict the order tests will be executed. If you run them one at a time it works...

			// DiffConfig config = new DiffConfig();
			// config.UseIsolatedAppDomains = false;
			// config.UseReflectionOnlyContext = false;
			// ExtractAll(Subjects.One, config);
			// AssertAssemblyInCurrentAppDomain("BitDiffer.Tests.Subject");
		}

		[TestMethod]
		public void AppDomain_LoadTwoAssembliesSameName()
		{
			// Verify that we can load two different assemblies with the same strong name and examine them
			AttributeDetail ad1 = ExtractAttribute(Subjects.One, "System.Runtime.InteropServices.GuidAttribute");
			AttributeDetail ad2 = ExtractAttribute(Subjects.Two, "System.Runtime.InteropServices.GuidAttribute");

			Assert.AreNotEqual(ad1.Declaration, ad2.Declaration);
		}

		[TestMethod]
		public void IsolationDetector_Dirs_Medium()
		{
			Assert.AreEqual(AppDomainIsolationLevel.Medium, IsolationDetector.AutoDetectIsolationLevelDirs(Subjects.DirOne, Subjects.DirTwo));
		}

		[TestMethod]
		public void IsolationDetector_Dirs_High()
		{
			string working = Path.Combine(Subjects.DirEmpty, "working_2");
			Directory.CreateDirectory(working);
			string file1 = Path.Combine(working, "t1.dll");
			string file2 = Path.Combine(working, "t2.dll");
			File.Copy(Subjects.One, file1, true);
			File.Copy(Subjects.One, file2, true);

			try
			{
				Assert.AreEqual(AppDomainIsolationLevel.High, IsolationDetector.AutoDetectIsolationLevelDirs(working, Subjects.DirOne));
			}
			finally
			{
				File.Delete(file1);
				File.Delete(file2);
				Directory.Delete(working);
			}
		}

		[TestMethod]
		public void IsolationDetector_Files_Medium()
		{
			Assert.AreEqual(AppDomainIsolationLevel.Medium, IsolationDetector.AutoDetectIsolationLevelFiles(Subjects.One, Subjects.Two));
		}

		[TestMethod]
		public void IsolationDetector_Files_High()
		{
			string working = Path.Combine(Subjects.DirEmpty, "working_1");
			Directory.CreateDirectory(working);
			string file1 = Path.Combine(working, "t1.dll");
			string file2 = Path.Combine(working, "t2.dll");
			File.Copy(Subjects.One, file1, true);
			File.Copy(Subjects.One, file2, true);

			try
			{
				Assert.AreEqual(AppDomainIsolationLevel.High, IsolationDetector.AutoDetectIsolationLevelFiles(file1, file2));
			}
			finally
			{
				File.Delete(file1);
				File.Delete(file2);
				Directory.Delete(working);
			}
		}

		private void AssertAssemblyInCurrentAppDomain(string name)
		{
			bool foundSubjectAssembly = false;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				foundSubjectAssembly |= assembly.FullName.Contains(name);
			}

			Assert.IsTrue(foundSubjectAssembly);
		}

		private void AssertAssemblyNotInCurrentAppDomain(string name)
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				Assert.IsFalse(assembly.FullName.Contains(name));
			}
		}
	}
}
