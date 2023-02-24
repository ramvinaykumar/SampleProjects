namespace RandomActivity.API.Models
{
    public class ExceptionsDto
    {
        public string MessageCode { get; set; }

        public string DisplayMessage { get; set; }

        public string ServiceName { get; set; }

        public ExceptionsDto(string messageCode, string displayMessage, string serviceName)
        {
            MessageCode = messageCode;
            DisplayMessage = displayMessage;
            ServiceName = serviceName;
        }
    }
}
