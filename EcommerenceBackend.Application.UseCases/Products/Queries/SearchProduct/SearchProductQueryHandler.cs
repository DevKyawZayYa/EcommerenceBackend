using AutoMapper;
using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Dto.Products.EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.SearchProduct
{
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PagedResult<ProductDto>>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public SearchProductQueryHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDto>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Products.Include(x=> x.ProductImages)
                .AsSplitQuery()
                .AsNoTracking();

            // Apply filters  
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => p.Name != null && p.Name.Contains(request.Name));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price != null && p.Price.Amount >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price != null && p.Price.Amount <= request.MaxPrice.Value);
            }

            if (request.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId);
            }

            // Apply sorting  
            query = request.SortBy switch
            {
                "Price" => request.IsDescending ? query.OrderByDescending(p => p.Price!.Amount) : query.OrderBy(p => p.Price!.Amount),
                "CreatedDate" => request.IsDescending ? query.OrderByDescending(p => p.CreatedDate) : query.OrderBy(p => p.CreatedDate),
                _ => request.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            };

            // Get total count for pagination  
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination  
            var products = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Map to DTO  
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return new PagedResult<ProductDto>(productDtos, totalCount, request.Page, request.PageSize);
        }
    }
}

