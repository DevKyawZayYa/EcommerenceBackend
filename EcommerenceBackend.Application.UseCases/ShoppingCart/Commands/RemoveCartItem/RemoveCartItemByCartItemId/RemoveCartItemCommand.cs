using MediatR;
using System;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.RemoveCartItem.RemoveCartItemByCartItemId
{
    public class RemoveCartItemCommand : IRequest<bool>
    {
        public Guid CartItemId { get; set; }

        public RemoveCartItemCommand(Guid cartItemId)
        {
            CartItemId = cartItemId;
        }
    }
}
