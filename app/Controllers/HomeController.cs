using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.ViewModels;

namespace SCJ.Booking.MVC.Controllers
{
    public class HomeController : Controller
    {
        //Services
        private readonly ScCoreService _scCoreService;

        //Constructor
        public HomeController(ScCoreService scCoreService)
        {
            _scCoreService = scCoreService;
        }

        public async Task<IActionResult> Index()
        {
            return View(
                new IndexViewModel
                {
                    AvailableBookingTypes = await _scCoreService.GetAvailableBookingTypesAsync()
                }
            );
        }

        [Route("~/sso")]
        public IActionResult Sso()
        {
            ViewBag.AppUrl =
                Request.Query["court"] == "coa"
                    ? "/scjob/booking/coa/case-search"
                    : "/scjob/booking/sc";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("~/Error")]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("~/NotAuthorized")]
        public IActionResult NotAuthorized()
        {
            return View();
        }

        [HttpPost]
        [Route("~/Logout")]
        public IActionResult Logout()
        {
            return new SignOutResult(
                new[]
                {
                    OpenIdConnectDefaults.AuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme
                }
            );
        }
    }
}
