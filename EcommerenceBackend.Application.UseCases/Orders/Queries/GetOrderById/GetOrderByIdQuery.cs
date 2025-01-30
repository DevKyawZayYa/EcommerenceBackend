using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Dto.Orders.Response;
using MediatR;
using System;

namespace EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public OrderId? OrderId { get; }
        public CustomerId? CustomerId { get; }

        public GetOrderByIdQuery(OrderId orderId, CustomerId customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
