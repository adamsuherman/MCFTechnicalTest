using MCFTechnicaltest.Context;
using MCFTechnicaltest.Model;
using MCFTechnicaltest.Models.CodingTest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly CodingTestContext _context;
        public LoginController(IConfiguration config, CodingTestContext context) 
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost(Name ="Login")]
        public IActionResult Login(UserLogin user)
        {
            Login login = new Login(_config, _context);
            var users = login.AuthenticateUser(user);
            if(users.username != null)
            {
                var token = login.GenerateToken(users);
                return Ok(new { token = token });
            }
            return Unauthorized();
        }

    }
}
