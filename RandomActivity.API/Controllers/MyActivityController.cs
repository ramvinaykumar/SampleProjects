using Microsoft.AspNetCore.Mvc;
using RandomActivity.API.Services;
using RandomActivity.API.Models;

namespace RandomActivity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyActivityController : ControllerBase
    {
        private readonly ILogger<MyActivityController> _logger;
        private readonly IMyActivityService _myActivityService;

        public MyActivityController(ILogger<MyActivityController> logger, IMyActivityService backendService)
        {
            _logger = logger;
            _myActivityService = backendService;
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
    }
}
