using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.CQRS.Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract  class BaseController : ControllerBase
    {
        #region Property
        private IMediator _mediator;
        #endregion
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }
}
