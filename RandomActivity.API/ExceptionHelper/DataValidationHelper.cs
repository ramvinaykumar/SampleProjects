using RandomActivity.API.Models;

namespace RandomActivity.API.ExceptionHelper
{
    public static class DataValidationHelper
    {
        public static List<ExceptionsDto> GetExceptionMessageList(string exceptionMessage, string actionMethodName)
        {
            List<ExceptionsDto> list = new List<ExceptionsDto>();
            string[] array = exceptionMessage.Split(Environment.NewLine);
            foreach (string text in array)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    ExceptionsDto item = new ExceptionsDto("02", text, actionMethodName);
                    list.Add(item);
                }
            }

            return list;
        }
    }
}
