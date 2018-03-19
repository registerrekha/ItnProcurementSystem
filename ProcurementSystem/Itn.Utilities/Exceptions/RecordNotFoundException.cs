using System;
using System.Runtime.Serialization;

namespace Itn.Utilities.Exceptions
{
    [Serializable]
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() { }

        public RecordNotFoundException(string message)
            : base(message) { }

        public RecordNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public RecordNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public RecordNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected RecordNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
