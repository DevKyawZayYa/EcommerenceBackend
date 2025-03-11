using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shops.Commands.CreateShop
{
    public class CreateShopCommand : IRequest<Guid>
    {
        public string ShopName { get; set; }
        public int OwnerId { get; set; }
        public string Location { get; set; }
        public string ContactInfo { get; set; }
        public bool IsVerified { get; set; }
    }
}
