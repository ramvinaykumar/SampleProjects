using System.Runtime.Serialization;

namespace MicroService.Application.Helper
{
    [Serializable]
    public class JWTException : Exception
    {
        public JWTException()
        {
        }

        public JWTException(string message)
            : base(message)
        {
        }

        public JWTException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected JWTException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
