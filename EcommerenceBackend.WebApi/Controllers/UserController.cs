using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.Dto.Users.EcommerenceBackend.Application.Dto.Response;
using EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById;
using EcommerenceBackend.Application.UseCases.User.Queries.GetUserProfileByAllQuery;
using EcommerenceBackend.Application.UseCases.Users.Commands.UpdateUserProfileById;
using EcommerenceBackend.Application.UseCases.Users.Queries.GetMyProfileQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUserProfileById(Guid id, [FromBody] UpdateUserProfileByIdCommand command)
        {
            if (id != command.Id!.Value)
            {
                return BadRequest("User ID mismatch.");
            }

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpGet("GetUserProfileByAll")]
        public async Task<IActionResult> GetUserProfileByAll()
        {
            var result = await _mediator.Send(new GetUserProfileByAllQuery());
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<ActionResult<MyProfileResponse>> GetMyProfile()
        {
            var result = await _mediator.Send(new GetMyProfileQuery());
            return Ok(result);
        }
    }
}
