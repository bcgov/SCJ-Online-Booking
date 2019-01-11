using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Models;

namespace SCJ.Booking.MVC.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult BookYourHearing()
        {
            return View();
        }

        public IActionResult Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View("Results", new SearchResultsViewModel());
            }
            else
            {
                return View("BookYourHearing", new SearchViewModel());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
