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
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "Request cannot be null.");
                if (request.ProductId == null)
                    throw new ArgumentNullException(nameof(request.ProductId), "Product ID cannot be null.");

                if (request == null) throw new ArgumentNullException(nameof(request));

                var cacheKey = $"product_detail_{request.ProductId}";

                var cached = await _cache.GetAsync<ProductDetailsDto>(cacheKey);
                if (cached is not null)
                    return cached;

                var product = await _dbContext.Products
                    .Include(p => p.ProductImages)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {request.ProductId} was not found.");

                var dto = _mapper.Map<ProductDetailsDto>(product);

                await _cache.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(15));

                return dto;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Get Product Details By Id: {ex.Message}");
                throw;
            }          
        }
    }
}
