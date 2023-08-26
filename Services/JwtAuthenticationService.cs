using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Detailing.Services
{
    public class JwtAuthenticationService : IAuthenticationService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtAuthenticationService(IConfiguration config)
        {
            _secretKey = config.GetSection("Jwt:SecretKey").Value;
            _issuer = config.GetSection("Jwt:Issuer").Value;
            _audience = config.GetSection("Jwt:Audience").Value;

        }

        public string GenerateToken(UserLogin userLogin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userLogin.Email)
            };

            var token = new JwtSecurityToken
            (
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}