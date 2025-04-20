using AutoMapper;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.Dto.Products.EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Application.UseCases.Products.Commands.UpdateProduct;

namespace EcommerenceBackend.Application.UseCases.MappingProfile.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {

            CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value)) // Map ProductId to Guid
                    .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.ProductImages!.ToList().Where(x => x.IsPrimary).FirstOrDefault()!.ImageUrl))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price!.Amount));

            // Map ProductId to Guid
            CreateMap<ProductId, Guid>().ConvertUsing(src => src.Value);

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Money(src.Price))) // Custom mapping for Price
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => new Sku(src.Stock.ToString()))) // Custom mapping for Sku
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            ;

            CreateMap<Product, ProductListDto>()
             .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.ProductImages!.ToList().Where(x => x.IsPrimary).FirstOrDefault()!.ImageUrl))
              ;

            CreateMap<Product, ProductDetailsDto>()
               .ForMember(dest => dest.PrimaryImageUrl, opt => opt.MapFrom(src => src.ProductImages!.ToList().Where(x=> x.IsPrimary).FirstOrDefault()!.ImageUrl))
               .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ProductImages!.ToList()))
                ;

            CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
