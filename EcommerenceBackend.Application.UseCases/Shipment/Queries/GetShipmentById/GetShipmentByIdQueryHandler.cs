using AutoMapper;
using EcommerenceBackend.Application.Dto.Shipment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Shipment.Queries.GetShipmentById
{
    public class GetShipmentByIdQueryHandler : IRequestHandler<GetShipmentByIdQuery, ShipmentDto>
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public GetShipmentByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShipmentDto> Handle(GetShipmentByIdQuery request, CancellationToken cancellationToken)
        {
            var shipment = await _context.Shipments.FindAsync(request.ShipmentID);
            return shipment != null ? _mapper.Map<ShipmentDto>(shipment) : null;
        }
    }
}
