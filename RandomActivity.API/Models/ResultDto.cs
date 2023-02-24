namespace RandomActivity.API.Models
{
    public class ResultDto
    {
        public string ServiceName { get; set; }

        public string ReturnCode { get; set; }

        public List<ExceptionsDto> Errors { get; set; }

        public object Payload { get; set; }

        public ResultDto(string serviceName, string returnCode, List<ExceptionsDto> errors, object payload)
        {
            ServiceName = serviceName;
            ReturnCode = returnCode;
            Errors = errors;
            Payload = payload;
        }
    }
}
