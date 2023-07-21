using Newtonsoft.Json;

namespace MicroService.Application.Models.Weather
{
    public class WeatherForecastResponseDto
    {
        [JsonProperty("Date")]
        public DateTime Date { get; set; } 

        [JsonProperty("TemperatureC")]
        public int TemperatureC { get; set; } 

        [JsonProperty("WeatherType")]
        public string WeatherType { get; set; } = string.Empty;
    }
}
