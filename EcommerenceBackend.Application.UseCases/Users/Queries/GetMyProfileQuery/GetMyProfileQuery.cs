using EcommerenceBackend.Application.Dto.Users.EcommerenceBackend.Application.Dto.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Users.Queries.GetMyProfileQuery
{
    public class GetMyProfileQuery : IRequest<MyProfileResponse> { }

}
