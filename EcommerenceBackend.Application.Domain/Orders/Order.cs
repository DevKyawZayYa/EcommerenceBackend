using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Orders
{
    public class Order
    {
        private readonly HashSet<OrderItem> _orderItems = new ();

        private Order()
        {
        }
        public OrderId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.ToList();

        public static Order Create(Customer  customer)
        {
            return new Order
            {
                Id = OrderId.Create(Guid.NewGuid()),
                CustomerId = customer.Id
            };
        }

        public void Add(Product product)
        {
            var OrderItem = new OrderItem(
                OrderItemId.Create(Guid.NewGuid()),
                Id,
                product.Id,
                product.Price
            );

            _orderItems.Add(OrderItem);
        }
    }
}
