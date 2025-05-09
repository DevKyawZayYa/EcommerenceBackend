using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.UpdateOrderPaymentStatus
{
    public class UpdateOrderPaymentStatusCommandHandler : IRequestHandler<UpdateOrderPaymentStatusCommand, Unit>
    {
        private readonly OrderDbContext _dbContext;

        public UpdateOrderPaymentStatusCommandHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateOrderPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.OrderId == Guid.Empty)
                    throw new ArgumentNullException(nameof(request.OrderId), "Order ID cannot be empty.");

                var order = await _dbContext.Orders
              .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

                if (order is null)
                    throw new Exception(" Order not found for the provided Stripe session ID.");

                switch (request.NewStatus)
                {
                    case "Paid":
                        order.MarkAsPaid(); 
                        break;

                    case "Failed":
                        order.MarkAsFailed(); 
                        break;

                    default:
                        throw new Exception($"Unsupported payment status: {request.NewStatus}");
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
