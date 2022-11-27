namespace Admin.Announcement.Models
{
    public class GenericResponse<T>
    {
        public GenericResponse()
        {
            Errors = new List<ErrorMessage>();
            Result = default(T);
        }
        public bool IsValid { get; set; }
        public List<ErrorMessage> Errors { get; set; }
        public T Result { get; set; }
        public int StatusCode { get; set; }
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
        public ErrorType ErrorType { get; set; }
    }

    public enum ErrorType
    {
        NotFound = 404,
        Invalid = -99,
        UnAuthorized = 401,
        Forbidden = 403,
        NullException = -1,
        ServerError = 500,
    }
}
