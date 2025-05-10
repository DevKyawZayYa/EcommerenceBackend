// EcommerenceBackend.Application/MappingProfiles/OrderMappingProfile.cs
using AutoMapper;
using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Dto.Orders.Response;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId.Value))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<Order, OrderDetailByIdDto>()
          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.DeliveryStatus))
          .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
          .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
          .ForMember(dest => dest.DeliveryStatus, opt => opt.MapFrom(src => src.DeliveryStatus))
          .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
          .ForMember(dest => dest.ShippingCost, opt => opt.MapFrom(src => src.ShippingCost))
          .ForMember(dest => dest.GrandTotal, opt => opt.MapFrom(src => src.GrandTotal))
          .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
          .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))         
          ;

        CreateMap<Order, OrderListByCustomerIdDto>()
             .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
            .ForMember(dest => dest.DeliveryStatus, opt => opt.MapFrom(src => src.DeliveryStatus))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));  

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.Value))
           .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Products.ProductImages!.ToList().Where(x => x.IsPrimary).FirstOrDefault()!.ImageUrl))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products!.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.Amount));
    }
}

