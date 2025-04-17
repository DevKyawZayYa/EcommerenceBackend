// EcommerenceBackend.Application.UseCases/Products/Queries/GetProductDetailsByIdQueryHandler.cs
using AutoMapper;
using MediatR;
using EcommerenceBackend.Application.Dto.Products;
using EcommerenceBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Infrastructure.Contexts;

namespace EcommerenceBackend.Application.UseCases.Products.Queries.GetProductDetailsById
{
    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, ProductDetailsDto>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductDetailsByIdQueryHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {request.ProductId} was not found.");

            var dto = _mapper.Map<ProductDetailsDto>(product);

            return dto;
        }
    }
}
