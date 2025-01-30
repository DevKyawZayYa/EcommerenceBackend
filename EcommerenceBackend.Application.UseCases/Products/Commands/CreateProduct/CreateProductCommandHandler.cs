using AutoMapper;
using MediatR;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Infrastructure;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Infrastructure.Contexts;

namespace EcommerenceBackend.Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductDto);
            product.GetType().GetProperty("ShoppingCartId")?.SetValue(product, ProductId.Create(Guid.NewGuid()));

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id!.Value; // Return the created Product ID
        }
    }
}
