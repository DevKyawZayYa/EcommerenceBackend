using AutoMapper;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerenceBackend.Infrastructure.Contexts;

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly OrderDbContext _dbContext;

        public UpdateOrderCommandHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {

            try
            {
                if (request.OrderId == null)
                {
                    throw new ArgumentNullException(nameof(request.OrderId), "Order ID cannot be null.");
                }
                if (request.UpdatedItems == null || !request.UpdatedItems.Any())
                {
                    throw new ArgumentNullException(nameof(request.UpdatedItems), "Updated items cannot be null or empty.");
                }

                var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
                if (order == null) return false;

                var updatedItems = request.UpdatedItems.Select(dto =>
                    new OrderItem(
                        OrderItemId.Create(Guid.NewGuid()),
                        order.Id,
                        new ProductId(dto.ProductId),
                        new Money(dto.Price),
                        new Money(dto.Quantity)
                    )
                ).ToList();

                order.UpdateOrderItems(updatedItems);

                _dbContext.Orders.Update(order);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }        
        }
    }
}
