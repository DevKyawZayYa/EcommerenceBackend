// EcommerenceBackend.Application.UseCases/Orders/Queries/GetOrderById/GetOrderByIdQueryHandler.cs
using AutoMapper;
using EcommerenceBackend.Application.Domain.Orders;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Application.Dto.Orders.Response;
using EcommerenceBackend.Infrastructure.Contexts;

namespace EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDetailByIdDto>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderDetailByIdDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                if (request.OrderId == null && request.CustomerId == null)
                {
                    throw new ArgumentException("Either OrderId or CustomerId must be provided.");
                }

                var query = _dbContext.Orders
                    .AsSplitQuery().AsNoTracking()
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Products).ThenInclude(x => x.ProductImages)
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

                return _mapper.Map<OrderDetailByIdDto>(order);

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error in Get Order By Id: {ex.Message}");
                throw;
            }
        }
    }
}
