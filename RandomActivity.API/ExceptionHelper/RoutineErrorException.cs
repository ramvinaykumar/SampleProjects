using System.Runtime.Serialization;

namespace RandomActivity.API.ExceptionHelper
{
    [Serializable]
    public class RoutineErrorException : Exception
    {
        public RoutineErrorException()
        {
        }

        public RoutineErrorException(string message)
            : base(message)
        {
        }

        public RoutineErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RoutineErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
