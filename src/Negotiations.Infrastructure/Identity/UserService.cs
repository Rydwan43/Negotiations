using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Negotiations.Application.Exceptions;
using Negotiations.Application.Interfaces;
using Negotiations.Application.Models;
using Negotiations.Application.Settings;
using Negotiations.Domain.Entities;
using Negotiations.Infrastructure.DatabaseContext;

namespace Negotiations.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly NegotiationsDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;

        public UserService(IPasswordHasher<User> passwordHasher, NegotiationsDbContext dbContext, JwtSettings jwtSettings)
        {
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _jwtSettings = jwtSettings;
        }

        public string GenerateJwt(LoginUserDto userDto) 
        {
            var user = _dbContext.Users
                .FirstOrDefault(u => u.Login == userDto.Login);

            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, $"{user.Email}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_jwtSettings.JwtIssuer,
                _jwtSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto userDto)
        {
            var newUser = new User()
            {
                Login = userDto.Login,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, userDto.Password);

            newUser.PasswordHash = hashedPassword;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}