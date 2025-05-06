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
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order is null)
                throw new Exception(" Order not found for the provided Stripe session ID.");

            switch (request.NewStatus)
            {
                case "Paid":
                    order.MarkAsPaid(); // You already have this method
                    break;

                case "Failed":
                    order.MarkAsFailed(); // You’ll create this next
                    break;

                default:
                    throw new Exception($"Unsupported payment status: {request.NewStatus}");
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
