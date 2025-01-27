using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.User.Queries.GetUserProfileByAllQuery
{
    public class GetUserProfileByAllResponse
    {
        public List<UserProfileDto> Users { get; set; }
    }
}
