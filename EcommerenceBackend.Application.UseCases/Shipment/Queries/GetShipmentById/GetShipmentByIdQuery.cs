using EcommerenceBackend.Application.Dto.Shipment;
using MediatR;

namespace EcommerenceBackend.Application.UseCases.Shipment.Queries.GetShipmentById
{
    public class GetShipmentByIdQuery : IRequest<ShipmentDto>
    {
        public Guid ShipmentID { get; set; }
    }
}
