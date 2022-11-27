using Admin.Announcement.Models.CmsKeys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Admin.Announcement.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "solution-campaign-api.all")]
    [ApiController]
    public class CmsController : Controller
    {
        private readonly IMediator _mediator;

        public CmsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("cmskeys")]
        public async Task<Dictionary<string, string>> GetCmsKeyValue(string country, string language, string region)
        {
            MicroContentRequest request = new MicroContentRequest();
            request.Country = country;
            request.Language = language;
            request.Region = region;

            return await _mediator.Send(new GetCmsDataQuery { ContentRequest = request });
        }
    }
}
