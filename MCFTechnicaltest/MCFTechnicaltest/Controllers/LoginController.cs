using MCFTechnicaltest.Model;
using MCFTechnicaltest.Models.CodingTest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MCFTechnicaltest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config) 
        {
            _config = config;
        }

        private UserLogin AuthenticateUser(UserLogin user)
        {
            UserLogin _user = null;
            if(user.username == "adam.suherman" && user.password == "12345")
            {
                _user = new UserLogin { username = "Adam Suherman" };
            }
            return _user;
        }

        private string GenerateToken(UserLogin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost(Name ="Login")]
        public IActionResult Login(UserLogin user)
        {
            var users = AuthenticateUser(user);
            if(users != null)
            {
                var token = GenerateToken(users);
                return Ok(new { token = token });
            }
            return Unauthorized();
        }

    }
}
