using MCFTechnicalTestFrontEnd.Helper;
using MCFTechnicalTestFrontEnd.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MCFTechnicalTestFrontEnd.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly API _api;
        private IConfiguration _config;
        public readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _api = new API(_config, _httpContextAccessor);
        }

        public IActionResult Index()
        {
            List<LocationModel> locationModel = new List<LocationModel>();
            try
            {
                locationModel = _api.RequestGetLocation(); 
            }
            catch(Exception ex)
            {

            }
            return View(locationModel);
        }

        [HttpPost]
        public IActionResult Index(IFormCollection col)
        {
            SaveResult result = new SaveResult();
            try
            {
                result = _api.SaveTransaction(col);
            }
            catch(Exception ex)
            {
                result.ResultCode = "0";
                result.ResultMessage = ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
