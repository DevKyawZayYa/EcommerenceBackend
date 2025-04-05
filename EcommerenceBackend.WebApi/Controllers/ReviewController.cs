using EcommerenceBackend.Application.UseCases.Reviews.Commands.CreateReview;
using EcommerenceBackend.Application.UseCases.Reviews.Queries.GetReviewByProductIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerenceBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var reviewId = await _mediator.Send(command);
            return Ok(CreatedAtAction(nameof(CreateReview), new { id = reviewId }, reviewId));
        }
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewByProductId(Guid productId)
        {
            var reviews = await _mediator.Send(new GetReviewByProductIdQuery(productId));
            if (reviews == null || reviews.Count == 0) return NotFound();
            return Ok(reviews);
        }
    }
}
