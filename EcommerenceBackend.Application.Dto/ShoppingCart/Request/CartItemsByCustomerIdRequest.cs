using EcommerenceBackend.Application.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Dto.ShoppingCart.Request
{
    public class CartItemsByCustomerIdRequest
    {
        public Guid? CustomerId { get; set; }

    }
}
