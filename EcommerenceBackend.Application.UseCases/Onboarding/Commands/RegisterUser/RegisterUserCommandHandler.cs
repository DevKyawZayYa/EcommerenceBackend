using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Application.Domain.Users;

namespace EcommerenceBackend.Application.UseCases.Onboarding.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IValidator<RegisterUserCommand> _validator;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(
            IValidator<RegisterUserCommand> validator,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the request
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new InvalidOperationException("Validation failed.");
                }

                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

                if (existingUser != null)
                {
                    throw new InvalidOperationException("A user with this email already exists.");
                }

                var newUser = _mapper.Map<EcommerenceBackend.Application.Domain.Users.User>(request);
                newUser.Id = new UserId(Guid.NewGuid()); // Generate a new ID for the user
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
                newUser.CreatedDate = DateTime.UtcNow;
                newUser.IsActive = true;

                await _context.Users.AddAsync(newUser, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Validation error: " + ex.Message);
            }      
        }
    }
}
