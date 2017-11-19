using System;
using System.Runtime.Serialization;

namespace CatalogSystem.Exceptions
{
    [Serializable]
    public class EntityWriterNotFoundedException : CatalogSystemException
    {
        public EntityWriterNotFoundedException()
        {
        }

        public EntityWriterNotFoundedException(string message) : base(message)
        {
        }

        public EntityWriterNotFoundedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityWriterNotFoundedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
