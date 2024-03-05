using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.ViewModels;

namespace SCJ.Booking.MVC.Controllers
{
    public class HomeController : Controller
    {
        //Services
        private readonly ScBookingService _scBookingService;

        //Constructor
        public HomeController(ScBookingService scBookingService)
        {
            _scBookingService = scBookingService;
        }

        public async Task<IActionResult> Index()
        {
            return View(
                new IndexViewModel
                {
                    AvailableBookingTypes = await _scBookingService.GetAvailableBookingTypes()
                }
            );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
