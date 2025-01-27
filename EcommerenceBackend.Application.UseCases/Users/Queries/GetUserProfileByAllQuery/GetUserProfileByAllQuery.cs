using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.User.Queries.GetUserProfileByAllQuery
{
    public class GetUserProfileByAllQuery : IRequest<GetUserProfileByAllResponse>
    {
        public UserId? UserId { get; set; }
    }
}
