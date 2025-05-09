using EcommerenceBackend.Application.Domain.ShoppingCart;
using EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.AddToCart;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, Guid>
{
    private readonly OrderDbContext _dbContext;

    public AddCartItemCommandHandler(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.CustomerId == null)
                throw new ArgumentException("Customer ID cannot be empty.", nameof(request.CustomerId));
            if (request.ProductId == null)
                throw new ArgumentException("Product ID cannot be empty.", nameof(request.ProductId));
            if (request.Quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(request.Quantity), "Quantity must be greater than zero.");
            if (request.Price <= 0)
                throw new ArgumentOutOfRangeException(nameof(request.Price), "Price must be greater than zero.");

            var cart = await _dbContext.ShoppingCarts
           .Include(c => c.Items)
           .FirstOrDefaultAsync(c => c.CustomerId == request.CustomerId, cancellationToken);

            if (cart == null)
            {
                cart = new ShoppingCart(request.CustomerId);
                await _dbContext.ShoppingCarts.AddAsync(cart, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken); // Cart now has real ID
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + request.Quantity);
                _dbContext.Entry(existingItem).State = EntityState.Modified;
            }
            else
            {
                cart.AddItem(request.ProductId, request.Price, request.Quantity);

                var newItem = cart.Items.First(i => i.ProductId == request.ProductId);
                _dbContext.Entry(newItem).State = EntityState.Added;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return cart.ShoppingCartId;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error in Add Cart Item: {ex.Message}");
            throw;
        }
    }
}
