using Admin.Announcement.Core.Constants;
using Admin.Announcement.Models;
using Microsoft.AspNetCore.Http;

namespace Admin.Announcement.Business.Handler
{
    public class BaseHandler
    {
        public GenericResponse<T> GetNotFoundResponse<T>()
        {
            return GetErrorResponse<T>(new ErrorMessage
            {
                ErrorType = ErrorType.NotFound,
                Message = ApplicationConstants.ResourceNotFound
            }
            , StatusCodes.Status404NotFound);
        }

        public GenericResponse<T> GetErrorResponse<T>(ErrorMessage errorMessage, int statusCode)
        {
            return new GenericResponse<T>
            {
                Errors = new System.Collections.Generic.List<ErrorMessage>
                    {
                        errorMessage
                    },
                StatusCode = statusCode
            };
        }
    }
}
