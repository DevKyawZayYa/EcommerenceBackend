using EcommerenceBackend.Application.Domain.Shops;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shops.Commands.CreateShop
{
    public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateShopCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shop = new Shop
                {
                    ShopId = Guid.NewGuid(),
                    ShopName = request.ShopName,
                    OwnerId = request.OwnerId,
                    Location = request.Location,
                    ContactInfo = request.ContactInfo,
                    CreatedDate = DateTime.UtcNow,
                    IsVerified = request.IsVerified
                };

                _context.Shops.Add(shop);
                await _context.SaveChangesAsync(cancellationToken);
                return shop.ShopId;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Create Shop: {ex.Message}");
                throw;
            }       
        }
    }
}
