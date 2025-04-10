using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Dto.Orders.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderListByCustomerId
{

    public class GetOrderListByCustomerIdQuery : IRequest<List<OrderListByCustomerIdDto>>
    {
        public GetOrderListByCustomerIdQuery(CustomerId customerId)
        {
            CustomerId = customerId;
        }

        public CustomerId? CustomerId { get; }
    }
}
