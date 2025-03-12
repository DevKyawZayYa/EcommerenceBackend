using EcommerenceBackend.Application.Domain.Shipment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shipment.Commands
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public Guid OrderID { get; set; }
        public decimal ShippingCost { get; set; }
        public string Carrier { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public DateTime ShipmentDate { get; set; } = DateTime.UtcNow;
        public ShipmentStatus DeliveryStatus { get; set; } = ShipmentStatus.Pending;
    }
}
