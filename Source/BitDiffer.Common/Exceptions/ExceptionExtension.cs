using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BitDiffer.Common.Exceptions
{
    public static class ExceptionExtension
    {
        public static string GetNestedExceptionMessage(this Exception ex)
        {
            var sb = new StringBuilder();

            while (ex != null)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" -> ");
                }

                sb.Append(ex.Message);

                if (ex is System.Reflection.ReflectionTypeLoadException)
                {
                    var typeLoadException = ex as ReflectionTypeLoadException;
                    var loaderExceptions = typeLoadException.LoaderExceptions;
                    foreach (Exception e in typeLoadException.LoaderExceptions)
                    {
                        
                        var exFileNotFound = e as FileNotFoundException;
                        if (exFileNotFound != null)
                        {
                            sb.Append(string.Format("\nFile:{0} FusionLog:{1} Message:{2}",
                                exFileNotFound.FileName, exFileNotFound.FusionLog, exFileNotFound.Message));
                        }
                        else
                        {
                            sb.Append("\n" + e.Message);
                        }
                    }
                }
                else if (ex is FileNotFoundException)
                {
                    var exFileNotFound = ex as FileNotFoundException;
                    sb.Append(string.Format("\nFile:{0} FusionLog:{1} Message:{2}",
                        exFileNotFound.FileName, exFileNotFound.FusionLog, exFileNotFound.Message));
                }

                ex = ex.InnerException;
            }
            return sb.ToString();
        }
    }
}
