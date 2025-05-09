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
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var category = _mapper.Map<Category>(request);

                category.Id = Guid.NewGuid();
                category.CreatedOn = DateTime.UtcNow;

                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return category.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating category: {ex.Message}");
                throw;
            }
        
        }
    }
}
