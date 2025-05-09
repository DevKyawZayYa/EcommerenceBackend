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
            try
            {
                if (request.UserId == null)
                    throw new ArgumentNullException(nameof(request.UserId), "User ID cannot be null.");
                if (request.ShopId == null)
                    throw new ArgumentNullException(nameof(request.ShopId), "Shop ID cannot be null.");
                if (request.BusinessType == null)
                    throw new ArgumentNullException(nameof(request.BusinessType), "Business Type cannot be null.");
                if (request.RevenueShare < 0 || request.RevenueShare > 100)
                    throw new ArgumentOutOfRangeException(nameof(request.RevenueShare), "Revenue Share must be between 0 and 100.");

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
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Create Shop Owner: {ex.Message}");
                throw;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error in Create Shop Owner: {ex.Message}");
                throw;
            }           
        }
    }
}
