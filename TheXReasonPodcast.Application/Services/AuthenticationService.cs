using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TheXReasonPodcast.Application.Interfaces;
using TheXReasonPodcast.Application.Models.Requests;
using TheXReasonPodcast.Application.Models.Responses;

namespace TheXReasonPodcast.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly IDictionary<string, string> _usersRefreshTokens = new Dictionary<string, string>();
        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
        {
            { "admin", "admin" }
        };

        private readonly string _key;

        public AuthenticationService(string key)
        {
            _key = key;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest authenticateRequest)
        {
            if (!_users.Any(u => u.Key == authenticateRequest.Username && 
                            u.Value == authenticateRequest.Password))
            {
                return null;
            }

            string accessToken = GenerateToken(authenticateRequest.Username);
            string refreshToken = GenerateRefreshToken();

            AddRefreshTokenInDict(authenticateRequest.Username, refreshToken);

            var response = new AuthenticateResponse(accessToken, refreshToken);

            return response;
        }

        public AuthenticateResponse RefreshCredentials(RefreshCredentialsRequest refreshRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(refreshRequest.AccessToken,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, 
                out validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;

            ValidateIfTokenIsNull(jwtToken);
            ValidateSignatureAlg(jwtToken);

            var username = principal.Identity.Name;

            ValidateIfRefreshTokenExistsInDict(refreshRequest.RefreshToken, username);

            Claim[] claims = new[]
            {
               new Claim(ClaimTypes.Name, principal.Identity.Name),
               new Claim(ClaimTypes.Role, "Manager")
            };

            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                                            SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();

            AddRefreshTokenInDict(username, refreshToken);

            var response = new AuthenticateResponse(accessToken, refreshToken);

            return response;
        }

        //TODO - Add Roles
        private string GenerateToken(string username)
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
            string accessToken = tokenHandler.WriteToken(token);

            return accessToken;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            string base64String = Convert.ToBase64String(randomNumber);

            return base64String;
        }

        private void AddRefreshTokenInDict(string username, string refreshToken)
        {
            if (_usersRefreshTokens.ContainsKey(username))
            {
                _usersRefreshTokens[username] = refreshToken;
            }
            else
            {
                _usersRefreshTokens.Add(username, refreshToken);
            }
        }

        private void ValidateIfTokenIsNull(JwtSecurityToken jwtToken)
        {
            if (jwtToken == null)
            {
                throw new SecurityTokenException("The token is invalid");
            }
        }

        private void ValidateSignatureAlg(JwtSecurityToken jwtToken)
        {
            if (!jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("The signature algorithm is invalid.");
            }
        }

        private void ValidateIfRefreshTokenExistsInDict(string refreshToken, string username)
        {
            if (refreshToken != _usersRefreshTokens[username])
            {
                throw new SecurityTokenException("The refresh token does not exist");
            }
        }
    }
}