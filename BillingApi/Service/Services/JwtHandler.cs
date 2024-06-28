using BillingApi.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BillingApi.Service.Services
{
    public class JwtHandler(IConfiguration configuration) : IJwtHandler
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(string login, int expiryMinutes = 0)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:SecretKey") ?? string.Empty);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, login),
                    new("Guid", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes((expiryMinutes <= 0
                    ? _configuration.GetValue<int>("Jwt:ExpiryMinutes")
                    : expiryMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
