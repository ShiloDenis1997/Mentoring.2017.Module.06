using System;
using System.Runtime.Serialization;

namespace CatalogSystem.Exceptions
{
    [Serializable]
    public class MandatoryAttributeMissedException : CatalogSystemException
    {
        public MandatoryAttributeMissedException()
        {
        }

        public MandatoryAttributeMissedException(string message) : base(message)
        {
        }

        public MandatoryAttributeMissedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MandatoryAttributeMissedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}