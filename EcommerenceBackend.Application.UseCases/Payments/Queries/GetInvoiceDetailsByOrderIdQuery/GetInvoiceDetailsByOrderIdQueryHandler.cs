using AutoMapper;
using EcommerenceBackend.Application.Dto.Payments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Payments.Queries.GetInvoiceDetailsByOrderIdQuery
{
    public class GetInvoiceDetailsByOrderIdQueryHandler : IRequestHandler<GetInvoiceDetailsByOrderIdQuery, List<PaymentDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetInvoiceDetailsByOrderIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PaymentDto>> Handle(GetInvoiceDetailsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var payments = await _context.Payments
                .Where(p => p.OrderId == request.OrderId).ToListAsync(cancellationToken);
            return _mapper.Map<List<PaymentDto>>(payments);
        }
    }
}
