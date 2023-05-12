using MicroService.Application.Models.Response;
using MicroService.Application.Models.Weather;
using MicroService.Application.Repository.Interface;

namespace MicroService.Application.Repository.Service
{
    public class WeatherForecastDataService : IWeatherForecastDataService
    {
        private readonly ILogger _logger;

        public WeatherForecastDataService(ILogger<WeatherForecastDataService> logger)
        {
            _logger = logger;
        } 


        public async Task<IEnumerable<WeatherForecastResponseDto>> GetWeatherForecastAsync()
        {
            string[] weatherSummaries = new[]
            {
                 "Freezing",
                 "Bracing",
                 "Chilly",
                 "Cool",
                 "Mild",
                 "Warm",
                 "Balmy",
                 "Hot",
                 "Sweltering",
                 "Scorching"
             };

            _logger.LogInformation("---Start in GetWeatherForecastAsync---");
            _logger.LogInformation("---WeatherForecast Data ---" + Newtonsoft.Json.JsonConvert.SerializeObject(weatherSummaries));

            List<ExceptionsDto> exceptionsDtoList = new List<ExceptionsDto>();
            var responseDto = new List<WeatherForecastResponseDto>();

            responseDto = Enumerable.Range(1, 15).Select(index => new WeatherForecastResponseDto
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                WeatherType = weatherSummaries[Random.Shared.Next(weatherSummaries.Length)]
            }).ToList();

            return responseDto;


            //_logger.LogInformation("---Start in RetrieveA55MyActivitiesDetails---plainTransactionNumber---" + plainTransactionNumber);
            //_logger.LogInformation("---Start in RetrieveA55MyActivitiesDetails---requestDto.Uid---" + requestDto.Uid);
        }
    }
}
