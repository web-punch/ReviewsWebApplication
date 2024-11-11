using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reviews.Domain.Interfaces;
using Reviews.Domain.Models;
using Reviews.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConfigurationManager = Reviews.Domain.Services.ConfigurationManager;

namespace ReviewsWebApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<ReviewController> _logger;
        private readonly ILoginService loginService;

        public AuthenticationController(ILogger<ReviewController> logger, ILoginService loginService)
        {
            _logger = logger;
            this.loginService = loginService;
        }

        [HttpGet("CheckLogin")]
        public async Task<IActionResult> CheckLoginAsync(string userName)
        {
            var result = await loginService.CheckLoginAsync(userName);
            return result ? Ok(result) : BadRequest("There is no user with this name!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
            if (login is null)
            {
                return BadRequest("Invalid user request!!!");
            }

            var res = await loginService.CheckLoginAsync(login.UserName);
            if (res)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new JWTTokenResponse
                {
                    Token = tokenString
                });
            }
            return Unauthorized();
        }

        [HttpPost("Addlogin")]
        public async Task<IActionResult> AddLogin(Login login)
        {
            if(login is null || string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest("Invalid request!!!");
            }
            var result = await loginService.AddLoginAsync(login);
            return result ? Ok(result) : Conflict("Login exists!");
        }
    }
}
