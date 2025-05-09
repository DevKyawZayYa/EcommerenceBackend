// EcommerenceBackend.Application.UseCases/Products/Commands/UpdateProduct/UpdateProductCommandHandler.cs
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Infrastructure;
using EcommerenceBackend.Infrastructure.Contexts;

namespace EcommerenceBackend.Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.ProductId == null)
                    throw new ArgumentNullException(nameof(request.ProductId), "Product ID cannot be null.");

                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
                if (product == null) return false;

                _mapper.Map(request, product); // Map the request to the product entity

                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Update Product: {ex.Message}");
                throw;
            }

          
        }
    }
}
