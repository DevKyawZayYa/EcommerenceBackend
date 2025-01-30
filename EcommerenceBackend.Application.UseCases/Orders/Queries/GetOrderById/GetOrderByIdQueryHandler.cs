// EcommerenceBackend.Application.UseCases/Orders/Queries/GetOrderById/GetOrderByIdQueryHandler.cs
using AutoMapper;
using EcommerenceBackend.Application.Domain.Orders;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Application.Dto.Orders.Response;

namespace EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Products)
                .AsQueryable();

            if (request.OrderId != null)
            {
                query = query.Where(o => o.Id == request.OrderId);
            }

            if (request.CustomerId != null)
            {
                query = query.Where(o => o.CustomerId == request.CustomerId);
            }

            var order = await query.FirstOrDefaultAsync(cancellationToken);

            if (order == null) return null;

            return _mapper.Map<OrderDto>(order);
        }
    }
}
