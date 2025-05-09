using AutoMapper;
using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Contexts;
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
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<PagedResult<ProductListDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "Request cannot be null.");

                //Middleware usage
                if (request == null) throw new ArgumentNullException(nameof(request));

                int page = request.Page;
                int pageSize = request.PageSize;

                string cacheKey = $"products_all_page_{page}_size_{pageSize}";

                var cachedResult = await _cache.GetAsync<PagedResult<ProductListDto>>(cacheKey);
                if (cachedResult is not null)
                    return cachedResult;

                var totalItems = await _dbContext.Products.CountAsync(cancellationToken);

                var products = await _dbContext.Products
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(p => p.ProductImages)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                var mappedProducts = _mapper.Map<List<ProductListDto>>(products);

                var pagedResult = new PagedResult<ProductListDto>(mappedProducts, totalItems, page, pageSize);

                await _cache.SetAsync(cacheKey, pagedResult, TimeSpan.FromMinutes(10));

                return pagedResult;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Get All Products: {ex.Message}");
                throw;
            }         
        }
    }
}
