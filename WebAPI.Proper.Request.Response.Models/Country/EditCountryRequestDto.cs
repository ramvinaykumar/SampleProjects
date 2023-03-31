using Newtonsoft.Json;

namespace WebAPI.Proper.Request.Response.Models.Country
{
    public class EditCountryRequestDto : CountryCommonDto
    {
        [JsonProperty("CountryID")]
        public int CountryID { get; set; }
    }
}
