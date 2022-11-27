using Admin.Announcement.Models;
using Admin.Announcement.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Admin.Announcement.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "solution-campaign-api.all")]
    [ApiController]
    public class LanguagesController : Controller
    {
        private readonly IMediator _mediator;
        public LanguagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GenericResponse<IEnumerable<Language>>> GetLanguages()
        {
            return await _mediator.Send(new GetLanguagesQuery());
        }
    }
}
