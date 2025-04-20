using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Dto.Products.EcommerenceBackend.Application.Dto.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.SearchProduct
{
    public class SearchProductQuery : IRequest<PagedResult<ProductDto>>
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Guid? CategoryId { get; set; }
        public string? SortBy { get; set; } = "Name"; // Default sorting by Name
        public bool IsDescending { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
