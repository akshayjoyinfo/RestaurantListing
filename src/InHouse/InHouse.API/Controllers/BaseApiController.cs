using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace InHouse.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Defines the _mediator.
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Gets the Mediator.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
