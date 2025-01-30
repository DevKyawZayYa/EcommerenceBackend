using AutoMapper;
using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.ShoppingCart;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Guid>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddToCartCommandHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = _dbContext.ShoppingCarts.FirstOrDefault(c => c.CustomerId == request.CustomerId);

            if (cart == null)
            {
                cart = new Domain.ShoppingCart.ShoppingCart(request.CustomerId!);
                await _dbContext.ShoppingCarts.AddAsync(cart, cancellationToken);
            }

            foreach (var item in request.Items)
            {
                cart.AddItem(new ProductId(item.ProductId!.Value), item.Price, item.Quantity);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return cart.ShoppingCartId;
        }
    }
}
