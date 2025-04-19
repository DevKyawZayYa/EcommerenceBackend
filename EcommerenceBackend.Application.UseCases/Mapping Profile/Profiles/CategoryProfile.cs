using AutoMapper;
using EcommerenceBackend.Application.Domain.Categories;
using EcommerenceBackend.Application.Dto.Categories;
using EcommerenceBackend.Application.UseCases.Categories.Commands.CreateCategory;

namespace EcommerenceBackend.Application.UseCases.Mapping_Profile.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
