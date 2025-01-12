using EcommerenceBackend.Application.Domain.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerenceBackend.Application.Domain.Services
{
    public class JwtTokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        //public string GenerateToken(User user)
        //{
        //var tokenHandler = new JwtSecurityTokenHandler();
        //var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        //var tokenDescriptor = new SecurityTokenDescriptor
        //{
        //    Subject = new ClaimsIdentity(new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.CustomerId.ToString()),
        //        new Claim(ClaimTypes.Email, user.Email),
        //        // Add other claims as needed
        //    }),
        //    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
        //    Issuer = _jwtSettings.Issuer,
        //    Audience = _jwtSettings.Audience,
        //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //};
        //var token = tokenHandler.CreateToken(tokenDescriptor);
        //return tokenHandler.WriteToken(token);
        // }
    }
}
