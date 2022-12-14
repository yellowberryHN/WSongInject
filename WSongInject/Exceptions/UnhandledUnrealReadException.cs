using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSongInject.Exceptions
{
    public class UnhandledUnrealReadException : Exception
    {
        public UnhandledUnrealReadException()
        {
        }

        public UnhandledUnrealReadException(string message)
            : base(message)
        {
        }

        public UnhandledUnrealReadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
