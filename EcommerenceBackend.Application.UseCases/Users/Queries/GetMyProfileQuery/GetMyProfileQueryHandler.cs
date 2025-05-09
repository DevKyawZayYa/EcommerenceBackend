using EcommerenceBackend.Application.Dto.Users.EcommerenceBackend.Application.Dto.Response;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Users.Queries.GetMyProfileQuery
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, MyProfileResponse>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetMyProfileQueryHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public Task<MyProfileResponse> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {

            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "Request cannot be null.");

                    var userId = _currentUserService.UserId;

                    if (string.IsNullOrEmpty(userId))
                    {
                        throw new UnauthorizedAccessException("User not logged in");
                    }

                    return Task.FromResult(new MyProfileResponse
                    {
                        Id = userId,
                    });
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Get My Profile: {ex.Message}");
                throw;
            }
          
        }
    }
}
