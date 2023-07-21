using Newtonsoft.Json;

namespace WebAPI.Proper.Request.Response.Models.Country
{
    public class CountryResponseDto : CountryCommonDto
    {
        [JsonProperty("IsDeleted")]
        public string IsDeleted { get; set; } = string.Empty;
    }
}
