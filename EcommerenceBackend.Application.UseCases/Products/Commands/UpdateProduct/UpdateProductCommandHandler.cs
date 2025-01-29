// EcommerenceBackend.Application.UseCases/Products/Commands/UpdateProduct/UpdateProductCommandHandler.cs
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Infrastructure;

namespace EcommerenceBackend.Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
            if (product == null) return false;

            _mapper.Map(request, product); // Map the request to the product entity

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
