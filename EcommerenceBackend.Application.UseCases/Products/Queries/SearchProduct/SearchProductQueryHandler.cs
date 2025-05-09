using AutoMapper;
using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Dto.Products.EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Contexts;
using EcommerenceBackend.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.SearchProduct
{
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PagedResult<ProductDto>>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRedisService _cache;

        public SearchProductQueryHandler(OrderDbContext dbContext, IMapper mapper, IRedisService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<PagedResult<ProductDto>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var cacheKey = $"search_products_" +
                    $"{request.Name?.ToLower() ?? "any"}_" +
                    $"{request.CategoryId?.ToString() ?? "any"}_" +
                    $"{request.MinPrice?.ToString() ?? "min"}_" +
                    $"{request.MaxPrice?.ToString() ?? "max"}_" +
                    $"{request.SortBy}_{request.IsDescending}_{request.Page}_{request.PageSize}";

                // Try to get cached result
                var cached = await _cache.GetAsync<PagedResult<ProductDto>>(cacheKey);
                if (cached is not null)
                    return cached;

                // Build query
                var query = _dbContext.Products.Include(x => x.ProductImages)
                    .AsSplitQuery()
                    .AsNoTracking();

                // Apply filters  
                if (!string.IsNullOrEmpty(request.Name))
                    query = query.Where(p => p.Name != null && p.Name.Contains(request.Name));

                if (request.MinPrice.HasValue)
                    query = query.Where(p => p.Price != null && p.Price.Amount >= request.MinPrice.Value);

                if (request.MaxPrice.HasValue)
                    query = query.Where(p => p.Price != null && p.Price.Amount <= request.MaxPrice.Value);

                if (request.CategoryId.HasValue)
                    query = query.Where(p => p.CategoryId == request.CategoryId);

                // Sorting  
                query = request.SortBy switch
                {
                    "Price" => request.IsDescending ? query.OrderByDescending(p => p.Price!.Amount) : query.OrderBy(p => p.Price!.Amount),
                    "CreatedDate" => request.IsDescending ? query.OrderByDescending(p => p.CreatedDate) : query.OrderBy(p => p.CreatedDate),
                    _ => request.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                };

                // Pagination  
                var totalCount = await query.CountAsync(cancellationToken);

                var products = await query
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                var productDtos = _mapper.Map<List<ProductDto>>(products);
                var result = new PagedResult<ProductDto>(productDtos, totalCount, request.Page, request.PageSize);

                // Cache 
                await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(2));

                return result;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Search Product: {ex.Message}");
                throw;
            }
        }
    }
}
