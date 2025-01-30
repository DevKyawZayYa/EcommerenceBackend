// EcommerenceBackend.Application/MappingProfiles/OrderMappingProfile.cs
using AutoMapper;
using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Dto.Orders.Response;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId.Value))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.Value))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products!.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.Amount));
    }
}

