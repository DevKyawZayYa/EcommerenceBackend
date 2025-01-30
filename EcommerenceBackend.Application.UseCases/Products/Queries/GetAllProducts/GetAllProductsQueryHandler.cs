using AutoMapper;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDetailsDto>>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

      public GetAllProductsQueryHandler(OrderDbContext dbContext, IMapper mapper)
      {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDetailsDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var totalItems = await _dbContext.Products.CountAsync();
            int page = request!.Page;
            int pageSize = request!.PageSize;

            var items = await _dbContext.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedItems = _mapper.Map<List<ProductDetailsDto>>(items);

            return new PagedResult<ProductDetailsDto>(mappedItems, totalItems, page, pageSize);
        }
    }

}
