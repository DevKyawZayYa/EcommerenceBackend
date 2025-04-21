using AutoMapper;
using EcommerenceBackend.Application.Domain.Customers;
using EcommerenceBackend.Application.Dto.ShoppingCart.Response;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Contexts;
using EcommerenceBackend.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerenceBackend.Application.UseCases.ShoppingCart.Queries.GetCartItemsByCustomerId
{
    public class GetCartItemsByCustomerIdQueryHandler : IRequestHandler<GetCartItemsByCustomerIdQuery, List<ShoppingCartDto>>
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;

        public GetCartItemsByCustomerIdQueryHandler(OrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ShoppingCartDto>> Handle(GetCartItemsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            if (request.CustomerId == Guid.Empty)
                throw new ArgumentException("CustomerId cannot be empty.");

            var shoppingCart = await _context.ShoppingCarts
                .AsSplitQuery()
                .AsNoTracking()
                .Include(c => c.Items)
                    .ThenInclude(i => i.Products)
                        .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(c => c.CustomerId == new CustomerId(request.CustomerId), cancellationToken);

            if (shoppingCart == null || !shoppingCart.Items.Any())
                return new List<ShoppingCartDto>();

            var shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);
            var result = new List<ShoppingCartDto> { shoppingCartDto };

            return result;
        }
    }
}
