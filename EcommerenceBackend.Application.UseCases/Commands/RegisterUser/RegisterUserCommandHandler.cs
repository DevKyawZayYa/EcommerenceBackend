using EcommerenceBackend.Application.Domain.Entities;
using EcommerenceBackend.Application.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.UseCases.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IValidator<RegisterUserCommand> _validator;
        private readonly ApplicationDbContext _context;

        public RegisterUserCommandHandler(IValidator<RegisterUserCommand> validator,ApplicationDbContext context)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _context = context;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);

            var newUser = new User
            {
                Id = request.Id,
                FirstName = new FirstName(request.FirstName!),
                LastName = new LastName(request.LastName!),
                Email = request.Email!,
                Password = hashedPassword,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _context.Users.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
