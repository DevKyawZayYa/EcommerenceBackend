using AutoMapper;
using EcommerenceBackend.Application.Dto.Orders.Response;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Contexts;
using EcommerenceBackend.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Orders.Queries.GetOrderListByCustomerId
{
    public class GetOrderListByCustomerIdQueryHandler : IRequestHandler<GetOrderListByCustomerIdQuery, List<OrderListByCustomerIdDto>>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRedisService _cache;

        public GetOrderListByCustomerIdQueryHandler(OrderDbContext dbContext, IMapper mapper, IRedisService cache)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<OrderListByCustomerIdDto>> Handle(GetOrderListByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CustomerId == null)
                    throw new ArgumentNullException(nameof(request.CustomerId), "Customer ID cannot be null.");

                // Check Redis cache
                var query = _dbContext.Orders
                    .Where(o => o.CustomerId == request.CustomerId)
                    .AsSplitQuery()
                    .AsNoTracking()
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Products)
                    .ThenInclude(p => p.ProductImages)
                    .OrderByDescending(o => o.OrderDate);

                var orders = await query.ToListAsync(cancellationToken);

                if (orders == null || !orders.Any())
                    return new List<OrderListByCustomerIdDto>();

                var result = _mapper.Map<List<OrderListByCustomerIdDto>>(orders);

                return result;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error in Get Order List By Customer Id: {ex.Message}");
                throw;
            }      
        }
    }
}
