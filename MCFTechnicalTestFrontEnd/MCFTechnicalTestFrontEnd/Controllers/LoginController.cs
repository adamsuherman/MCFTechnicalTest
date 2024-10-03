using MCFTechnicalTestFrontEnd.Helper;
using MCFTechnicalTestFrontEnd.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MCFTechnicalTestFrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private readonly API _api;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public LoginController(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _api = new API(_config, _httpContextAccessor);
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection col)
        {
            LoginResultModel objLogin = new LoginResultModel();
            LoginModel loginModel = new LoginModel();
            loginModel.Username = col["InputUsername"];
            loginModel.password = col["InputPassword"];

            objLogin = _api.RequestPostLogin(loginModel);

            if (objLogin.Token != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, objLogin.Username),
                    new Claim("Token", objLogin.Token)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = "User not found.";
            return View();
        }
    }
}
