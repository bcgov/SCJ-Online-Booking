using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.ViewModels.SC;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/sc-trial/[action]")]
    [Authorize]
    public class ScTrialController : ScLotteryEnabledControllerBase
    {
        public ScTrialController(
            SessionService sessionService,
            ScCoreBookingService scCoreBookingService,
            ScTrialBookingService scTrialBookingService
        )
            : base(sessionService, scCoreBookingService, scTrialBookingService) { }

        [HttpGet]
        [Route("~/booking/sc-trial/available-dates")]
        public new async Task<IActionResult> AvailableDatesAsync()
        {
            return await base.AvailableDatesAsync();
        }

        [HttpPost]
        [Route("~/booking/sc-trial/available-dates")]
        public new async Task<IActionResult> AvailableDatesAsync(
            ScLotteryEnabledAvailableSlotsViewModel model
        )
        {
            return await base.AvailableDatesAsync(model);
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
            return base.AppearanceBooked();
        }

        [HttpGet]
        [Route("~/booking/sc-trial/trial-request-submitted")]
        public new IActionResult RequestSubmitted()
        {
            return base.RequestSubmitted();
        }
    }
}
