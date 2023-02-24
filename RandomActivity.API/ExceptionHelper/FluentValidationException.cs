using System.Runtime.Serialization;

namespace RandomActivity.API.ExceptionHelper
{
    [Serializable]
    public class FluentValidationException : Exception
    {
        public FluentValidationException()
        {
        }

        public FluentValidationException(string message)
            : base(message)
        {
        }

        public FluentValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FluentValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
