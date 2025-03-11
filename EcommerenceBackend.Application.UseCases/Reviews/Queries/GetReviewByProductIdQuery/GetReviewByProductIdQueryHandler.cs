using AutoMapper;
using EcommerenceBackend.Application.Dto.Reviews;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Reviews.Queries.GetReviewByProductIdQuery
{
    public class GetReviewByProductIdQueryHandler : IRequestHandler<GetReviewByProductIdQuery, List<ReviewDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReviewByProductIdQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReviewDto>> Handle(GetReviewByProductIdQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == request.ProductId).ToListAsync(cancellationToken);
            return _mapper.Map<List<ReviewDto>>(reviews);
        }
    }
}

