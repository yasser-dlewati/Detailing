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

        public JwtAuthenticationService(IConfiguration config)
        {
            _secretKey = "this is my secret key.";
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
                issuer: "https://localhost:7145/",
                audience: "https://localhost:7145/",
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