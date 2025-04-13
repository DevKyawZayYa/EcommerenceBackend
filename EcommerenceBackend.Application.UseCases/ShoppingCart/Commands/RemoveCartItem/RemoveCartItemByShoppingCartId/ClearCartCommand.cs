using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Commands.RemoveCartItem.RemoveCartItemByShoppingCartId
{
    public class ClearCartCommand : IRequest<bool>
    {
        public Guid ShoppingCartId { get; set; }

        public ClearCartCommand(Guid shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }
    }
}
