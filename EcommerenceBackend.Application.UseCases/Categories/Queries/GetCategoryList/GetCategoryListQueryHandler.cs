using AutoMapper;
using EcommerenceBackend.Application.Dto.Categories;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCategoryListQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var categories = await _dbContext.Categories.Where(x => x.IsShowNavBar == true)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

                return _mapper.Map<List<CategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling request: {ex.Message}");
                throw;
            }
        
        }
    }
}
