using Newtonsoft.Json;

namespace RandomActivity.API.Models
{
    public class ValidatePaymentInputResponseDto
    {
        [JsonProperty("guid")]
        public string Guid { get; set; } = string.Empty;

        [JsonProperty("isValidInput")]
        public bool IsValidInput { get; set; }
    }
}
