using AutoMapper;
using EcommerenceBackend.Application.Dto.Shops;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shops.Queries.GetShopByIdQuery
{
    public class GetShopByIdQueryHandler : IRequestHandler<GetShopByIdQuery, ShopDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetShopByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShopDto> Handle(GetShopByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id == null)
                    throw new ArgumentNullException(nameof(request.Id), "Shop ID cannot be null.");

                    var shop = await _context.Shops.FirstOrDefaultAsync(s => s.ShopId == request.Id, cancellationToken);
                    return _mapper.Map<ShopDto>(shop);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Get Shop By Id: {ex.Message}");
                throw;
            }

        }
    }
}
