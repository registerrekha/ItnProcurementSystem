using System;
using System.Runtime.Serialization;

namespace Itn.Utilities.Enums
{

    [Serializable]
    public class ServiceNotOkException : Exception
    {
        public HttpActionServiceResultType HttpResultType { get; set; }
        public ServiceNotOkException() { }

        public ServiceNotOkException(string message, HttpActionServiceResultType resultType)
            : base(message)
        {
            HttpResultType = resultType;
        }

        public ServiceNotOkException(string format, HttpActionServiceResultType resultType, params object[] args)
            : base(string.Format(format, args))
        {
            HttpResultType = resultType;
        }

        public ServiceNotOkException(string message, HttpActionServiceResultType resultType, Exception innerException)
            : base(message, innerException)
        {
            HttpResultType = resultType;
        }

        public ServiceNotOkException(string format, HttpActionServiceResultType resultType, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
            HttpResultType = resultType;
        }

        protected ServiceNotOkException(HttpActionServiceResultType resultType, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            HttpResultType = resultType;
        }
    }
}
