using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Exceptions
{
    public class ExternalResourcesException : Exception
    {
        public ExternalResourcesException()
        {
        }

        public ExternalResourcesException(string message)
            : base(message)
        {
        }

        public ExternalResourcesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}



