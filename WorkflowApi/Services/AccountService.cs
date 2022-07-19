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
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AuthSettings _authSettings;
       

        public AccountService(UserManager<AppUser> userManager ,SignInManager<AppUser> signInManager, AuthSettings authSettings)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._authSettings = authSettings;
        }

        public async Task<Tuple<string, DateTime>> GenerateJwt(UserDto dto)
        {
            AppUser user = _userManager.Users
                //.Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            
            if(user is null)
            {
                throw new BadRequestException("Błędny użytkownik lub hasło");
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Błędny użytkownik lub hasło");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
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
        }

        public async Task RegisterUserAsync(RegisterUserDto dto)
        {
            AppUser newUser = new()
            {
                Email = dto.Email,
                UserName = dto.Email

            };
            var resoult = await _userManager.CreateAsync(newUser, dto.Password);

            //should be 401 (Unauthorized)
            if (!resoult.Succeeded)
            {
                string errorDescription = "";
                resoult.Errors.ToList().ForEach(x => errorDescription+= x.Description);
                throw new BadRequestException(errorDescription);
            }
        }
    }
}
