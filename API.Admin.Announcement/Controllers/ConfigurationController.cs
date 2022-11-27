using Admin.Announcement.Models.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NewRelic.Api.Agent;

namespace API.Admin.Announcement.Controllers
{
    [[Route("api/[controller]")]
    [Authorize(Roles = "solution-campaign-api.all")]
    [ApiController]
    public class ConfigurationController : Controller
    {
        private readonly IOptionsMonitor<ConfiguratorSettingConfig> _configuratorSettingConfig;
        public ConfigurationController(IOptionsMonitor<ConfiguratorSettingConfig> configuratorSettingConfig)
        {
            _configuratorSettingConfig = configuratorSettingConfig;
        }

        [Trace]
        [HttpGet("Settings")]
        public IActionResult GetSettings()
        {
            return Ok(_configuratorSettingConfig.CurrentValue);
        }

    }
}
