using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shops.Commands.CreateShopOwner
{
    public class CreateShopOwnerCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ShopId { get; set; }
        public string BusinessType { get; set; } = string.Empty;
        public decimal RevenueShare { get; set; }
    }
}
