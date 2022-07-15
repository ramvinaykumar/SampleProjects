using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using WebAPI.Proper.Request.Response.Common.Constants;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Enums;

namespace WebAPI.Proper.Request.Response.Repository.Helper
{
    public class ErrorsHelper
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
                Errors = new List<ErrorMessage>
                    {
                        errorMessage
                    },
                StatusCode = statusCode
            };
        }
    }
}
