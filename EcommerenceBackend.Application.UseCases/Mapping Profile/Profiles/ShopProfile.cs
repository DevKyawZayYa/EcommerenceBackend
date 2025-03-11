using AutoMapper;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Shops;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Dto.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Mapping_Profile.Profiles
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Shop, ShopDto>();
        }
    }
}
