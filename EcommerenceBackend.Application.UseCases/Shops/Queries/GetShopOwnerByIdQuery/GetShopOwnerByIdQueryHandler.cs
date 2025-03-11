using AutoMapper;
using EcommerenceBackend.Application.Dto.Shops;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shops.Queries.GetShopOwnerByIdQuery
{
    public class GetShopOwnerByIdQueryHandler : IRequestHandler<GetShopOwnerByIdQuery, ShopOwnerDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetShopOwnerByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShopOwnerDto> Handle(GetShopOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var shopOwner = await _context.ShopOwners.FirstOrDefaultAsync(s => s.ShopOwnerId == request.Id, cancellationToken);
            return _mapper.Map<ShopOwnerDto>(shopOwner);
        }
    }
}
