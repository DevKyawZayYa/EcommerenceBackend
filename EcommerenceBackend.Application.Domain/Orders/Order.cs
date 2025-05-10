// EcommerenceBackend.Application.Domain/Orders/Order.cs
using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EcommerenceBackend.Application.Domain.Orders
{
    [Table("orders")]

    public class Order
    {
        private readonly HashSet<OrderItem> _orderItems = new();

        private Order()
        {
        }

        public Order(CustomerId customerId, 
            IEnumerable<OrderItem> items, 
            decimal taxAmount, 
            decimal shippingCost, 
            decimal discountAmount, 
            string status, 
            string paymentMethod, 
            string paymentStatus, 
            string deliveryStatus,
            string stripeSessionId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            _orderItems = new HashSet<OrderItem>(items);
            TaxAmount = taxAmount;
            ShippingCost = shippingCost;
            DiscountAmount = discountAmount;
            Status = status;
            PaymentMethod = paymentMethod;
            PaymentStatus = paymentStatus;
            DeliveryStatus = deliveryStatus;
            TotalAmount = _orderItems.Sum(item => item.Price.Amount * item.Quantity.Amount);
            GrandTotal = TotalAmount + TaxAmount + ShippingCost - DiscountAmount;
            StripeSessionId = stripeSessionId;
        }

        public Guid Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public DateTime OrderDate { get; private set; } = DateTime.UtcNow;
        public decimal TaxAmount { get; private set; }
        public decimal ShippingCost { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal GrandTotal { get; private set; }
        public string Status { get; private set; }
        public string PaymentMethod { get; private set; }
        public string PaymentStatus { get; private set; }
        public string DeliveryStatus { get; private set; }
        public string? StripeSessionId { get; private set; }

        public void MarkAsPaid()
        {
            PaymentStatus = "Paid";
        }

        public void MarkAsFailed()
        {
            PaymentStatus = "Failed";
        }


        public void SetStripeSessionId(string sessionId)
        {
            StripeSessionId = sessionId;
        }


        public void UpdateOrderItems(IEnumerable<OrderItem> updatedItems)
        {
            _orderItems.Clear();
            foreach (var item in updatedItems)
            {
                _orderItems.Add(item);
            }
            TotalAmount = _orderItems.Sum(item => item.Price.Amount * item.Quantity.Amount);
            GrandTotal = TotalAmount + TaxAmount + ShippingCost - DiscountAmount;
        }
    }
}
