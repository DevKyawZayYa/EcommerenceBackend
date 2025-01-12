using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Orders
{
    public class LineItem
    {
        internal LineItem(LineItemId id, OrderId orderId, ProductId productId, Money price)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Price = price;
        }
        public LineItemId Id { get; private set; }
        public OrderId OrderId { get; private set; }
        public ProductId ProductId { get; private set; }
        public Money Price { get; private set; }
    }
}
