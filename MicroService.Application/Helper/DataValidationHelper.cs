using MicroService.Application.Constant;
using MicroService.Application.Models.Response;

namespace MicroService.Application.Helper
{
    public static class DataValidationHelper
    {
        public static List<ExceptionsDto> GetExceptionMessageList(string exceptionMessage, string actionMethodName)
        {
            List<ExceptionsDto> exceptionMessageList = new List<ExceptionsDto>();
            string[] messages = exceptionMessage.Split(Environment.NewLine);

            foreach (var message in messages)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ExceptionsDto exception = new ExceptionsDto(ExceptionConstant.INPUT_ERROR, message, actionMethodName);
                    exceptionMessageList.Add(exception);
                }
            }

            return exceptionMessageList;
        }
    }
}
