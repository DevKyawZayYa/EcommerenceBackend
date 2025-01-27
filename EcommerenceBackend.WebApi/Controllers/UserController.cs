using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById;
using EcommerenceBackend.Application.UseCases.User.Queries.GetUserProfileByAllQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfileById(Guid id)
        {
            var query = new GetUserProfileByIdQuery { UserId = new UserId(id) };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetUserProfileByAll")]
        public async Task<IActionResult> GetUserProfileByAll()
        {
            var result = await _mediator.Send(new GetUserProfileByAllQuery());
            return Ok(result);
        }
    }
}
