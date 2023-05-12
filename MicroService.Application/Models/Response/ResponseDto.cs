namespace MicroService.Application.Models.Response
{
    public class ResponseDto
    {
        public bool Success { get; set; }

        public List<ResultDto> Result { get; set; }

        public ResponseDto(bool success, List<ResultDto> result)
        {
            Success = success;
            Result = result;
        }
    }
}
