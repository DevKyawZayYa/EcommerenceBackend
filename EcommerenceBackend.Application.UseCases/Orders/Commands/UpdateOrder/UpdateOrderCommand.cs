using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Dto.Orders.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public Guid? OrderId { get; set; }
        public List<OrderItemDto> UpdatedItems { get; set; } = new List<OrderItemDto>();
        public decimal TotalAmount { get; set; }
    }
}
