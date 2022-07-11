using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WorkflowApi.Data;
using WorkflowApi.Exceptions;
using WorkflowApi.Models;
using System.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly AuthSettings _authSettings;

        public AccountService(ApplicationDbContext dbContext, IPasswordHasher<AppUser> passwordHasher, AuthSettings authSettings)
        {
            this._dbContext = dbContext;
            this._passwordHasher = passwordHasher;
            this._authSettings = authSettings;
        }

        public Tuple<string, DateTime> GenerateJwt(UserDto dto)
        {
            AppUser user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            
            if(user is null)
            {
                throw new BadRequestException("Błędny użytkownik lub hasło");
            }

            var resoult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(resoult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Błędny użytkownik lub hasło");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };
            
            var issuerAppSettings = _authSettings.jwtIssuer;
            double minutesAppSettings = _authSettings.jwtExpire;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresMinutes = DateTime.Now.AddMinutes(minutesAppSettings);


            var token = new JwtSecurityToken(issuerAppSettings,
                issuerAppSettings,
                claims,
                expires: expiresMinutes,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            
            return Tuple.Create(tokenHandler.WriteToken(token), expiresMinutes);
            //return tokenHandler.WriteToken(token);

        }

        public void RegisterUser(RegisterUserDto dto)
        {
            AppUser newUser = new()
            {
                Email = dto.Email
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
