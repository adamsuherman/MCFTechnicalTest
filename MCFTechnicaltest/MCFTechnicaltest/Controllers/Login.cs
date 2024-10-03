using MCFTechnicaltest.Context;
using MCFTechnicaltest.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MCFTechnicaltest.Controllers
{
    public class Login
    {
        private IConfiguration _configuration;
        private readonly CodingTestContext _context;
        public Login(IConfiguration configuration, CodingTestContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public UserLogin CheckUser(UserLogin user)
        {
            UserLogin _userLogin = new UserLogin();
            try
            {
                var result = _context.ms_user.Where(a => a.user_name == user.username 
                && a.password == user.password 
                &&  a.is_active==true).ToList();

                //if(result.Count == 0) check = false;
                //else check = true;
                _userLogin.username = result.FirstOrDefault().user_name;
                _userLogin.password = result.FirstOrDefault().password;
            }
            catch(Exception ex)
            {

            }

            return _userLogin;
        }

        public UserLogin AuthenticateUser(UserLogin user)
        {
            UserLogin _user = null;
            Login login = new Login(_configuration, _context);
            _user = login.CheckUser(user);
            if (_user != null)
            {
                _user = new UserLogin { username =  _user.username};
            }
            return _user;
        }

        public string GenerateToken(UserLogin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
