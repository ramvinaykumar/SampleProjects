using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using RandomActivity.API.ExceptionHelper;
using RandomActivity.API.Models;
using System.Diagnostics.CodeAnalysis;

namespace RandomActivity.API
{
    [ExcludeFromCodeCoverage]
    public abstract class AbstractController : ControllerBase
    {
        protected ResponseDto PrepareResponse(bool success, string serviceName, string returnCode, List<ExceptionsDto> errors, object payload)
        {
            ResultDto item = new ResultDto(serviceName, returnCode, errors, payload);
            return new ResponseDto(success, new List<ResultDto> { item });
        }

        protected ResponseDto PrepareResponse(bool success, List<ResultDto> results)
        {
            return new ResponseDto(success, results);
        }

        protected ObjectResult PopulateException(Exception ex, string actionMethodName)
        {
            if (ex is JWTException)
            {
                ResponseDto value = PrepareResponse(success: false, actionMethodName, "01", new List<ExceptionsDto>
                {
                    new ExceptionsDto("01", "Invalid JWT", actionMethodName)
                }, new List<object>());
                return StatusCode(400, value);
            }

            if (ex is ArgumentException)
            {
                ResponseDto value2 = PrepareResponse(success: false, actionMethodName, "02", new List<ExceptionsDto>
                {
                    new ExceptionsDto("02", ex.Message, actionMethodName)
                }, new List<object>());
                return StatusCode(400, value2);
            }

            if (ex is IdempotenceCheckException)
            {
                ResponseDto value3 = PrepareResponse(success: false, actionMethodName, "98", new List<ExceptionsDto>
                {
                    new ExceptionsDto("98", "An existing record was already found", actionMethodName)
                }, new List<object>());
                return StatusCode(409, value3);
            }

            if (ex is NotFoundException)
            {
                ResponseDto value4 = PrepareResponse(success: false, actionMethodName, "02", new List<ExceptionsDto>
                {
                    new ExceptionsDto("02", "Data not found", actionMethodName)
                }, new List<object>());
                return StatusCode(404, value4);
            }

            if (ex is RoutineErrorException)
            {
                ResponseDto value5 = PrepareResponse(success: false, actionMethodName, "31", new List<ExceptionsDto>
                {
                    new ExceptionsDto("31", ex.Message, actionMethodName)
                }, new List<object>());
                return StatusCode(503, value5);
            }

            if (ex is FluentValidationException)
            {
                ResponseDto value6 = PrepareResponse(success: false, actionMethodName, "02", DataValidationHelper.GetExceptionMessageList(ex.Message, actionMethodName), new List<object>());
                return StatusCode(400, value6);
            }

            ResponseDto value7 = PrepareResponse(success: false, actionMethodName, "99", new List<ExceptionsDto>
            {
                new ExceptionsDto("99", ex.Message, actionMethodName)
            }, new List<object>());
            return StatusCode(500, value7);
        }
    }
}
