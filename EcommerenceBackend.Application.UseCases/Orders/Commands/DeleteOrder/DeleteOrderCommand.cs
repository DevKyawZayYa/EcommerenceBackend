using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public Guid? OrderId { get; set; }
    }
}
