using DAL_Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationBL.Services.Interfaces;

namespace UserRegistrationBL.Services
{
    public class JwtTokenProviderService : IJwtTokenProviderService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly double _tokenExpirationInMinutes;

        public JwtTokenProviderService(IConfiguration configuration)
        {
            _secretKey = configuration.GetSection("JwtSettings").GetSection("SecretKey").Value;
            _issuer = configuration.GetSection("JwtSettings").GetSection("Issuer").Value;
            _audience = configuration.GetSection("JwtSettings").GetSection("Audience").Value;
            _tokenExpirationInMinutes = double.Parse(configuration.GetSection("JwtSettings").GetSection("ExpirationMinutes").Value);
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenExpirationInMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = creds
            };

            var tokenHandler = new JsonWebTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescripter);

            return token;
        }
    }
}
