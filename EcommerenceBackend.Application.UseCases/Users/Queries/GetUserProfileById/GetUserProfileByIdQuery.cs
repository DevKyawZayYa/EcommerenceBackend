using EcommerenceBackend.Application.Domain.Users;
using MediatR;

namespace EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById
{
    public class GetUserProfileByIdQuery : IRequest<GetUserProfileByIdResponse>
    {
        public UserId? UserId { get; set; }
    }
}
