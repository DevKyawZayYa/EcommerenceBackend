// EcommerenceBackend.Application.UseCases/Products/Queries/GetProductDetailsByIdQuery.cs
using MediatR;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetProductDetailsById
{
    public class GetProductDetailsByIdQuery : IRequest<ProductDetailsDto>
    {
        public ProductId ProductId { get; set; }

        public GetProductDetailsByIdQuery(ProductId productId)
        {
            ProductId = productId;
        }
    }
}