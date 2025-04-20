using AutoMapper;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Contexts;
using EcommerenceBackend.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductListDto>>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRedisService _cache;

        public GetAllProductsQueryHandler(OrderDbContext dbContext, IMapper mapper, IRedisService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<PagedResult<ProductListDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            int page = request!.Page;
            int pageSize = request!.PageSize;
            string cacheKey = $"products_all_page_{page}_size_{pageSize}";

            // Try get from Redis
            var cached = await _cache.GetAsync<PagedResult<ProductListDto>>(cacheKey);
            if (cached is not null)
                return cached;

            var totalItems = await _dbContext.Products.CountAsync(cancellationToken);

            var items = await _dbContext.Products.AsSplitQuery().AsNoTracking()
                .Include(p => p.ProductImages)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<ProductListDto>>(items);
            var result = new PagedResult<ProductListDto>(mappedItems, totalItems, page, pageSize);

            // Cache for 10 minutes
            await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(10));

            return result;
        }
    }
}
