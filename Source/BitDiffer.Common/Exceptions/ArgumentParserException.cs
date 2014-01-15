using System;
using System.Collections.Generic;
using System.Text;

namespace BitDiffer.Common.Exceptions
{
    public class ArgumentParserException : Exception
    {
        public ArgumentParserException(string message)
            : base(message)
        {
        }

        public ArgumentParserException(string format, params object[] parms)
            : base(string.Format(format, parms))
        {
        }
    }
}
