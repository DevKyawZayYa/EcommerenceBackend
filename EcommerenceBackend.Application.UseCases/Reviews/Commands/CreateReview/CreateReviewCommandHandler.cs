using EcommerenceBackend.Application.Domain.Reviews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateReviewCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var review = new Review
                {
                    ReviewId = Guid.NewGuid(),
                    CustomerId = request.CustomerId,
                    ProductId = request.ProductId,
                    Rating = request.Rating,
                    Comment = request.Comment,
                    DateCreated = DateTime.UtcNow
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync(cancellationToken);
                return review.ReviewId;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error in Create Review: {ex.Message}");
                throw;
            }     
        }
    }
}
