using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TheXReasonPodcast.Application.Interfaces;

namespace TheXReasonPodcast.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" },
        };

        private readonly string _key;

        public JwtService(string key)
        {
            _key = key;
        }

        public string Login(string username, string password)
        {
            if (!users.Any(u => u.Key == username &&
                u.Value == password))
            {
                return null;
            }

            return GenerateToken(username, password);
        }

        //TODO - Add Roles
        private string GenerateToken(string username, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Manager")
                }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}