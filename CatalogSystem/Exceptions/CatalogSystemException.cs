using System;
using System.Runtime.Serialization;

namespace CatalogSystem.Exceptions
{
    [Serializable]
    public class CatalogSystemException : Exception
    {
        public CatalogSystemException()
        {
        }

        public CatalogSystemException(string message) : base(message)
        {
        }

        public CatalogSystemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CatalogSystemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
