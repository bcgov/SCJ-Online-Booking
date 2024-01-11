using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SCJ.Booking.MVC.Controllers
{
    [Route("booking/coa/[action]")]
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
                CertificateOfReadiness = bookingInfo.CertificateOfReadiness,
                IsFullDay = bookingInfo.IsFullDay,
                DateIsAgreed = bookingInfo.DateIsAgreed,
                HearingTypeName = bookingInfo.HearingTypeName,
                CaseType = bookingInfo.CaseType,
                //LowerCourtOrder = bookingInfo.LowerCourtOrder,
                SelectedDate = bookingInfo.SelectedDate,
                IsValidCaseNumber = !string.IsNullOrEmpty(bookingInfo.CaseNumber),
                CaseList = bookingInfo.CaseList,
                SelectedCases = bookingInfo.SelectedCases
            };

            if (model.Step2Complete)
            {
                model.Results = await _coaBookingService.GetAvailableDates(
                    model.IsFullDay ?? false
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
                if (model.IsAppealHearing == null)
                {
                    ModelState.AddModelError("IsAppealHearing", "Please answer this question.");
                }

                if (model.DateIsAgreed == null)
                {
                    ModelState.AddModelError("DateIsAgreed", "Please answer this question.");
                }
            }

            model = await _coaBookingService.GetSearchResults(model);

            //populate the available dates if at that step
            if (model.SubmitButton == "GetDates" && model.SelectedDate == null)
            {
                model.Results = await _coaBookingService.GetAvailableDates(
                    model.IsFullDay ?? false
                );
            }

            //test if the user selected a time-slot that is available
            if (model.SelectedDate != null && !model.TimeSlotExpired)
            //go to confirmation screen
            {
                return new RedirectResult("/scjob/booking/coa/CaseConfirm");
            }

            return View(model);
            //return new RedirectResult("/scjob/booking/coa/CaseSearch");
        }

        [HttpGet]
        public IActionResult CaseConfirm()
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
                var mainCase = bookingInfo.CaseList.Where(x => x.Main).First();
                var finalCaseNumber =
                    bookingInfo.SelectedCases.Where(x => x == mainCase.Case_Num).FirstOrDefault()
                    ?? bookingInfo.CaseNumber;
                //Store final main case number back to the session
                bookingInfo.CaseNumber = finalCaseNumber;

                //Filtering out related cases
                var relatedCaseList = new List<string>();
                foreach (var x in bookingInfo.SelectedCases)
                {
                    if (x != finalCaseNumber)
                    {
                        relatedCaseList.Add(x);
                    }
                }
                //Store final main case number back to the session
                bookingInfo.RelatedCaseList = relatedCaseList;
            }

            //Time-slot is still available
            var model = new CoaCaseConfirmViewModel
            {
                CaseNumber = bookingInfo.CaseNumber,
                CaseType = bookingInfo.CaseType,
                DateIsAgreed = bookingInfo.DateIsAgreed,
                //LowerCourtOrder = bookingInfo.LowerCourtOrder,
                HearingTypeId = bookingInfo.HearingTypeId,
                HearingTypeName = bookingInfo.HearingTypeName,
                SelectedDate = bookingInfo.SelectedDate,
                EmailAddress = cui.Email,
                Phone = cui.Phone,
                CaseList = bookingInfo.CaseList,
                SelectedCases = bookingInfo.SelectedCases,
                RelatedCaseList = bookingInfo.RelatedCaseList,
                IsAppealHearing = bookingInfo.IsAppealHearing.Value
            };

            if(bookingInfo.IsAppealHearing.Value)
            {
                model.IsFullDay = bookingInfo.IsFullDay;
                model.CertificateOfReadiness = bookingInfo.CertificateOfReadiness;
            }
            else
            {
                model.IsHalfHour = bookingInfo.IsHalfHour;
                model.SelectedApplicationTypes = bookingInfo.SelectedApplicationTypes;
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
                userGuid = HttpContext.Request.Headers.ContainsKey("smgov_userguid")
                    ? HttpContext.Request.Headers["smgov_userguid"].ToString()
                    : string.Empty;

                //read smgov_userdisplayname SiteMinder header
                userDisplayName = HttpContext.Request.Headers.ContainsKey("smgov_userdisplayname")
                    ? HttpContext.Request.Headers["smgov_userdisplayname"].ToString()
                    : string.Empty;
            }

            //make booking
            CoaCaseConfirmViewModel result = await _coaBookingService.BookCourtCase(
                model,
                userGuid,
                userDisplayName
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
