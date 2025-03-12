using AutoMapper;
using EcommerenceBackend.Application.Domain.Shipment;
using EcommerenceBackend.Application.Dto.Shipment;

namespace EcommerenceBackend.Application.UseCases.Mapping_Profile.Profiles
{
    public class ShipmentProfile : Profile
    {
        public ShipmentProfile()
        {
            CreateMap<Domain.Shipment.Shipment, ShipmentDto>();
        }
    }
}
