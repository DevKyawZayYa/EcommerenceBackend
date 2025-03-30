using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.Dto.Payments;
using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Queries.GetCartItemsByCustomerId
{

    public class GetCartItemsByCustomerIdQuery : IRequest<List<ShoppingCartDto>>
    {
        public Guid CustomerId { get; set; }
    }
}
