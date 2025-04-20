using AutoMapper;
using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.UseCases.Products.Queries.GetAllProducts;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetProductListByCategoryId
{

    public class GetProductListByCategoryIdQueryHandler : IRequestHandler<GetProductListByCategoryIdQuery, PagedResult<ProductListDto>>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductListByCategoryIdQueryHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductListDto>> Handle(GetProductListByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var totalItems = await _dbContext.Products.CountAsync();
            int page = request!.Page;
            int pageSize = request!.PageSize;

            var items = await _dbContext.Products.Where(x => x.CategoryId == request.CategoryId)
                .AsSplitQuery().AsNoTracking()
                .Include(p => p.ProductImages)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedItems = _mapper.Map<List<ProductListDto>>(items);

            return new PagedResult<ProductListDto>(mappedItems, totalItems, page, pageSize);
        }
    }
}
