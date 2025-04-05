using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.Dto.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Users.Commands.UpdateUserProfileById
{
    // UpdateUserProfileByIdCommand.cs
    public class UpdateUserProfileByIdCommand : IRequest<UpdateUserProfileByIdResponse>
    {
        public UserId? Id { get; set; }
        public FirstName? FirstName { get; set; }
        public LastName? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? MobileCode { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Role { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }

}
