using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly OrderDbContext _dbContext;

        public DeleteOrderCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.OrderId == null)
                {
                    throw new ArgumentNullException(nameof(request.OrderId), "Order ID cannot be null.");
                }

                var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
                if (order == null) return false;

                _dbContext.Orders.Remove(order);
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
