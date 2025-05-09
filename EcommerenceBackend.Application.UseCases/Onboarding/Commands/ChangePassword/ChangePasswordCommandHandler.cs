using EcommerenceBackend.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerenceBackend.Application.UseCases.Onboarding.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public ChangePasswordCommandHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
                if (user == null)
                {
                    return Result.Failure("User not found.");
                }

                if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
                {
                    return Result.Failure("Current password is incorrect.");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success("Password changed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling request: {ex.Message}");
                throw;
            }        
        }
    }
}
