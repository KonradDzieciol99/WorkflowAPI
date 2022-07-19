using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Models;
//using System.Web.Http;
using WorkflowApi.Services;

namespace WorkflowApi.Controllers
{
    //[AutoValidateAntiforgeryToken]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            this._accountService = accountService;
            this._logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            //_logger.LogWarning("asdfasdfsdaf");
            await this._accountService.RegisterUserAsync(dto);
            Tuple<string, DateTime> TokenAndExpireTime = await _accountService.GenerateJwt(new UserDto { Email=dto.Email, Password=dto.Password });

            return Ok( new { Token = TokenAndExpireTime.Item1, ExpireMinutes = TokenAndExpireTime.Item2 });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            Tuple<string, DateTime> TokenAndExpireTime = await _accountService.GenerateJwt(dto);
            return Ok( new { Token = TokenAndExpireTime.Item1, ExpireMinutes = TokenAndExpireTime.Item2 });
        }
    }
}
