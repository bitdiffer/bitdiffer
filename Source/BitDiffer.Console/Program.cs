using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using BitDiffer.Common.Exceptions;
using BitDiffer.Common.Misc;
using BitDiffer.Common.TraceListeners;
using BitDiffer.Common.Utility;
using BitDiffer.Core;

namespace BitDiffer.ConsoleApp
{
	// -out "..\..\..\..\Sets\comparison.xml" -raw "..\..\..\..\Sets\raw.xml" "..\..\..\BitDiffer.Tests.bin\1\BitDiffer.Tests.Subject.dll"  "..\..\..\BitDiffer.Tests.bin\2\BitDiffer.Tests.Subject.dll"
	// -dirs -out "..\..\..\..\Sets\comparison.html" -raw "..\..\..\..\Sets\raw.xml" "..\..\..\BitDiffer.Tests.bin\1"  "..\..\..\BitDiffer.Tests.bin\2"
	// -out "..\..\..\..\Sets\comparison.html" -raw "..\..\..\..\Sets\raw.xml" "..\..\..\..\Sets\UnitTestData.cset"

	class Program
	{
        private static StreamWriter _logFileWriter;

		static int Main(string[] arguments)
		{
			foreach (TraceListener tl in Trace.Listeners)
			{
				if (tl is RelayingTraceListener)
				{
					((RelayingTraceListener)tl).Message += new EventHandler<TraceEventArgs>(Program_Message);
				}
			}

			Log.Info("BitDiffer Console");
			Log.Info("Version {0} ({1:d})", Assembly.GetExecutingAssembly().GetName().Version, DateTime.Today);
			Log.Info("");

            ProgramArguments args = new ProgramArguments();

		    try
		    {
                args.Parse(true, arguments);
		    }
		    catch (ArgumentParserException ex)
		    {
                string message = "ERROR: " + ex.Message;

                if (args.LogFile != null)
                {
                    File.WriteAllText(args.LogFile, message);
                }

                Console.WriteLine(message);
		        Console.WriteLine();

                Usage();

		        return -1;
		    }

            if (args.Help)
            {
                Usage();

                Console.WriteLine("Press any key to exit...");
                Console.Read();

                return 1;
            }

            if (args.LogFile != null)
            {
                _logFileWriter = new StreamWriter(args.LogFile, false);
            }

            AssemblyComparison ac = new AssemblyComparer().CompareAssemblies(args.ComparisonSet);

            ac.WriteReport(args.ReportFile, AssemblyComparisonXmlWriteMode.Normal);

            if (args.RawFile != null)
			{
                ac.WriteXmlReport(args.RawFile, AssemblyComparisonXmlWriteMode.Raw);
			}

            Log.Info("Done!");

            if (_logFileWriter != null)
            {
                _logFileWriter.Flush();
                _logFileWriter.Close();
            }

		    return 0;
		}

		static void Program_Message(object sender, TraceEventArgs e)
		{
            string text;

			if (e.Level >= TraceLevel.Info)
			{
                text = e.Message;
			}
			else
			{
				text = e.Level.ToString().ToUpper() + " : " + e.Message;
			}

            Console.WriteLine(text);

            if (_logFileWriter != null && _logFileWriter.BaseStream.CanWrite)
            {
                _logFileWriter.WriteLine(text);
            }
		}

		private static void Usage()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine();
			Console.WriteLine("Compare a set of files or directories:");
			Console.WriteLine("  BitDiffer.Console.exe [options] \"file1\" \"file2\" ... \"fileN\"");
			Console.WriteLine();
			Console.WriteLine("Compare using files and options in existing comparison set project file:");
			Console.WriteLine("  BitDiffer.Console.exe -out \"report.html\" \"file.cset\"");
			Console.WriteLine();
			Console.WriteLine("General Options:");
			Console.WriteLine();
			Console.WriteLine("     -dirs                     Input files are directory names (compare directories)");
            Console.WriteLine("     -recurse                  Recurse all subdirectories (only valid with -dirs)");
            Console.WriteLine("     -out \"report.html\"        Output comparison report to specified file (.HTML or .XML)");
			Console.WriteLine("     -raw \"details.xml\"        Output raw details to given file");
            Console.WriteLine("     -log \"messages.log\"       Output log messages to given file");
			Console.WriteLine();
			Console.WriteLine("Filter Options:");
			Console.WriteLine();
			Console.WriteLine("     -all                      Report all items instead of just changed items");
			Console.WriteLine("     -xpublic                  Exclude public types and members");
            Console.WriteLine("     -xprotected               Exclude protected types and members");
            Console.WriteLine("     -xinternal                Exclude internal types and members");
            Console.WriteLine("     -xprivate                 Exclude private types and members");
            Console.WriteLine("     -noimpl                   Do not compare method and property implementations");
			Console.WriteLine("     -noattrs                  Ignore changes in assembly attributes");
			Console.WriteLine();
			Console.WriteLine("Advanced Options:");
			Console.WriteLine();
			Console.WriteLine("     -nomulti                  Do not multithread");
			Console.WriteLine("     -gacfirst                 Try to resolve references from the GAC first");
			Console.WriteLine("     -isolation level          Set isolation level. Valid values are \"medium\" or \"high\". (Use with caution)");
			Console.WriteLine("     -execution                Load into execution context rather than reflection context");
            Console.WriteLine("     -refdirs \"dirs\"           Semicolon-seperated list of reference directories");
			Console.WriteLine();
		}
	}
}
