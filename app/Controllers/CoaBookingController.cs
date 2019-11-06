using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.MVC.Data;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/coa/[action]")]
    public class CoaBookingController : Controller
    {
        //Services
        private readonly CoaBookingService _coaBookingService;

        //HttpContext
        private readonly HttpContext _httpContext;

        // Strongly typed session
        private readonly SessionService _session;

        //Give us access to the HostEnvironment properties
        private readonly IViewRenderService _viewRenderService;

        //Constructor
        public CoaBookingController(ApplicationDbContext context, IHttpContextAccessor httpAccessor,
            IConfiguration configuration, SessionService sessionService, IViewRenderService viewRenderService)
        {
            _httpContext = httpAccessor.HttpContext;
            _viewRenderService = viewRenderService;
            _session = sessionService;
            _coaBookingService = new CoaBookingService(context, httpAccessor, configuration, sessionService, viewRenderService);
        }

        [HttpGet]
        public IActionResult CaseSearch()
        {
            var model = new CoaCaseSearchViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CaseSearch(ViewModels.CoaCaseSearchViewModel model)
        {

            if (!string.IsNullOrEmpty(model.CaseType))
            {
                if (model.CaseType == CoaCaseType.Civil)
                {
                    if (model.CertificateOfReadiness == null)
                    {
                        ModelState.AddModelError("CertificateOfReadiness", "Please answer this question.");
                    }
                }

                if (model.CaseType == CoaCaseType.Criminal)
                {
                    if (model.LowerCourtOrder == null)
                    {
                        ModelState.AddModelError("LowerCourtOrder", "Please answer this question.");
                    }
                }

                if (model.DateIsAgreed == null)
                {
                    ModelState.AddModelError("DateisAgree", "Please answer this question.");
                }

                if (model.IsFullDay == null)
                {
                    ModelState.AddModelError("IsFullDay", "Please choose the length of time required for your Hearing.");
                }
            }


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model = await _coaBookingService.GetSearchResults(model);

            //test if the user selected a time-slot that is available
            if (model != null && model.SelectedDate != null  && !model.TimeSlotExpired)
            //go to confirmation screen
            {
                return new RedirectResult("/scjob/booking/sc/CaseConfirm");
            }

            return View(model);
        }
    }
}
