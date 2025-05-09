using EcommerenceBackend.Application.Dto.Users;
using EcommerenceBackend.Application.Interfaces.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EcommerenceBackend.Application.UseCases.Onboarding.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(ApplicationDbContext context, IConfiguration configuration, IJwtService jwtService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            try
            {
                if (request == null) throw new ArgumentNullException(nameof(request));
                if (string.IsNullOrEmpty(request.Email)) throw new ArgumentException("Email cannot be null or empty.", nameof(request.Email));
                if (string.IsNullOrEmpty(request.Password)) throw new ArgumentException("Password cannot be null or empty.", nameof(request.Password));

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    throw new UnauthorizedAccessException("Invalid Email or Password!");
                }

                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["Secret"];
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];
                var expirationInMinutes = int.Parse(jwtSettings["ExpirationInMinutes"]!);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id!.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(expirationInMinutes),
                    signingCredentials: creds
                );

                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                var refreshToken = _jwtService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);

                return new LoginUserResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                    UserId = user.Id.ToString(),
                    Email = user.Email!,
                    Role = user.Role ?? "User"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling request: {ex.Message}");
                throw;
            }
      
        }
    }
}
