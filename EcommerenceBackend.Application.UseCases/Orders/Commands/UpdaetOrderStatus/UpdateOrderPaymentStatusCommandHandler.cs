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
                .FirstOrDefaultAsync(o => o.StripeSessionId == request.StripeSessionId, cancellationToken);

            if (order is null)
                throw new Exception("Order not found for this Stripe session");

            order.MarkAsPaid(); // This should change PaymentStatus = "Paid"
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
