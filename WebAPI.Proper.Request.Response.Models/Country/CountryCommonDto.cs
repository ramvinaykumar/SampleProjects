using Newtonsoft.Json;

namespace WebAPI.Proper.Request.Response.Models.Country
{
    public class CountryCommonDto
    {
        [JsonProperty("CountryID")]
        public int CountryID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("ISO3")]
        public string ISO3 { get; set; } = string.Empty;

        [JsonProperty("ISO2")]
        public string ISO2 { get; set; } = string.Empty;

        [JsonProperty("Code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("PhoneCode")]
        public string PhoneCode { get; set; } = string.Empty;

        [JsonProperty("Capital")]
        public string Capital { get; set; } = string.Empty;

        [JsonProperty("Currency")]
        public string Currency { get; set; } = string.Empty;

        [JsonProperty("CurrencyName")]
        public string CurrencyName { get; set; } = string.Empty;

        [JsonProperty("CurrencySymbol")]
        public string CurrencySymbol { get; set; } = string.Empty;

        [JsonProperty("Region")]
        public string Region { get; set; } = string.Empty;

        [JsonProperty("Subregion")]
        public string Subregion { get; set; } = string.Empty;
    }
}
