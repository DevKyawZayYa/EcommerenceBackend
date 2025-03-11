using EcommerenceBackend.Application.Dto.Shops;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shops.Queries.GetShopByIdQuery
{
    public class GetShopByIdQuery : IRequest<ShopDto>
    {
        public Guid Id { get; set; }
        public GetShopByIdQuery(Guid id) => Id = id;
    }
}
