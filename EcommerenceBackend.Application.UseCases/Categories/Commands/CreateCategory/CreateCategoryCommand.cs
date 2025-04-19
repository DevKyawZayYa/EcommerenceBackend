using EcommerenceBackend.Application.Dto.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public CreateCategoryCommand(CategoryDto dto)
        {
            Name = dto.Name;
            Description = dto.Description;
        }
    }
}
