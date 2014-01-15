using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using BitDiffer.Common.Configuration;
using BitDiffer.Common.Exceptions;

namespace BitDiffer.Common.Misc
{
	public class ProgramArguments
	{
        private bool _help;
        private string _reportFile;
        private string _rawFile;
        private string _logFile;
        private ComparisonSet _set = new ComparisonSet();

		public ProgramArguments()
		{
			_reportFile = GetFilePath("comparison.xml");
            _set.Filter.ChangedItemsOnly = true;
		}

        public bool Help
        {
            get { return _help; }
        }

        public string ReportFile
        {
            get { return _reportFile; }
        }

        public string RawFile
        {
            get { return _rawFile; }
        }

        public string LogFile
        {
            get { return _logFile; }
        }

        public ComparisonSet ComparisonSet
		{
			get { return _set; }
		}

		public void Parse(bool required, string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].StartsWith("-"))
				{
				    ParseOption(args, ref i);
				}
				else
				{
					string fileName = args[i];

                    if (i == 0 && fileName == Assembly.GetEntryAssembly().Location)
                    {
                        continue;
                    }
                    else if (Path.GetExtension(fileName).ToLower() == ".cset")
					{
						using (FileStream fs = File.Open(fileName, FileMode.Open))
						{
							XmlSerializer xs = new XmlSerializer(typeof(ComparisonSet));
							_set = (ComparisonSet)xs.Deserialize(fs);
						    _set.FileName = fileName;
						}
					}
					else
					{
						_set.Items.Add(fileName);
					}
				}
			}

		    Validate(required);
		}

		private void ParseOption(string[] args, ref int i)
		{
			switch (args[i])
			{
				case "-?":
					_help = true;
			        break;
				case "-out":
					_reportFile = GetFilePath(args[++i]);
					break;
				case "-raw":
					_rawFile = GetFilePath(args[++i]);
					break;
                case "-log":
                    _logFile = GetFilePath(args[++i]);
                    break;
                case "-dirs":
					_set.Mode = ComparisonMode.CompareDirectories;
					break;
                case "-recurse":
                    _set.RecurseSubdirectories = true;
                    break;
                case "-all":
					_set.Filter.ChangedItemsOnly = false;
					break;
                case "-publiconly":
                    _set.Filter.IncludePublic = true;
                    _set.Filter.IncludeProtected = false;
                    _set.Filter.IncludeInternal = false;
                    _set.Filter.IncludePrivate = false;
                    break;
				case "-xpublic":
			        _set.Filter.IncludePublic = false;
					break;
                case "-xprotected":
                    _set.Filter.IncludeProtected = false;
                    break;
                case "-xinternal":
                    _set.Filter.IncludeInternal = false;
                    break;
                case "-xprivate":
                    _set.Filter.IncludePrivate = false;
                    break;
                case "-noattrs":
                    _set.Filter.IgnoreAssemblyAttributeChanges = true;
					break;
				case "-noimpl":
                    _set.Filter.CompareMethodImplementations = false;
					break;
				case "-nomulti":
                    _set.Config.Multithread = false;
					break;
                case "-gacfirst":
                    _set.Config.TryResolveGACFirst = true;
					break;
				case "-isolation":
			        ParseIsolationLevel(args[++i]);
					break;
				case "-execution":
                    _set.Config.UseReflectionOnlyContext = false;
					break;
                case "-ref":
                    _set.Config.ReferenceDirectories = GetMultiPath(args[++i]);
                    break;
                case "-refdirs":
                    _set.Config.ReferenceDirectories = args[++i];
                    break;
                default:
			        throw new ArgumentParserException("Unknown option {0}", args[i]);
			}
		}

		private void ParseIsolationLevel(string level)
		{
			// Dont use Enum.Parse, obfuscation will break it
			switch (level.ToLower())
			{
				case "low":
                    _set.Config.IsolationLevel = AppDomainIsolationLevel.Low;
			        break;
				case "medium":
                    _set.Config.IsolationLevel = AppDomainIsolationLevel.Medium;
                    break;
                case "high":
                    _set.Config.IsolationLevel = AppDomainIsolationLevel.High;
                    break;
                default:
			        throw new ArgumentParserException("Unknown isolation level {0}", level);
            }
		}

        private string GetMultiPath(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentParserException("File " + path + " was not found");
            }

            string[] paths = File.ReadAllLines(path);
            List<string> trimPaths = new List<string>();

            foreach (string p in paths)
            {
                if (!string.IsNullOrEmpty(p))
                {
                    trimPaths.Add(p.Trim());
                }
            }

            return string.Join(";", trimPaths.ToArray());
        }

		private string GetFilePath(string fileName)
		{
			return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), fileName));
		}

        private void Validate(bool required)
        {
            if (required && _set.Items.Count == 0)
            {
                throw new ArgumentParserException("No filenames specified");
            }

            if (_reportFile != null)
            {
                if (!Directory.Exists(Path.GetDirectoryName(_reportFile)))
                {
                    throw new ArgumentParserException("Directory for report does not exist: '{0}'", Path.GetDirectoryName(_reportFile));
                }
            }

            if (_rawFile != null)
            {
                if (!Directory.Exists(Path.GetDirectoryName(_rawFile)))
                {
                    throw new ArgumentParserException("Directory for report does not exist: '{0}'", Path.GetDirectoryName(_rawFile));
                }
            }

            if (_set.Mode == ComparisonMode.CompareDirectories)
            {
                foreach (string dir in _set.Items)
                {
                    if (!Directory.Exists(dir))
                    {
                        throw new ArgumentParserException("Directory does not exist or is not a directory: '{0}'", dir);
                    }
                }
            }
            else
            {
                if (_set.RecurseSubdirectories)
                {
                    throw new ArgumentParserException("Cannot specify to recurse subdirectories if not in directory mode");
                }

                foreach (string file in _set.Items)
                {
                    if (!File.Exists(file))
                    {
                        throw new ArgumentParserException("File does not exist: '{0}'", file);
                    }
                }
            }
        }
	}
}
