namespace MicroService.Application.Models.Response
{
    public class MessageDto
    {
        public string MessageCode { get; set; }

        public string DisplayMessage { get; set; }

        public MessageDto(string messageCode, string displayMessage)
        {
            MessageCode = messageCode;
            DisplayMessage = displayMessage;
        }
    }
}
