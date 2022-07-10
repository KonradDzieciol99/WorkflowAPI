using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;//?
using System.Security.Claims;
using WorkflowApi.DataTransferObject;
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
            _logger.LogWarning("asdfasdfsdaf");
            this._accountService.RegisterUser(dto);
            Tuple<string, DateTime> TokenAndExpireTime = _accountService.GenerateJwt(new UserDto { Email=dto.Email, Password=dto.Password });

            return Ok( new { Token = TokenAndExpireTime.Item1, ExpireMinutes = TokenAndExpireTime.Item2 });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto dto)
        {
            //throw new HttpResponseException(HttpStatusCode.NotFound);
            Tuple<string, DateTime> TokenAndExpireTime = _accountService.GenerateJwt(dto);
            //JsonResult resoult = new JsonResult(new { Token = TokenAndExpireTime.Item1, ExpireMinutes = TokenAndExpireTime.Item2 });
            return Ok( new { Token = TokenAndExpireTime.Item1, ExpireMinutes = TokenAndExpireTime.Item2 });
        }
        [HttpGet("TestTest")]
        public async Task<IActionResult> TestTest(int id)
        {
            //User.GetUsername
            //Use
            //throw new HttpResponseException(HttpStatusCode.NotFound);
            return new JsonResult(new { message = "asdasdas", date = DateTime.Now });

        }
        [HttpPost("Login2TEST")]
        public async Task<IActionResult> Login2( UserDto dto)
        {
            //throw new HttpResponseException(HttpStatusCode.NotFound);
            Tuple<string, DateTime> TokenAndExpireTime = _accountService.GenerateJwt(dto);
            //JsonResult resoult = new JsonResult(new { Token = TokenAndExpireTime.Item1, ExpireMinutes = TokenAndExpireTime.Item2 });
            return Ok(new { Token = TokenAndExpireTime.Item1, ExpireMinutes = TokenAndExpireTime.Item2 });
        }



        [HttpGet("Test")]
        public async Task<IActionResult> Test(int id)
        {
            
            
            return new JsonResult(new { message = "This is a JSON result.", date = DateTime.Now });
        }

        [HttpGet("ForLogIn")]
        
        public async Task<IActionResult> ForLogIn(int id)
        {
            //User.FindFirst(Claim)
            //List<ClaimsIdentity> claims = new List<ClaimsIdentity>(); 
            foreach (var item in HttpContext.User.Claims)
            {
                _logger.LogInformation(item.ToString());
                //claims.Add(item.);
            }
            
            return Ok();
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{ 
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
