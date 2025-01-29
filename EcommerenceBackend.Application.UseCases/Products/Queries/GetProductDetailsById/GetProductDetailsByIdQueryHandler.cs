// EcommerenceBackend.Application.UseCases/Products/Queries/GetProductDetailsByIdQueryHandler.cs
using AutoMapper;
using MediatR;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetProductDetailsById
{
    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ProductDetailsDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductDetailsByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {request.ProductId} was not found.");

            return _mapper.Map<ProductDetailsDto>(product);
        }
    }
}
