using Maintenance.Application.Security;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.FreelancerEntites;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken<TUser>(TUser user) where TUser : class 
        {
            var claims = new List<Claim>();

            if (user is Freelancer freelancer)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, freelancer.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, freelancer.Email));
                claims.Add(new Claim("UserType", "Freelancer"));
            }
            else if (user is Client client)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, client.Email));
                claims.Add(new Claim("UserType", "Client"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
