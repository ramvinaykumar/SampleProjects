using Newtonsoft.Json;

namespace RandomActivity.API.Models
{
    public class ValidatePaymentInputRequestDto
    {
        [JsonProperty("bankName")]
        public string BankName { get; set; } = string.Empty;

        [JsonProperty("bankAccountNumber")]
        public string BankAccountNumber { get; set; } = string.Empty;
    }
}
