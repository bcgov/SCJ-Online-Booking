using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Models;

namespace SCJ.Booking.MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Logout()
        {
            //TODO:
            //Logout logic to be added once we can do this

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
