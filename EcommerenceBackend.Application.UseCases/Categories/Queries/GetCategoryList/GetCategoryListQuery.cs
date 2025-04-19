using EcommerenceBackend.Application.Dto.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<List<CategoryDto>>
    {
    }
}
