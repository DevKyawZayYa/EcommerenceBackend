using EcommerenceBackend.Application.Dto.Common;
using EcommerenceBackend.Application.Dto.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetProductListByCategoryId
{
    public class GetProductListByCategoryIdQuery : IRequest<PagedResult<ProductListDto>>
    {
        public Guid? CategoryId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
