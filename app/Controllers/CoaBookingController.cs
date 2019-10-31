using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/coa/[action]")]
    public class CoaBookingController : Controller
    {
        public IActionResult CaseSearch()
        {
            return View();
        }
    }
}
