using EcommerenceBackend.Application.Dto.Payments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Payments.Queries.GetInvoiceDetailsByOrderIdQuery
{
    public class GetInvoiceDetailsByOrderIdQuery : IRequest<List<PaymentDto>>
    {
        public Guid OrderId { get; set; }
        public GetInvoiceDetailsByOrderIdQuery(Guid orderId) => OrderId = orderId;
    }
}
