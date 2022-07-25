using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using MarvelAPI.Models.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace MarvelAPI.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public TokenService(AppDbContext context, IConfiguration config) {
            _context = context;
            _config = config;
        }

        public async Task<TokenResponse> GetTokenAsync(TokenRequest model) {
            var userEntity = await GetValidUserAsync(model);
            if (userEntity is null) {
                return null;
            }
            return GenerateToken(userEntity);
        }

        private async Task<UserEntity> GetValidUserAsync(TokenRequest model) {
            var userEntity = await _context.Users.FirstOrDefaultAsync
            (
                user => user.Username.ToLower() == model.Username.ToLower()
            );
            if (userEntity is null) {
                return null;
            }
            var passwordHasher = new PasswordHasher<UserEntity>();
            var verifyPasswordResult = passwordHasher.VerifyHashedPassword
            (
                userEntity, userEntity.Password, model.Password
            );
            if (verifyPasswordResult == PasswordVerificationResult.Failed) {
                return null;
            }
            return userEntity;
        }

        private TokenResponse GenerateToken(UserEntity entity) {
            var claims = GetClaims(entity);
            var securityKey = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );
            var credentials = new SigningCredentials
            (
                securityKey, SecurityAlgorithms.HmacSha256
            );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenResponse = new TokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                IssuedAt = token.ValidFrom,
                Expires = token.ValidTo
            };
            return tokenResponse;
        }

        private Claim[] GetClaims(UserEntity user) {
            var fullName = $"{user.FirstName} {user.LastName}";
            var name = !string.IsNullOrWhiteSpace(fullName) ? 
            fullName : user.Username;
            var claims = new Claim[]{
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.Username),
                new Claim("Name", name)
            };
            return claims;
        }
    }
}