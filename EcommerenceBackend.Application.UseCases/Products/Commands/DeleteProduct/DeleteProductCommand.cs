using EcommerenceBackend.Application.Domain.Products.EcommerenceBackend.Application.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public ProductId? ProductId { get; set; }
    }
}
