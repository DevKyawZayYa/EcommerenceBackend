using EcommerenceBackend.Application.Domain.Shipment;
using MediatR;

namespace EcommerenceBackend.Application.UseCases.Shipment.Commands
{
    public class CreateShipmentCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateShipmentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.OrderID == null)
                    throw new ArgumentNullException(nameof(request.OrderID), "Order ID cannot be null.");

                var shipment = new Domain.Shipment.Shipment
                {
                    OrderID = request.OrderID,
                    ShippingCost = request.ShippingCost,
                    Carrier = request.Carrier,
                    TrackingNumber = request.TrackingNumber,
                    ShipmentDate = request.ShipmentDate,
                    DeliveryStatus = request.DeliveryStatus
                };

                _context.Shipments.Add(shipment);
                await _context.SaveChangesAsync(cancellationToken);
                return shipment.ShipmentID;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Create Shipment: {ex.Message}");
                throw;
            }       
        }
    }
}
