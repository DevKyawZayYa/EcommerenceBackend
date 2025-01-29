// EcommerenceBackend.Application.UseCases/Products/Commands/CreateProductCommand.cs
using MediatR;
using EcommerenceBackend.Application.Dto.Products;

namespace EcommerenceBackend.Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public CreateProductDto ProductDto { get; set; }

        public CreateProductCommand(CreateProductDto productDto)
        {
            ProductDto = productDto;
        }
    }
}
