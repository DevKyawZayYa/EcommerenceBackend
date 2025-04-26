using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Application.UseCases.Onboarding.Commands.RegisterUser;
using EcommerenceBackend.Infrastructure.Contexts;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using EcommerenceBackend.Application.Domain.Users;

public class RegisterUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldRegisterUser_WhenDataIsValid()
    {
        // Arrange  
        var validatorMock = new Mock<IValidator<RegisterUserCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<User>(It.IsAny<RegisterUserCommand>()))
                  .Returns((RegisterUserCommand cmd) => new User
                  {
                      Id = UserId.Create(Guid.NewGuid()),
                      Email = cmd.Email,
                      Password = BCrypt.Net.BCrypt.HashPassword(cmd.Password),
                      Role = cmd.Role,
                      FirstName = cmd.FirstName,
                      LastName = cmd.LastName
                  });

        var dbContext = CreateInMemoryDbContext(new List<User>());
        var handler = new RegisterUserCommandHandler(validatorMock.Object, dbContext, mapperMock.Object);

        var command = new RegisterUserCommand
        {
            Email = "kzykzy@example.com",
            Password = "ValidPassword123@",
            FirstName = "KZY",
            LastName = "KZY",
            Role = "Customer"
        };

        // Act  
        await handler.Handle(command, CancellationToken.None);

        // Assert  
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == command.Email);
        user.Should().NotBeNull();
        user.Email.Should().Be(command.Email);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange  
        var existingUser = new User
        {
            Id = UserId.Create(Guid.NewGuid()),
            Email = "kzy@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("ExistingPassword123@"),
            Role = "Customer"
        };

        var dbContext = CreateInMemoryDbContext(new List<User> { existingUser });

        var validatorMock = new Mock<IValidator<RegisterUserCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var mapperMock = new Mock<IMapper>();

        var handler = new RegisterUserCommandHandler(validatorMock.Object, dbContext, mapperMock.Object);

        var command = new RegisterUserCommand
        {
            Email = "kzy@example.com", // Same email as existing user  
            Password = "NewPassword123@",
            FirstName = "Jane",
            LastName = "Doe",
            Role = "Customer"
        };

        // Act  
        var act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert  
        await act.Should().ThrowAsync<InvalidOperationException>()
                 .WithMessage("A user with this email already exists.");
    }

    private static ApplicationDbContext CreateInMemoryDbContext(List<User> users)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
            .Options;

        var context = new ApplicationDbContext(options);
        context.Users.AddRange(users);
        context.SaveChanges();
        return context;
    }
}
