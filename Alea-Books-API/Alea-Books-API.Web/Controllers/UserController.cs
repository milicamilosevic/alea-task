using Alea_Books_API.Web.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Test()
        {
            //var userRole = new IdentityRole
            //{
            //    Name = "User"
            //};
            //await _roleManager.CreateAsync(userRole);
            //var user = await _userManager.FindByEmailAsync("jubo@jubasin.com");
            //var result = await _userManager.AddToRoleAsync(user, "User");
            return Ok("Test");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(Authenticate_RequestModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordCorrect)
            {
                return BadRequest("Wrong password");
            }
            else
            {
                //var issuer = _configuration["TokenConstants:Issuer"];
                //var audience = _configuration["TokenConstants:Audience"];
                var secret = _configuration["TokenConstants:Secret"]; //TODO: Use user secrets for this

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            };

                var secretBytes = Encoding.UTF8.GetBytes(secret);
                var key = new SymmetricSecurityKey(secretBytes);
                var algorithm = SecurityAlgorithms.HmacSha256;

                var signinCredentials = new SigningCredentials(key, algorithm);

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddDays(1),
                    signinCredentials);

                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = tokenJson });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Register_RequestModel request)
        {
            var user = new IdentityUser
            {
                Email = request.Email,
                EmailConfirmed = false,
                UserName = request.Email,
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return Ok(result);
        }
    }
}
