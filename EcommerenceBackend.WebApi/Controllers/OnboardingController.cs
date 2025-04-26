using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.Dto.ApplicationUser.Request;
using EcommerenceBackend.Application.Dto.ApplicationUser.Response;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Application.UseCases.Onboarding.Commands.ChangePassword;
using EcommerenceBackend.Application.UseCases.Onboarding.Commands.LoginUser;
using EcommerenceBackend.Application.UseCases.Onboarding.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtService _jwtService;
        private readonly ApplicationDbContext _context;

        public OnboardingController(IMediator mediator, IJwtService jwtService, ApplicationDbContext context)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User registered successfully.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                if (result.Message == "User not found.")
                {
                    return NotFound(new { Message = result.Message, Errors = result.Errors });
                }
                else if (result.Message == "Current password is incorrect.")
                {
                    return BadRequest(new { Message = result.Message, Errors = result.Errors });
                }
                else
                {
                    return StatusCode(500, new { Message = result.Message, Errors = result.Errors });
                }
            }

            return Ok(new { Message = result.Message });
        }



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenRequestDto tokenRequest)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
            if (principal == null)
                return BadRequest("Invalid access token");

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        
            var parstUserId = new UserId(Guid.Parse(userId!));

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == parstUserId);
            if (user == null || user.RefreshToken != tokenRequest.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return BadRequest("Invalid refresh token");

            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

    }
}
