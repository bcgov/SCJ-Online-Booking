using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels.SC;
using SCJ.Booking.RemoteAPIs;
using SCJ.Booking.TaskRunner.Services;
using SCJ.Booking.TaskRunner.Utils;
using SCJ.OnlineBooking;
using Serilog;

namespace SCJ.Booking.MVC.Services.SC
{
    public abstract class ScLotteryEnabledBookingServiceBase
    {
        public bool IsLocalDevEnvironment { get; protected set; }

        protected readonly IOnlineBooking _client;
        protected readonly ILogger _logger;
        protected readonly SessionService _session;
        protected readonly DataWriterService _dbWriterService;
        private readonly IViewRenderService _viewRenderService;
        protected readonly MailQueueService _mailService;
        protected readonly ScCacheService _cache;
        protected readonly ApplicationDbContext _dbContext;

        //Constructor
        public ScLotteryEnabledBookingServiceBase(
            ApplicationDbContext dbContext,
            IConfiguration configuration,
            SessionService sessionService,
            IViewRenderService viewRenderService,
            ScCacheService cacheService
        )
        {
            //check if this is running on a developer workstation (outside OpenShift)
            string tagName = configuration["TAG_NAME"] ?? "";
            if (tagName.ToLower().Equals("localdev"))
            {
                IsLocalDevEnvironment = true;
            }

            _logger = LogHelper.GetLogger(configuration);
            _client = OnlineBookingClientFactory.GetClient(configuration);
            _dbContext = dbContext;
            _session = sessionService;
            _viewRenderService = viewRenderService;
            _mailService = new MailQueueService(configuration, dbContext);
            _dbWriterService = new DataWriterService(dbContext, cacheService);
            _cache = cacheService;
        }

        /// <summary>
        ///     Loads the available times form with session info
        /// </summary>
        public async Task<ScLotteryEnabledAvailableSlotsViewModel> LoadAvailableDatesFormAsync()
        {
            var bookingInfo = _session.ScBookingInfo;

            //Model instance
            var model = new ScLotteryEnabledAvailableSlotsViewModel
            {
                HearingTypeId = bookingInfo.HearingTypeId,
                SelectedRegularDate = bookingInfo.SelectedRegularDate,
                SelectedFairUseDates = bookingInfo.SelectedFairUseDates,
                SessionInfo = bookingInfo
            };

            model = await LoadAvailableDatesFormulaInfoAsync(model, null);

            model.FormulaType =
                bookingInfo.FairUseFormula is null && bookingInfo.RegularFormula is not null
                    ? ScFormulaType.RegularBooking
                    : bookingInfo.FormulaType;

            return model;
        }

        /// <summary>
        ///    Loads the available times form with formula info
        /// </summary>
        public async Task<ScLotteryEnabledAvailableSlotsViewModel> LoadAvailableDatesFormulaInfoAsync(
            ScLotteryEnabledAvailableSlotsViewModel model,
            FormulaLocation fairUseFormula
        )
        {
            var bookingInfo = _session.ScBookingInfo;

            fairUseFormula ??= await _cache.GetFormulaLocationAsync(
                ScFormulaType.FairUseBooking,
                bookingInfo.AlternateLocationRegistryId,
                bookingInfo.SelectedCourtFile?.courtClassCode ?? "",
                bookingInfo.HearingTypeId
            );

            // The fair use start/end dates are the period inwhich dates are selected for the lottery
            model.FairUseStartDate = fairUseFormula?.FairUseBookingPeriodStartDate;
            model.FairUseEndDate = fairUseFormula?.FairUseBookingPeriodEndDate;

            // The fair use "result date" is the date when the lottery takes place and users are notified
            model.FairUseResultDate = fairUseFormula?.FairUseContactDate;

            // The fair use "selection date" is the period inwhich the trials booked by
            // the lottery take place. Example: "June 2025" for trials in June 2025
            model.FairUseSelectionDate = fairUseFormula?.StartDate;

            return model;
        }

        /// <summary>
        ///    Saves the available times form to session
        /// </summary>
        public void SaveAvailableDatesFormAsync(ScLotteryEnabledAvailableSlotsViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            int maxSelections =
                bookingInfo.HearingTypeId == ScHearingType.LONG_CHAMBERS
                    ? ScGeneral.ScMaxChambersDateSelections
                    : ScGeneral.ScMaxTrialDateSelections;

            bookingInfo.SelectedRegularDate = model.SelectedRegularDate;
            bookingInfo.SelectedFairUseDates = model
                .SelectedFairUseDates.Take(maxSelections)
                .ToList();
            bookingInfo.FormulaType = model.FormulaType;

            _session.ScBookingInfo = bookingInfo;
        }

