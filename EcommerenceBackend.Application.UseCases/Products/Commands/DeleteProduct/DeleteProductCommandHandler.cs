using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly OrderDbContext _dbContext;

        public DeleteProductCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.ProductId == null)
                    throw new ArgumentNullException(nameof(request.ProductId), "Product ID cannot be null.");

                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
                if (product == null)
                    return false;

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Delete Product: {ex.Message}");
                throw;
            }

        }
    }
}
