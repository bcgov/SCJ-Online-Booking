using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels.SC;

namespace SCJ.Booking.MVC.Controllers
{
    public abstract class ScLotteryEnabledControllerBase : Controller
    {
        protected readonly ScCoreService _scCoreService;

        protected readonly SessionService _session;

        protected readonly IScLotteryEnabledBookingService _bookingService;

        //Constructor
        public ScLotteryEnabledControllerBase(
            SessionService sessionService,
            ScCoreService scCoreService,
            IScLotteryEnabledBookingService bookingService
        )
        {
            _session = sessionService;
            _scCoreService = scCoreService;
            _bookingService = bookingService;
        }

        protected async Task<IActionResult> AvailableDatesAsync()
        {
            var model = await _bookingService.LoadAvailableDatesFormAsync();

            if (string.IsNullOrWhiteSpace(model.CaseNumber))
            {
                return RedirectToAction("Index");
            }

            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            // Chambers bookings: get lists of available chambers dates
            (model.AvailableRegularDates, bookingInfo.RegularFormula) =
                await _bookingService.GetAvailableBookingDatesAsync(
                    ScFormulaType.RegularBooking,
                    bookingInfo.RegularFormula
                );

            (model.AvailableFairUseDates, bookingInfo.FairUseFormula) =
                await _bookingService.GetAvailableBookingDatesAsync(
                    ScFormulaType.FairUseBooking,
                    bookingInfo.FairUseFormula
                );

            _session.ScBookingInfo = bookingInfo;

            if (bookingInfo.FairUseFormula is null)
            {
                model.FormulaType = ScFormulaType.RegularBooking;
            }
            else
            {
                model.FormulaType = bookingInfo.FormulaType ?? ScFormulaType.FairUseBooking;
            }

            if (bookingInfo.HearingTypeId == ScHearingType.LONG_CHAMBERS)
            {
                model.HasExistingLongChambersRequest =
                    await _bookingService.CheckIfLongChambersAlreadyRequestedAsync();
            }

            return View(model);
        }

        protected async Task<IActionResult> AvailableDatesAsync(ScAvailableSlotsViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;
            model.AvailableConferenceDates = bookingInfo.AvailableConferenceDates;

            if (
                model.FormulaType == ScFormulaType.RegularBooking
                && !model.SelectedRegularDate.HasValue
            )
            {
                ModelState.AddModelError(
                    "SelectedRegularDate",
                    "Please choose from one of the available dates."
                );
            }
            else if (
                model.FormulaType == ScFormulaType.FairUseBooking
                && model.SelectedFairUseDates.Count == 0
            )
            {
                ModelState.AddModelError(
                    "SelectedFairUseDates",
                    "Please choose from the available dates."
                );
            }
            else if (model.FormulaType == "")
            {
                // If the formula type field is empty
                // (e.g. user tampered with the form or submitted without JavaScript)
                ModelState.AddModelError("FormulaType", "Please choose what you are booking.");
            }

            if (!ModelState.IsValid)
            {
                model.SessionInfo = bookingInfo;

                model = await _bookingService.LoadAvailableDatesFormulaInfoAsync(
                    model,
                    bookingInfo.FairUseFormula
                );

                (model.AvailableRegularDates, _) =
                    await _bookingService.GetAvailableBookingDatesAsync(
                        ScFormulaType.RegularBooking,
                        null
                    );

                (model.AvailableFairUseDates, _) =
                    await _bookingService.GetAvailableBookingDatesAsync(
                        ScFormulaType.FairUseBooking,
                        null
                    );

                return View(model);
            }

            _bookingService.SaveAvailableDatesFormAsync(model);

            return RedirectToAction("CaseConfirm");
        }

        protected async Task<IActionResult> CaseConfirmAsync()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            //user information
            var user = _session.GetUserInformation();

            string locationName = await _scCoreService.GetLocationNameAsync(
                bookingInfo.AlternateLocationRegistryId
            );

            //Time-slot is still available
            var model = new ScCaseConfirmViewModel
            {
                Date = bookingInfo.FormattedConferenceDate,
                Time = bookingInfo.FormattedConferenceTime,
                TrialLocationName = locationName,
                SelectedRegularDate = bookingInfo.SelectedRegularDate,
                EmailAddress = user.Email,
                Phone = user.Phone,
                SessionInfo = bookingInfo
            };

            return View(model);
        }

        protected async Task<IActionResult> CaseConfirm(
            ScCaseConfirmViewModel model,
            string regularSuccessUrl
        )
        {
            var bookingInfo = _session.ScBookingInfo;
            if (!ModelState.IsValid)
            {
                model.SessionInfo = bookingInfo;
                return View(model);
            }

            ClaimsPrincipal user = HttpContext.User;

            try
            {
                await _bookingService.CreateBookingAsync(model, user);

                if (bookingInfo.FormulaType == ScFormulaType.RegularBooking)
                {
                    // Redirect to "TrialBooked" page for Regular
                    return RedirectToAction(regularSuccessUrl);
                }
                else
                {
                    // Redirect to "RequestSubmitted" page for Fair-Use
                    return RedirectToAction("RequestSubmitted");
                }
            }
            catch (InvalidOperationException ex)
            {
                string errorMessage = ex.Message;
                ModelState.AddModelError("SelectedRegularDate", errorMessage);
                model.SessionInfo = bookingInfo;
                return View(model);
            }
        }

        protected IActionResult AppearanceBooked()
        {
            ScSessionBookingInfo bookingInfo = _session.ScBookingInfo;

            if (bookingInfo.SelectedCourtFile is null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        protected IActionResult RequestSubmitted()
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
