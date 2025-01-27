using EcommerenceBackend.Application.Dto.Users;
using MediatR;

namespace EcommerenceBackend.Application.UseCases.Onboarding.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
