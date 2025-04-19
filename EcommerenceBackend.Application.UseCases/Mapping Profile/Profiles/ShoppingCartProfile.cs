using AutoMapper;
using EcommerenceBackend.Application.Domain.ShoppingCart;
using EcommerenceBackend.Application.Domain.Shops;
using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using EcommerenceBackend.Application.Dto.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Mapping_Profile.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {

            CreateMap<CartItem, CartItemDto>()
          .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.Value))
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Products.Name!))
          .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.Products.ProductImages!.ToList().Where(x => x.IsPrimary).FirstOrDefault()!.ImageUrl))
          ;

            CreateMap<Domain.ShoppingCart.ShoppingCart, ShoppingCartDto>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.ShoppingCartId))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId.Value))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CalculateTotalPrice()))
                ;
        }
    }
}
