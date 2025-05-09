using AutoMapper;
using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Contexts;
using EcommerenceBackend.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetProductListByCategoryId
{
    public class GetProductListByCategoryIdQueryHandler : IRequestHandler<GetProductListByCategoryIdQuery, PagedResult<ProductListDto>>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRedisService _cache;

        public GetProductListByCategoryIdQueryHandler(OrderDbContext dbContext, IMapper mapper, IRedisService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<PagedResult<ProductListDto>> Handle(GetProductListByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                int page = request!.Page;
                int pageSize = request!.PageSize;
                string cacheKey = $"product_list_cat_{request.CategoryId}_page_{page}_size_{pageSize}";

                var cached = await _cache.GetAsync<PagedResult<ProductListDto>>(cacheKey);
                if (cached is not null)
                    return cached;

                var totalItems = await _dbContext.Products
                    .Where(x => x.CategoryId == request.CategoryId)
                    .CountAsync(cancellationToken);

                var items = await _dbContext.Products
                    .Where(x => x.CategoryId == request.CategoryId)
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(p => p.ProductImages)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                var mappedItems = _mapper.Map<List<ProductListDto>>(items);
                var result = new PagedResult<ProductListDto>(mappedItems, totalItems, page, pageSize);

                await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5)); // reasonable cache time

                return result;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in GetProductListByCategoryIdQueryHandler: {ex.Message}");
                throw;
            }     
        }
    }
}
