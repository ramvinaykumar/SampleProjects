using Microsoft.AspNetCore.Mvc;
using RandomActivity.API.Services;
using RandomActivity.API.Models;
using RandomActivity.API.Models.Constants;
using AutoMapper;

namespace RandomActivity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyActivityController : AbstractController
    {
        private readonly ILogger<MyActivityController> _logger;
        private readonly IMapper _mapper;
        private readonly IMyActivityService _myActivityService;
        private string actionMethodName = String.Empty;
        private string controllerName = nameof(MyActivityController);
        private readonly IA55ValidatePaymentInputService _a55ValidatePaymentInputService;

        public MyActivityController(ILogger<MyActivityController> logger,
           IMapper mapper, IMyActivityService backendService, IA55ValidatePaymentInputService a55ValidatePaymentInputService)
        {
            _logger = logger;
            _mapper = mapper;
            _myActivityService = backendService;
            _a55ValidatePaymentInputService = a55ValidatePaymentInputService;   
        }

        [HttpGet("GetMyActivity")]
        public async Task<RandomActivities?> Get()
        {
            var res = await _myActivityService.GetNewActity();

            if (res != null)
            {
                return new RandomActivities()
                {
                    Activity = res.Activity,
                    Type = res.Type
                };
            }
            return null;
        }

        /// <summary>
        /// Validate Payment Input
        /// </summary>
        /// <returns></returns>
        [HttpPost("A55ValidatePaymentInput")]
        [ProducesResponseType(typeof(ValidatePaymentInputResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> A55ValidatePaymentInputAsync(ValidatePaymentInputRequestDto requestDto)
        {
            List<ExceptionsDto> errorList = new List<ExceptionsDto>();
            var fullActionName = GetFullActionMethodName(controllerName, actionMethodName);
            actionMethodName = nameof(A55ValidatePaymentInputAsync);

            try
            {
                var result = await _a55ValidatePaymentInputService.A55ValidatePaymentInputAsync(requestDto);

                var responseDto = _mapper.Map(result, new ValidatePaymentInputResponseDto());

                ResponseDto response = PrepareResponse(true,
                    actionMethodName,
                    ReturnCode.Success,
                    errorList,
                    result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                  LoggerConstants.LogError,
                  ex.Message,
                  fullActionName,
                  controllerName,
                  actionMethodName,
                  ex.StackTrace);

                return PopulateException(ex, fullActionName);
            }
        }

        private string GetFullActionMethodName(string controllerName, string actionMethodName)
        {
            return $"{DashboardConstants.MS_NAME}.{controllerName}.{actionMethodName}";
        }
    }
}
