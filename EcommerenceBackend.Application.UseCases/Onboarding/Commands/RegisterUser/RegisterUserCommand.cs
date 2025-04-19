using EcommerenceBackend.Application.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Onboarding.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? City { get; set; }
        public string? MobileCode { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

    }
}
