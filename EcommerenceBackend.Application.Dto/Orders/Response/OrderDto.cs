using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Dto.Orders.Response
{
    public class OrderDto
    {
        public Guid? Id { get; set; }
        public CustomerId CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

}
