using EcommerenceBackend.Application.Domain.Shops;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shops.Commands.CreateShopOwner
{
    public class CreateShopOwnerCommandHandler : IRequestHandler<CreateShopOwnerCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateShopOwnerCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateShopOwnerCommand request, CancellationToken cancellationToken)
        {
            var shopOwner = new ShopOwner
            {
                ShopOwnerId = Guid.NewGuid(),
                UserId = request.UserId,
                ShopId = request.ShopId,
                BusinessType = request.BusinessType,
                RevenueShare = request.RevenueShare
            };

            _context.ShopOwners.Add(shopOwner);
            await _context.SaveChangesAsync(cancellationToken);
            return shopOwner.ShopOwnerId;
        }
    }
}
