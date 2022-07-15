using System.Collections.Generic;
using WebAPI.Proper.Request.Response.Models.Enums;

namespace WebAPI.Proper.Request.Response.Models
{
    public class GenericResponse<T>
    {
        public GenericResponse()
        {
            Errors = new List<ErrorMessage>();
            Result = default(T);
        }

        public int Count { get; set; }
        public int StatusCode { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public List<ErrorMessage> Errors { get; set; }
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
        public ErrorType ErrorType { get; set; }
    }
}
