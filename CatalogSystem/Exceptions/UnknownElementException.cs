using System;
using System.Runtime.Serialization;

namespace CatalogSystem.Exceptions
{
    [Serializable]
    public class UnknownElementException : CatalogSystemException
    {
        public UnknownElementException()
        {
        }

        public UnknownElementException(string message) : base(message)
        {
        }

        public UnknownElementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownElementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
