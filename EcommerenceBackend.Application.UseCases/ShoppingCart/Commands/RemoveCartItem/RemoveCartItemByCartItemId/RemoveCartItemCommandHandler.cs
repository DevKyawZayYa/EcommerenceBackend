using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.RemoveCartItem.RemoveCartItemByCartItemId
{
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, bool>
    {
        private readonly OrderDbContext _context;

        public RemoveCartItemCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CartItemId == Guid.Empty)
                    throw new ArgumentNullException(nameof(request.CartItemId), "Cart Item ID cannot be null.");
                    
                var item = await _context.CartItems
                    .FirstOrDefaultAsync(x => x.Id == request.CartItemId, cancellationToken);

                    if (item == null) return false;

                    _context.CartItems.Remove(item);
                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Remove Cart Item: {ex.Message}");
                throw;
            } 
        }
    }
}

