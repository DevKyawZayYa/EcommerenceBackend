using AutoMapper;
using EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Orders.EcommerenceBackend.Application.Domain.Orders;
using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using EcommerenceBackend.Application.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerenceBackend.Infrastructure.Contexts;

namespace EcommerenceBackend.Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(OrderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderItems = request.Items.Select(dto =>
                      new OrderItem(
                          OrderItemId.Create(Guid.NewGuid()),
                          null, // Guid null now
                          new ProductId(dto.ProductId),
                          new Money(dto.Price),
                          new Money(dto.Quantity)
                      )
                  ).ToList();

                var order = new Order(
                    request.CustomerId!,
                    orderItems,
                    request.TaxAmount,
                    request.ShippingCost,
                    request.DiscountAmount,
                    request.Status ?? "Pending",
                    request.PaymentMethod ?? "Stripe",
                    request.PaymentStatus ?? "Unpaid",
                    request.DeliveryStatus ?? "Processing",
                    request.StripeSessionId ?? string.Empty
                );

                await _dbContext.Orders.AddAsync(order, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return order.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating order: {ex.Message}");
                throw;
            }
        }
    }
}
