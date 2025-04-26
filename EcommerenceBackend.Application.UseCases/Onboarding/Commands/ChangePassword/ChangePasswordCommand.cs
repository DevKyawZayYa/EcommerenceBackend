using EcommerenceBackend.Application.Common.Results;
using EcommerenceBackend.Application.Domain.Users;
using MediatR;
using System;

namespace EcommerenceBackend.Application.UseCases.Onboarding.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public UserId UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
