using AutoMapper;
using EcommerenceBackend.Application.Dto.Users;
using EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.User.Queries.GetUserProfileByAllQuery
{
    public class GetUserProfileByAllQueryHandler : IRequestHandler<GetUserProfileByAllQuery, GetUserProfileByAllResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserProfileByAllQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetUserProfileByAllResponse> Handle(GetUserProfileByAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "Request cannot be null.");

                var users = await _context.Users.Where(x => x.IsActive).ToListAsync(cancellationToken);

                if (users == null || !users.Any())
                {
                    return null;
                }

                var response = new GetUserProfileByAllResponse
                {
                    Users = _mapper.Map<List<UserProfileDto>>(users)
                };

                return response;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Get User Profile By All: {ex.Message}");
                throw;
            }    
        }
    }
}
