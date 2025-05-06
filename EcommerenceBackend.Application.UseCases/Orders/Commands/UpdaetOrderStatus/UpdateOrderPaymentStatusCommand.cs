using MediatR;

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.UpdateOrderPaymentStatus
{
    public class UpdateOrderPaymentStatusCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; } = default!;
        public string NewStatus { get; set; } = "Paid";
    }
}
