using EcommerenceBackend.Application.Dto.Reviews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Reviews.Queries.GetReviewByProductIdQuery
{
    public class GetReviewByProductIdQuery : IRequest<List<ReviewDto>>
    {
        public Guid ProductId { get; set; }
        public GetReviewByProductIdQuery(Guid productId) => ProductId = productId;
    }
}
