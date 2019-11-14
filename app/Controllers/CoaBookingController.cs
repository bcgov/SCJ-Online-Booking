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

                else if (model.CaseType == CoaCaseType.Criminal)
                {
                    if (model.LowerCourtOrder == null)
                    {
                        ModelState.AddModelError("LowerCourtOrder", "Please answer this question.");
                    }
                }

                if (model.DateIsAgreed == null)
                {
                    ModelState.AddModelError("DateIsAgreed", "Please answer this question.");
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
            if (model != null && model.SelectedDate != null && !model.TimeSlotExpired)
            //go to confirmation screen
            {
                return new RedirectResult("/scjob/booking/coa/CaseConfirm");
            }

            ModelState.Remove("CaseType");
            ModelState.Remove("IsValidCaseNumber");
            return View(model);
        }

        [HttpGet]
        public IActionResult CaseConfirm()
        {
            var model = new CoaCaseConfirmViewModel();

            CoaSessionBookingInfo bookingInfo = _session.CoaBookingInfo;

            if (string.IsNullOrEmpty(bookingInfo.CaseNumber))
            {
                return Redirect("/scjob/booking/coa/CaseSearch");
            }

            //user information
            var cui = _coaBookingService.GetUserInformation();

            //Time-slot is still available
            var ccm = new CoaCaseConfirmViewModel
            {
                CaseNumber = bookingInfo.CaseNumber,
                CaseType = bookingInfo.CaseType,
                CertificateOfReadiness = bookingInfo.CertificateOfReadiness,
                DateIsAgreed = bookingInfo.DateIsAgreed,
                LowerCourtOrder = bookingInfo.LowerCourtOrder,
                IsFullDay = bookingInfo.IsFullDay,
                HearingTypeId = bookingInfo.HearingTypeId,
                HearingTypeName = bookingInfo.HearingTypeName,
                SelectedDate = bookingInfo.SelectedDate,
                IsUserKnown = true,
                EmailAddress = cui.Email,
                Phone = cui.Phone
            };

            return View(ccm);
        }

        [HttpPost]
        public async Task<IActionResult> CaseConfirm(CoaCaseConfirmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userGuid;
            string userDisplayName;

            if (_coaBookingService.IsLocalDevEnvironment)
            {
                // use fake SiteMinder header values for local development
                userGuid = "072cfc73-e3b9-437b-8012-0b0945f09879";
                userDisplayName = "Matthew Begbie";
            }
            else
            {
                //read smgov_userguid SiteMinder header
                userGuid = _httpContext.Request.Headers.ContainsKey("smgov_userguid")
                    ? _httpContext.Request.Headers["smgov_userguid"].ToString()
                    : string.Empty;

                //read smgov_userdisplayname SiteMinder header
                userDisplayName = _httpContext.Request.Headers.ContainsKey("smgov_userdisplayname")
                    ? _httpContext.Request.Headers["smgov_userdisplayname"].ToString()
                    : string.Empty;
            }

            //make booking
            var result =
                await _coaBookingService.BookCourtCase(model, userGuid, userDisplayName);

            return Redirect(
                $"/scjob/booking/coa/CaseBooked?booked={(result.IsBooked ? "true" : "false")}");
        }




        public IActionResult CaseBooked()
        {
            CoaSessionBookingInfo bookingInfo = _session.CoaBookingInfo;

            if (string.IsNullOrEmpty(bookingInfo.CaseNumber))
            {
                return Redirect("/scjob/booking/coa/CaseSearch");
            }

            return View();
        }
    }
}
