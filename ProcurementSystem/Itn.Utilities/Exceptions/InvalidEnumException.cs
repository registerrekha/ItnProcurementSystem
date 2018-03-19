using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Utilities.Exceptions
{
    [Serializable]
    public class InvalidEnumException : Exception
    {
        public InvalidEnumException() { }

        public InvalidEnumException(string message)
            : base(message) { }

        public InvalidEnumException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public InvalidEnumException(string message, Exception innerException)
            : base(message, innerException) { }

        public InvalidEnumException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected InvalidEnumException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
