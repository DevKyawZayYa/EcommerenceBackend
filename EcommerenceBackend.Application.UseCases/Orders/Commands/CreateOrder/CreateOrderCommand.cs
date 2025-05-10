using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Dto.Orders.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public CustomerId? CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public decimal TaxAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = "Stripe";
        public string PaymentStatus { get; set; } = "Unpaid";
        public string DeliveryStatus { get; set; } = "Processing";
        public string? StripeSessionId { get; set; } 

    }

}
