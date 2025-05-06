using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Dto.Orders.Request
{
    public class OrderRequest
    {
        public Guid? OrderId { get; set; }
        public CustomerId? CustomerId { get; set; }
    }
}
