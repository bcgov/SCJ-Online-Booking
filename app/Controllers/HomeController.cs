using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using SCJ.Booking.MVC.Utils;

namespace SCJ.Booking.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("~/sso")]
        public IActionResult Sso()
        {
            ViewBag.AppUrl =
                Request.Query["court"] == "coa"
                    ? "/scjob/booking/coa/CaseSearch"
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
