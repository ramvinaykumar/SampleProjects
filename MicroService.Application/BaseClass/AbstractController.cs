using MicroService.Application.Constant;
using MicroService.Application.Helper;
using MicroService.Application.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace MicroService.Application.BaseClass
{
    [ExcludeFromCodeCoverage]
    public abstract class AbstractController : ControllerBase
    {
        protected ResponseDto PrepareResponse(bool success, string serviceName, string returnCode, List<ExceptionsDto> errors, object payload)
        {
            ResultDto result = new ResultDto(serviceName, returnCode, errors, payload);
            ResponseDto response = new ResponseDto(success, new List<ResultDto>() { result });
            return response;
        }

        protected ResponseDto PrepareResponse(bool success, List<ResultDto> results)
        {
            ResponseDto response = new ResponseDto(success, results);
            return response;
        }

        protected ObjectResult PopulateException(Exception ex, string actionMethodName)
        {
            if (ex is JWTException)
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.ACTION_FILTER,
                    new List<ExceptionsDto> { new ExceptionsDto(ExceptionConstant.ACTION_FILTER, "Invalid JWT", actionMethodName) },
                    new List<object>());

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            else if (ex is ArgumentException)
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.INPUT_ERROR,
                    new List<ExceptionsDto> { new ExceptionsDto(ExceptionConstant.INPUT_ERROR, ex.Message, actionMethodName) },
                    new List<object>());

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            else if (ex is IdempotenceCheckException)
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.IDEMPOTENCE_ERROR,
                    new List<ExceptionsDto> { new ExceptionsDto(ExceptionConstant.IDEMPOTENCE_ERROR,
                    "An existing record was already found", actionMethodName) },
                    new List<object>());

                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            else if (ex is NotFoundException)
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.INPUT_ERROR,
                    new List<ExceptionsDto> { new ExceptionsDto(ExceptionConstant.INPUT_ERROR, "Data not found", actionMethodName) },
                    new List<object>());

                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            else if (ex is RoutineErrorException)
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.ROUTINE_ERROR,
                    new List<ExceptionsDto> { new ExceptionsDto(ExceptionConstant.ROUTINE_ERROR, ex.Message, actionMethodName) },
                    new List<object>());

                return StatusCode(StatusCodes.Status503ServiceUnavailable, response);
            }
            else if (ex is FluentValidationException)
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.INPUT_ERROR,
                    DataValidationHelper.GetExceptionMessageList(ex.Message, actionMethodName),
                    new List<object>());

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            else
            {
                ResponseDto response = PrepareResponse(false,
                    actionMethodName,
                    ExceptionConstant.TECHNICAL_ERROR,
                    new List<ExceptionsDto> { new ExceptionsDto(ExceptionConstant.TECHNICAL_ERROR, ex.Message, actionMethodName) },
                    new List<object>());

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
