using Admin.Announcement.Models;
using Admin.Announcement.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Admin.Announcement.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "solution-campaign-api.all")]
    [ApiController]
    public class CountryLanguageController : Controller
    {
        private readonly IMediator _mediator;
        public CountryLanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<GenericResponse<IEnumerable<CountryLanguage>>> GetCountryLanguage(CancellationToken token)
        {
            return await _mediator.Send(new GetCountryLanguageQuery(), token);
        }
    }
}