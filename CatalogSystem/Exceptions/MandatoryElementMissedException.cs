using System;
using System.Runtime.Serialization;

namespace CatalogSystem.Exceptions
{
    [Serializable]
    public class MandatoryElementMissedException : CatalogSystemException
    {
        public MandatoryElementMissedException()
        {
        }

        public MandatoryElementMissedException(string message) : base(message)
        {
        }

        public MandatoryElementMissedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MandatoryElementMissedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}