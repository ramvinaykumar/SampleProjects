using System;

namespace Application.API
{
    /// <summary>
    /// Model class for weather forecast
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Forecast date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Forecast temperature in C
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Forecast temperature in F
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Forecast summary
        /// </summary>
        public string Summary { get; set; }
    }
}
