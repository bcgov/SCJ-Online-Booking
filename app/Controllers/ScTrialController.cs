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
    [Route("booking/sc-trial/[action]")]
    [Authorize]
    public class ScTrialController : ScLotteryEnabledControllerBase
    {
        public ScTrialController(
            SessionService sessionService,
            ScCoreService scCoreService,
            ScTrialBookingService scTrialBookingService
        )
            : base(sessionService, scCoreService, scTrialBookingService) { }

        [HttpGet]
        [Route("~/booking/sc-trial/available-times")]
        public new async Task<IActionResult> AvailableTimesAsync()
        {
            return await base.AvailableTimesAsync();
        }

        [HttpPost]
        [Route("~/booking/sc-trial/available-times")]
        public new async Task<IActionResult> AvailableTimesAsync(ScAvailableTimesViewModel model)
        {
            return await base.AvailableTimesAsync(model);
        }

        [HttpGet]
        [Route("~/booking/sc-trial/case-confirm")]
        public new async Task<IActionResult> CaseConfirmAsync()
        {
            return await base.CaseConfirmAsync();
        }

        [HttpPost]
        [Route("~/booking/sc-trial/case-confirm")]
        public async Task<IActionResult> CaseConfirm(ScCaseConfirmViewModel model)
        {
            return await base.CaseConfirm(model, "TrialBooked");
        }

        [HttpGet]
        [Route("~/booking/sc-trial/trial-booked")]
        public IActionResult TrialBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Route("~/booking/sc-trial/trial-request-submitted")]
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
