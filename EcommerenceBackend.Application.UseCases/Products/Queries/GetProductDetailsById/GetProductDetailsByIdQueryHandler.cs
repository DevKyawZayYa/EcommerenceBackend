using AutoMapper;
using MediatR;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Infrastructure.Contexts;
using EcommerenceBackend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Application.Interfaces.Interfaces;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetProductDetailsById
{
    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ProductDetailsDto>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRedisService _cache;

        public GetProductDetailsByIdQueryHandler(OrderDbContext dbContext, IMapper mapper, IRedisService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<ProductDetailsDto> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"product_detail_{request.ProductId}";

            // 1️⃣ Try from Redis
            var cached = await _cache.GetAsync<ProductDetailsDto>(cacheKey);
            if (cached is not null)
                return cached;

            // 2️⃣ Load from DB
            var product = await _dbContext.Products
                .Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {request.ProductId} was not found.");

            var dto = _mapper.Map<ProductDetailsDto>(product);

            // 3️⃣ Save to Redis for 15 min
            await _cache.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(15));

            return dto;
        }
    }
}
