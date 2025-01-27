using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById
{
    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, GetUserProfileByIdResponse>
    {
        private readonly ApplicationDbContext _context;

        public GetUserProfileByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetUserProfileByIdResponse> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return null;
            }

            return new GetUserProfileByIdResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                City = user.City,
                MobileCode = user.MobileCode,
                MobileNumber = user.MobileNumber,
                Address = user.Address,
                Region = user.Region,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Role = user.Role,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate
            };
        }
    }
}
