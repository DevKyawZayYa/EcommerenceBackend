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

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateOrderCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
            if (order == null) return false;

            // ✅ Convert DTOs to domain models in UseCase Layer
            var updatedItems = request.UpdatedItems.Select(dto =>
                new OrderItem(
                    OrderItemId.Create(Guid.NewGuid()),
                    order.Id,
                    new ProductId(dto.ProductId),
                    new Money(dto.Price),
                    new Money(dto.Quantity)
                )
            ).ToList();

            order.UpdateOrderItems(updatedItems); // ✅ Use domain method

            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
