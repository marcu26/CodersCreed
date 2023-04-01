using Core.Database.Entities;
using Infrastructure.Config;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Handlers
{
    public class AuthTokenHandler
    {
        public string GenerateTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtConfiguration.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var idClaim = new Claim("UserId", user.Id.ToString());
            var usernameClaim = new Claim("Username", user.Username);
            var Claims = new List<Claim>();
            Claims.Add(idClaim);
            Claims.Add(usernameClaim);

            foreach(var ur in user.UserRoles)
            {
                Claims.Add(new Claim("roles", ur.Role.Title));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                SigningCredentials = credentials,
                Audience = AppConfig.JwtConfiguration.Audience,
                Issuer = AppConfig.JwtConfiguration.Issuer,
                Expires = DateTime.Now.AddMonths(AppConfig.JwtConfiguration.JwtLifetimeInMonths)

            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
