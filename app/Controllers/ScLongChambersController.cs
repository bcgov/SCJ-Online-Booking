using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels.SC;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/sc-long-chambers/[action]")]
    [Authorize]
    public class ScLongChambersController : ScLotteryEnabledControllerBase
    {
        public ScLongChambersController(
            SessionService sessionService,
            ScCoreService scCoreService,
            ScLongChambersBookingService scLongChambersBookingService
        )
            : base(sessionService, scCoreService, scLongChambersBookingService) { }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/available-times")]
        public new async Task<IActionResult> AvailableTimesAsync()
        {
            return await base.AvailableTimesAsync();
        }

        [HttpPost]
        [Route("~/booking/sc-long-chambers/available-times")]
        public new async Task<IActionResult> AvailableTimesAsync(ScAvailableTimesViewModel model)
        {
            return await base.AvailableTimesAsync(model);
        }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/case-confirm")]
        public new async Task<IActionResult> CaseConfirmAsync()
        {
            return await base.CaseConfirmAsync();
        }

        [HttpPost]
        [Route("~/booking/sc-long-chambers/case-confirm")]
        public async Task<IActionResult> CaseConfirm(ScCaseConfirmViewModel model)
        {
            return await base.CaseConfirm(model, "ChambersBooked");
        }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/chambers-booked")]
        public IActionResult ChambersBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/chambers-request-submitted")]
        public IActionResult RequestSubmitted()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
