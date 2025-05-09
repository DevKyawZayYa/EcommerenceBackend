using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.RemoveCartItem.RemoveCartItemByShoppingCartId
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, bool>
    {
        private readonly OrderDbContext _context;

        public ClearCartCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.ShoppingCartId == Guid.Empty)
                    throw new ArgumentNullException(nameof(request.ShoppingCartId), "Shopping Cart ID cannot be null.");

                var cart = await _context.ShoppingCarts
                              .Include(x => x.Items)
                              .FirstOrDefaultAsync(x => x.ShoppingCartId == request.ShoppingCartId, cancellationToken);

                if (cart == null) return false;

                cart.ClearCart();

                _context.ShoppingCarts.Remove(cart);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Clear Cart: {ex.Message}");
                throw;
            }     
        }
    }
}
