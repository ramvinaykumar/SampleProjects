using AutoMapper;
using MicroService.Application.BaseClass;
using MicroService.Application.Common.Helpers;
using MicroService.Application.Constant;
using MicroService.Application.Models.Response;
using MicroService.Application.Models.Weather;
using MicroService.Application.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : AbstractController
    {
       // private readonly IMapper _mapper;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastDataService _weatherForecastDataService;

        private string controllerName = nameof(WeatherForecastController);
        private string actionMethodName = string.Empty;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastDataService weatherForecastDataService
           // , IMapper mapper
            )
        {
            _logger = logger;
            _weatherForecastDataService = weatherForecastDataService;
            //_mapper = mapper;
        }

        /// <summary>
        /// Get weather forecast data
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeatherForecast")]
        [ProducesResponseType(typeof(WeatherForecastResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWeatherForecastAsync()
        {
            var applicationName = "GetWeatherForecast";
            actionMethodName = nameof(GetWeatherForecastAsync);
            var fullActionName = CommonHelper.GetFullActionMethodName(controllerName, actionMethodName);
            var errorList = new List<ExceptionsDto>();

            try
            {
                var result = await _weatherForecastDataService.GetWeatherForecastAsync();
                // var responseDto = _mapper.Map(result, new WeatherForecastResponseDto());

                ResponseDto response = PrepareResponse(true, applicationName, ReturnCode.Success, errorList, result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                      LoggerConstants.LogError,
                      ex.Message,
                      applicationName,
                      controllerName,
                      actionMethodName,
                      ex.StackTrace);

                return PopulateException(ex, applicationName);
            }
        }
    }
}