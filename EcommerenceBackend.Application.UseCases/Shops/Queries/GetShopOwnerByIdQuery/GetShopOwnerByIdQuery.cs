using EcommerenceBackend.Application.Dto.Shops;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shops.Queries.GetShopOwnerByIdQuery
{
    public class GetShopOwnerByIdQuery : IRequest<ShopOwnerDto>
    {
        public Guid Id { get; set; }
        public GetShopOwnerByIdQuery(Guid id) => Id = id;
    }
}
