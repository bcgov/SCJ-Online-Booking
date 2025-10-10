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
        [Route("~/booking/sc-long-chambers/available-dates")]
        public new async Task<IActionResult> AvailableDatesAsync()
        {
            return await base.AvailableDatesAsync();
        }

        [HttpPost]
        [Route("~/booking/sc-long-chambers/available-dates")]
        public new async Task<IActionResult> AvailableDatesAsync(ScAvailableSlotsViewModel model)
        {
            return await base.AvailableDatesAsync(model);
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
            return base.AppearanceBooked();
        }

        [HttpGet]
        [Route("~/booking/sc-long-chambers/chambers-request-submitted")]
        public new IActionResult RequestSubmitted()
        {
            return base.RequestSubmitted();
        }
    }
}
