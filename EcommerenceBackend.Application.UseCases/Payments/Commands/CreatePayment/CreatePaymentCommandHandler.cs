using EcommerenceBackend.Application.Domain.Payments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreatePaymentCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var payment = new Payment
                {
                    PaymentId = Guid.NewGuid(),
                    OrderId = request.OrderId,
                    Amount = request.Amount,
                    PaymentMethod = request.PaymentMethod,
                    PaymentStatus = request.PaymentStatus,
                    TransactionID = request.TransactionId,
                    TransactionType = request.TransactionType,
                    PaymentDate = DateTime.UtcNow
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync(cancellationToken);
                return payment.PaymentId;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error in Create Payment: {ex.Message}");
                throw;
            }       
        }
    }
}
