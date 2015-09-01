using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using BitDiffer.Common.Exceptions;
using BitDiffer.Common.Model;
using BitDiffer.Common.Utility;
using BitDiffer.Common.Misc;
using BitDiffer.Common.Configuration;

namespace BitDiffer.Extractor
{
	public class AssemblyExtractor : MarshalByRefObject
	{
		private DiffConfig _config;
		private string _assemblyFile;

		static AssemblyExtractor()
		{
			if (AppDomain.CurrentDomain.FriendlyName.StartsWith(Constants.ExtractionDomainPrefix))
			{
				AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(domain_AssemblyResolve);
			}
		}

		public AssemblyDetail ExtractFrom(string assemblyFile, DiffConfig config)
		{
			Assembly assembly;

			_assemblyFile = assemblyFile;
			_config = config;

			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);

		    try
		    {
		        if (config.UseReflectionOnlyContext)
		        {
		            Log.Info("Loading assembly {0} (ReflectionContext)", assemblyFile);
		            assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
		        }
		        else
		        {
		            Log.Info("Loading assembly {0}", assemblyFile);
		            assembly = Assembly.LoadFrom(assemblyFile);
		        }

		        return new AssemblyDetail(assembly);
		    }
            catch (Exception ex)
            {
                var errMessage = ex.GetNestedExceptionMessage();
                Log.Error(errMessage);
                throw new Exception(errMessage);
		    }
			finally
			{
				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);
			}
		}

		private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			Assembly assembly = null;

			Log.Verbose("Attempting to resolve assembly reference '{0}'", args.Name);

			Assembly[] list = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies();
			foreach (Assembly asm in list)
			{
				if (asm.FullName == args.Name)
				{
					return asm;
				}
			}

            if (_config.ReferenceDirectories != null)
            {
                string[] dirs = _config.ReferenceDirectories.Split(';');

                foreach (string dir in dirs)
                {
                    assembly = LoadAssemblyFromFile(args.Name, dir);

                    if (assembly != null)
                    {
                        break;
                    }
                }
            }

            if (assembly == null)
            {
                if (_config.TryResolveGACFirst)
                {
                    assembly = LoadAssemblyFromGAC(args.Name);

                    if (assembly == null)
                    {
                        assembly = LoadAssemblyFromFile(args.Name);
                    }
                }
                else
                {
                    assembly = LoadAssemblyFromFile(args.Name);

                    if (assembly == null)
                    {
                        assembly = LoadAssemblyFromGAC(args.Name);
                    }
                }
            }

			if (assembly == null)
			{
				Log.Error("Could not resolve assembly reference '{0}'.", args.Name);
			}
			else
			{
				Log.Verbose("Assembly loaded.");
			}

			return assembly;
		}

		private static Assembly domain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			// Apparently a bug in .NET... it has trouble resolving assemblies that are already loaded!
			Assembly[] list = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly asm in list)
			{
				if (asm.FullName == args.Name)
				{
					Log.Verbose("Resolving assembly {0} that is already loaded", asm.FullName);
					return asm;
				}
			}

			return Assembly.Load(args.Name);
		}

		private Assembly LoadAssemblyFromGAC(string name)
		{
			Log.Verbose("Searching for assembly '{0}' in GAC", name);

			try
			{
				Assembly assembly = Assembly.ReflectionOnlyLoad(name);
				if (assembly != null)
				{
					Log.Verbose("Resolving assembly {0} from GAC", name);
					return assembly;
				}
			}
			catch
			{
			}

			return null;
		}

        private Assembly LoadAssemblyFromFile(string name)
        {
            return LoadAssemblyFromFile(name, Path.GetDirectoryName(_assemblyFile));
        }

		private Assembly LoadAssemblyFromFile(string name, string dir)
		{
			// Try to load from file
			if (name.IndexOf(',') > 0)
			{
				name = name.Substring(0, name.IndexOf(','));
			}

            string fileName = Path.Combine(dir, name);

			if (!fileName.EndsWith(".dll"))
			{
				fileName += ".dll";
			}

			Log.Verbose("Searching for assembly file '{0}'", fileName);

			if (File.Exists(fileName))
			{
				Log.Verbose("Resolving assembly {0} from file", name);
				return Assembly.ReflectionOnlyLoadFrom(fileName);
			}

			return null;
		}

		public void AddTraceListener(TraceListener listener)
		{
			Trace.Listeners.Add(listener);
		}
	}
}
