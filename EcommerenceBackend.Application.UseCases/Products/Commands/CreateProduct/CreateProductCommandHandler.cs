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
            try
            {
                if (request.ProductDto == null)
                    throw new ArgumentNullException(nameof(request.ProductDto), "Product DTO cannot be null.");
                if (string.IsNullOrEmpty(request.ProductDto.Name))
                    throw new ArgumentException("Product name cannot be null or empty.", nameof(request.ProductDto.Name));
                if (request.ProductDto.Price <= 0)
                    throw new ArgumentOutOfRangeException(nameof(request.ProductDto.Price), "Product price must be greater than zero.");

                var product = _mapper.Map<Product>(request.ProductDto);
                product.GetType().GetProperty("ShoppingCartId")?.SetValue(product, ProductId.Create(Guid.NewGuid()));

                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return product.Id!.Value; 

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Create Product: {ex.Message}");
                throw;
            }

    
        }
    }
}
