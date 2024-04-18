using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.COA;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/coa/[action]")]
    [Authorize]
    public class CoaBookingController : Controller
    {
        //Services
        private readonly CoaBookingService _coaBookingService;

        // Strongly typed session
        private readonly SessionService _session;

        //Constructor
        public CoaBookingController(
            SessionService sessionService,
            CoaBookingService coaBookingService
        )
        {
            _session = sessionService;
            _coaBookingService = coaBookingService;
        }

        [HttpGet]
        public IActionResult Restart()
        {
            _session.CoaBookingInfo = null;
            return new RedirectResult("/scjob/booking/coa/CaseSearch");
        }

        [HttpGet]
        public async Task<IActionResult> CaseSearch()
        {
            var bookingInfo = _session.CoaBookingInfo;

            if (bookingInfo == null)
            {
                return View(new CoaCaseSearchViewModel { CaseNumber = "CA" });
            }

            var model = new CoaCaseSearchViewModel
            {
                CaseId = bookingInfo.CaseId,
                HearingTypeId = bookingInfo.HearingTypeId,
                CaseNumber = string.IsNullOrWhiteSpace(bookingInfo.CaseNumber)
                    ? "CA"
                    : bookingInfo.CaseNumber,
                FactumFiled = bookingInfo.FactumFiled,
                IsFullDay = bookingInfo.IsFullDay,
                DateIsAgreed = bookingInfo.DateIsAgreed,
                HearingTypeName = bookingInfo.HearingTypeName,
                CaseType = bookingInfo.CaseType,
                SelectedDate = bookingInfo.SelectedDate,
                IsValidCaseNumber = !string.IsNullOrEmpty(bookingInfo.CaseNumber),
                CaseList = bookingInfo.CaseList,
                SelectedCases = bookingInfo.SelectedCases
            };

            if (model.Step2Complete)
            {
                model.Results = await _coaBookingService.GetAvailableDates(
                    model.MinimumAvailabilityNeeded,
                    model.IsAppealHearing.GetValueOrDefault(false)
                );
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CaseSearch(CoaCaseSearchViewModel model)
        {
            if (!model.Step1Complete)
            {
                if ((model.CaseNumber ?? "CA").ToUpper().Trim() == "CA")
                {
                    ModelState.AddModelError("CaseNumber", "Please provide a court file number.");
                }
            }

            if (model.Step1Complete)
            {
                if (!model.IsAppealHearing.HasValue)
                {
                    ModelState.AddModelError("IsAppealHearing", "Please answer this question.");
                }

                if (!model.DateIsAgreed.HasValue)
                {
                    ModelState.AddModelError("DateIsAgreed", "Please answer this question.");
                }
            }

            model = await _coaBookingService.GetSearchResults(model);

            //populate the available dates if at that step
            if (model.SubmitButton == "GetDates" && model.SelectedDate == null)
            {
                model.Results = await _coaBookingService.GetAvailableDates(
                    model.MinimumAvailabilityNeeded,
                    model.IsAppealHearing.GetValueOrDefault(false)
                );
            }

            //test if the user selected a time-slot that is available
            if (model.SelectedDate != null && !model.TimeSlotExpired)
            //go to confirmation screen
            {
                return new RedirectResult("/scjob/booking/coa/CaseConfirm");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CaseConfirm()
        {
            CoaSessionBookingInfo bookingInfo = _session.CoaBookingInfo;

            if (string.IsNullOrEmpty(bookingInfo.CaseNumber))
            {
                return Redirect("/scjob/booking/coa/CaseSearch");
            }

            //user information
            SessionUserInfo cui = _session.GetUserInformation();

            //Swapping Case Number to the main case file if it was selected from the search result of a sub case file number
            if (bookingInfo.CaseList.Length > 1)
            {
                string finalCaseNumber = bookingInfo.CaseNumber;
                var mainCase = bookingInfo.CaseList.FirstOrDefault(x => x.Main);
                if (mainCase != null)
                {
                    finalCaseNumber =
                        bookingInfo.SelectedCases.FirstOrDefault(x => x == mainCase.Case_Num)
                        ?? bookingInfo.CaseNumber;
                    //Store final main case number back to the session
                    bookingInfo.CaseNumber = finalCaseNumber;
                }

                //Filtering out related cases
                var relatedCaseList = new List<string>();
                foreach (var caseNumber in bookingInfo.SelectedCases)
                {
                    if (caseNumber != finalCaseNumber)
                    {
                        relatedCaseList.Add(caseNumber);
                    }
                }
                //Store final main case number back to the session
                bookingInfo.RelatedCaseList = relatedCaseList;
            }

            var model = new CoaCaseConfirmViewModel
            {
                CaseNumber = bookingInfo.CaseNumber,
                CaseType = bookingInfo.CaseType,
                DateIsAgreed = bookingInfo.DateIsAgreed,
                HearingTypeId = bookingInfo.HearingTypeId,
                HearingTypeName = bookingInfo.HearingTypeName,
                SelectedDate = bookingInfo.SelectedDate,
                EmailAddress = cui.Email,
                Phone = cui.Phone,
                CaseList = bookingInfo.CaseList,
                SelectedCases = bookingInfo.SelectedCases,
                RelatedCaseList = bookingInfo.RelatedCaseList,
                IsAppealHearing = bookingInfo.IsAppealHearing.GetValueOrDefault(false)
            };

            if (bookingInfo.IsAppealHearing is true)
            {
                model.IsFullDay = bookingInfo.IsFullDay;
                model.FactumFiled = bookingInfo.FactumFiled;
            }
            else
            {
                model.IsHalfHour = bookingInfo.IsHalfHour;
                model.SelectedApplicationTypes = bookingInfo.SelectedApplicationTypes;
                model.SelectedApplicationTypeNames =
                    await _coaBookingService.GetApplicationTypeNames(
                        model.CaseType,
                        bookingInfo.SelectedApplicationTypes
                    );
            }

            // save the booking info back to the session
            _session.CoaBookingInfo = bookingInfo;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CaseConfirm(CoaCaseConfirmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ClaimsPrincipal user = HttpContext.User;

            //make booking
            CoaCaseConfirmViewModel result = await _coaBookingService.BookCourtCase(
                model,
                HttpContext.User
            );

            return Redirect(
                $"/scjob/booking/coa/CaseBooked?booked={(result.IsBooked ? "true" : "false")}"
            );
        }

        [HttpGet]
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
