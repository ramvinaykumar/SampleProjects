using MicroService.Application.Models.Weather;

namespace MicroService.Application.Repository.Interface
{
    public interface IWeatherForecastDataService
    {
        Task<IEnumerable<WeatherForecastResponseDto>> GetWeatherForecastAsync();
    }
}
