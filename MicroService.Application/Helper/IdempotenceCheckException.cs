using System.Runtime.Serialization;

namespace MicroService.Application.Helper
{
    [Serializable]
    public class IdempotenceCheckException : Exception
    {
        private const string ExceptionMessageTemplate = "Request with id {0} has already been processed.";

        public IdempotenceCheckException(string requestId)
            : base($"Request with id {requestId} has already been processed.")
        {
        }

        public IdempotenceCheckException()
        {
        }

        public IdempotenceCheckException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected IdempotenceCheckException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
