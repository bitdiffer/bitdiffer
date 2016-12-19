using System;
using System.IO;

namespace BitDiffer.Tests
{
	internal static class Subjects
	{
	    private static readonly string BasePath;

	    static Subjects()
	    {
	        var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
	        BasePath = Path.GetFullPath(directory.Name == "Release" || directory.Name == "Debug" 
                ? "..\\..\\.." 
                : "..\\..\\..\\..");
	    }

	    internal static string DirEmpty => Path.Combine(BasePath, "BitDiffer.Tests.bin");
	    internal static string DirOne => Path.Combine(DirEmpty, "1");
        internal static string DirTwo => Path.Combine(DirEmpty, "2");

        internal static string NamespaceOne => "BitDiffer.Tests.Subject";

        internal static string One => Path.Combine(DirOne, "BitDiffer.Tests.Subject.dll");
	    internal static string Two => Path.Combine(DirTwo, "BitDiffer.Tests.Subject.dll");
	}
}
