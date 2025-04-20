using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Infrastructure.Contexts;
using EcommerenceBackend.Infrastructure.Services;
using EcommerenceBackend.Application.Interfaces.Interfaces;

namespace EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById
{
    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, GetUserProfileByIdResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisService _cache;

        public GetUserProfileByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IRedisService cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetUserProfileByIdResponse> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"user_profile_{request.UserId}";

            // 1️⃣ Try from cache
            var cached = await _cache.GetAsync<GetUserProfileByIdResponse>(cacheKey);
            if (cached is not null)
                return cached;

            // 2️⃣ DB fallback
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
                return null;

            // 3️⃣ Map using AutoMapper
            var response = _mapper.Map<GetUserProfileByIdResponse>(user);

            // 4️⃣ Cache it for 10 mins
            await _cache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(10));

            return response;
        }
    }
}