        /// <summary>
        ///     Renders the template for the email body to a string
        /// </summary>
        protected async Task<string> GetEmailBodyAsync()
        {
            var user = _session.GetUserInformation();
            var booking = _session.ScBookingInfo;

            // lottery date, when users will be notified
            string resultDate =
                booking.FairUseFormula?.FairUseContactDate?.ToString("dddd MMMM d, yyyy")
                ?? "[N/A]";

            // set ViewModel for the email
            var viewModel = new ScLotteryEnabledEmailViewModel(booking)
            {
                EmailAddress = user.Email,
                Phone = user.Phone,
                LocationPrefix = booking.LocationPrefix,
                FullCaseNumber = booking.FullCaseNumber,
                StyleOfCause = booking.SelectedCourtFile.styleOfCause,
                CourtClassName = booking.SelectedCourtClassName,
                CaseLocationName = booking.CaseLocationName,
                BookingLocationName = booking.BookingLocationName,
                HearingLocationName = await _cache.GetLocationNameAsync(
                    booking.AlternateLocationRegistryId
                ),
                ResultDate = resultDate,
                LotteryEntryId = booking.LotteryEntryId,
                ChambersHearingSubTypeName = booking.ChambersHearingSubTypeName
            };

            string template = "";

            if (booking.HearingTypeId == ScHearingType.LONG_CHAMBERS)
            {
                template =
                    booking.FormulaType == ScFormulaType.FairUseBooking
                        ? "ScLongChambers/Emails/FairUse"
                        : "ScLongChambers/Emails/Regular";
            }

            if (booking.HearingTypeId == ScHearingType.TRIAL)
            {
                template =
                    booking.FormulaType == ScFormulaType.FairUseBooking
                        ? "ScTrial/Emails/FairUse"
                        : "ScTrial/Emails/Regular";
            }

            return await _viewRenderService.RenderToStringAsync(template, viewModel);
        }

        public async Task<Tuple<List<DateTime>, FormulaLocation>> GetAvailableBookingDatesAsync(
            string formulaType,
            FormulaLocation formula
        )
        {
            var bookingInfo = _session.ScBookingInfo;
            var courtClassCode = bookingInfo.SelectedCourtFile.courtClassCode ?? "";

            formula ??= await _cache.GetFormulaLocationAsync(
                formulaType,
                bookingInfo.AlternateLocationRegistryId,
                courtClassCode,
                bookingInfo.HearingTypeId
            );

            if (formula == null)
            {
                return Tuple.Create(new List<DateTime>(), (FormulaLocation)null);
            }

            AvailableTrialDatesRequestInfo datesRequestInfo =
                new()
                {
                    LocationID = bookingInfo.AlternateLocationRegistryId,
                    BookingLocationID = formula.BookingLocationID,
                    Courtclass = courtClassCode,
                    FormulaType = formulaType,
                    StartDate = formula.StartDate,
                    EndDate = formula.EndDate,
                    HearingLength = bookingInfo.BookingLength.GetValueOrDefault(1),
                    HearingTypeId = bookingInfo.HearingTypeId
                };

            AvailableTrialDatesResult availableDates =
                await _client.scAvailableDatesByHearingTypeAndLocationAsync(datesRequestInfo);

            if (availableDates.AvailableTrialDates.AvailablesDatesInfo == null)
            {
                return Tuple.Create(new List<DateTime>(), formula);
            }

            var dates = availableDates
                .AvailableTrialDates.AvailablesDatesInfo.Select(d => d.AvailableDate)
                .OrderBy(date => date)
                .ToList();

            return Tuple.Create(dates, formula);
        }

        public string GenerateLotteryEntryId()
        {
            return DateTime.Now.ToString("yyMMddHHmmss");
        }

        public async Task<bool> CheckIfBookingAlreadyRequestedAsync()
        {
            var booking = _session.ScBookingInfo;
            return await CheckIfBookingAlreadyRequestedAsync(booking.HearingTypeId);
        }

        public async Task<bool> CheckIfBookingAlreadyRequestedAsync(int hearingTypeId)
        {
            var booking = _session.ScBookingInfo;

            return await _dbContext.ScLotteryBookingRequests.AnyAsync(r =>
                r.CaseNumber == booking.CaseNumber // has index
                && r.CaseRegistryId == booking.CaseRegistryId
                && r.CourtClassCode == booking.SelectedCourtFile.courtClassCode
                && r.HearingTypeId == hearingTypeId
                && r.IsProcessed == false
            );
        }

        public Task<bool> CheckIfTrialAlreadyRequestedAsync()
        {
            // This is required by the interface so the base controller will work with either
            // the trial service or the chambers service. Only the trial service
            // actually implements this - the chambers service will throw this error if called.
            throw new NotImplementedException();
        }

        public Task<bool> CheckIfLongChambersAlreadyRequestedAsync()
        {
            // This is required by the interface so the base controller will work with either
            // the trial service or the chambers service. Only the chambers service
            // actually implements this - the trial service will throw this error if called.
            throw new NotImplementedException();
        }
    }
}
