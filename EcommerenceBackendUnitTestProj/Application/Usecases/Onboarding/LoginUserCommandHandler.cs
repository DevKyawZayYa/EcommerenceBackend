using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EcommerenceBackend.Application.UseCases.Onboarding.Commands.LoginUser;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using EcommerenceBackend.Infrastructure.Contexts;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using EcommerenceBackend.Application.Domain.Users;

public class LoginUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnLoginUserResponse_WhenCredentialsAreValid()
    {
        // Arrange
        var fakeUser = new User
        {
            Id = UserId.Create(Guid.NewGuid()),
            Email = "kzy@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("ValidPassword123@"),
            Role = "Customer"
        };

        var fakeJwtSettings = new Dictionary<string, string>
        {
            { "JwtSettings:Secret", "SuperSecretKeyForJwtToken" },
            { "JwtSettings:Issuer", "EcomBackend" },
            { "JwtSettings:Audience", "EcomClient" },
            { "JwtSettings:ExpirationInMinutes", "60" }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(fakeJwtSettings)
            .Build();

        var dbContext = CreateInMemoryDbContext(new List<User> { fakeUser });
        var jwtServiceMock = new Mock<IJwtService>();

        jwtServiceMock.Setup(x => x.GenerateRefreshToken())
                      .Returns("FakeRefreshToken");

        var handler = new LoginUserCommandHandler(dbContext, configuration, jwtServiceMock.Object);

        var command = new LoginUserCommand
        {
            Email = "test@example.com",
            Password = "ValidPassword123"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrEmpty();
        result.RefreshToken.Should().Be("FakeRefreshToken");
        result.Email.Should().Be("test@example.com");
        result.Role.Should().Be("Customer");
    }

    [Fact]
    public async Task Handle_ShouldThrowUnauthorizedAccessException_WhenPasswordIsInvalid()
    {
        // Arrange
        var fakeUser = new User
        {
            Id = UserId.Create(Guid.NewGuid()),
            Email = "kzy@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("ValidPassword123@"),
            Role = "Customer"
        };

        var dbContext = CreateInMemoryDbContext(new List<User> { fakeUser });
        var configuration = new Mock<IConfiguration>();
        var jwtServiceMock = new Mock<IJwtService>();

        var handler = new LoginUserCommandHandler(dbContext, configuration.Object, jwtServiceMock.Object);

        var command = new LoginUserCommand
        {
            Email = "kzy@example.com",
            Password = "InvalidPassword"
        };

        // Act
        var act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
                 .WithMessage("Invalid email or password.");
    }

    [Fact]
    public async Task Handle_ShouldThrowUnauthorizedAccessException_WhenUserDoesNotExist()
    {
        // Arrange
        var dbContext = CreateInMemoryDbContext(new List<User>());
        var configuration = new Mock<IConfiguration>();
        var jwtServiceMock = new Mock<IJwtService>();

        var handler = new LoginUserCommandHandler(dbContext, configuration.Object, jwtServiceMock.Object);

        var command = new LoginUserCommand
        {
            Email = "nonexistent@example.com",
            Password = "SomePassword"
        };

        // Act
        var act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
                 .WithMessage("Invalid email or password.");
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
