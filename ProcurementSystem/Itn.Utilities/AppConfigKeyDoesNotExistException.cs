using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Utilities
{
    [Serializable]
    public class AppConfigKeyDoesNotExistException : Exception
    {
        public AppConfigKeyDoesNotExistException() { }

        public AppConfigKeyDoesNotExistException(string message)
            : base(message) { }

        public AppConfigKeyDoesNotExistException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public AppConfigKeyDoesNotExistException(string message, Exception innerException)
            : base(message, innerException) { }

        public AppConfigKeyDoesNotExistException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected AppConfigKeyDoesNotExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
