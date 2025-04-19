using AutoMapper;
using EcommerenceBackend.Application.Dto.Orders.Response;
using EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderById;
using EcommerenceBackend.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderListByCustomerId
{
    public class GetOrderListByCustomerIdQueryHandler : IRequestHandler<GetOrderListByCustomerIdQuery, List<OrderListByCustomerIdDto>>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderListByCustomerIdQueryHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<OrderListByCustomerIdDto>> Handle(GetOrderListByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            if (request.CustomerId == null)
            {
                throw new ArgumentNullException(nameof(request.CustomerId), "Customer ID cannot be null.");
            }

            var query = _dbContext.Orders.Where(o => o.CustomerId == request.CustomerId)
                .AsSplitQuery()
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Products).ThenInclude(x=> x.ProductImages)
                .AsQueryable();

            var orders = await query.ToListAsync(cancellationToken);

            if (orders == null || !orders.Any()) return new List<OrderListByCustomerIdDto>();

            return _mapper.Map<List<OrderListByCustomerIdDto>>(orders);
        }
    }
}