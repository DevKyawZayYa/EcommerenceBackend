using AutoMapper;
using EcommerenceBackend.Application.Domain.Categories;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;

namespace EcommerenceBackend.Application.UseCases.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Use AutoMapper to map the command to the Category entity
            var category = _mapper.Map<Category>(request);

            // Generate a new ID for the category
            category.Id = Guid.NewGuid();
            category.CreatedOn = DateTime.UtcNow;

            // Add the category to the database
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
