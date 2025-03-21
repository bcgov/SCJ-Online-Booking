using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels.SC;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Services.SC
{
    public class ScCoreService
    {
        public readonly bool IsLocalDevEnvironment;

        private readonly IOnlineBooking _client;
        private readonly SessionService _session;
        private readonly ScCacheService _cache;

        //Constructor
        public ScCoreService(
            IConfiguration configuration,
            SessionService sessionService,
            ScCacheService scCacheService
        )
        {
            //check if this is running on a developer workstation (outside OpenShift)
            string tagName = configuration["TAG_NAME"] ?? "";
            if (tagName.ToLower().Equals("localdev"))
            {
                IsLocalDevEnvironment = true;
            }

            _client = OnlineBookingClientFactory.GetClient(configuration);
            _session = sessionService;
            _cache = scCacheService;
        }

        /// <summary>
        ///     Populate the dropdown list for locations for the search
        /// </summary>
        public ScCaseSearchViewModel LoadSearchForm()
        {
            //clear booking info session
            _session.ScBookingInfo = null;

            //Model instance
            return new ScCaseSearchViewModel();
        }

        public ScCaseSearchViewModel ReloadSearchForm()
        {
            var bookingInfo = _session.ScBookingInfo;

            //Model instance
            return new ScCaseSearchViewModel
            {
                CaseRegistryId = bookingInfo.CaseRegistryId,
                CaseLocationName = bookingInfo.CaseLocationName,
                SelectedCaseId = bookingInfo.PhysicalFileId,
                CaseNumber = bookingInfo.CaseNumber,
                CaseSearchResults = bookingInfo.CaseSearchResults,
                SelectedCourtClass = bookingInfo.SelectedCourtClass,
                LocationPrefix = bookingInfo.LocationPrefix
            };
        }

        public async Task<ScCaseSearchViewModel> GetSearchResults(ScCaseSearchViewModel model)
        {
            string prefix =
                (await _cache.GetLocationAsync(model.CaseRegistryId)).locationCode ?? "";

            // Load locations from cache
            var newModel = new ScCaseSearchViewModel
            {
                CaseRegistryId = model.CaseRegistryId,
                CaseNumber = model.CaseNumber,
                SelectedCourtClass = model.SelectedCourtClass,
                LocationPrefix = prefix,
                CaseLocationName = model.CaseLocationName
            };

            var searchableCaseNumber = string.IsNullOrWhiteSpace(model.SelectedCourtClass)
                ? $"{prefix}{model.CaseNumber}"
                : $"{prefix}{model.SelectedCourtClass}{model.CaseNumber}";

            newModel.CaseSearchResults = await _client.caseNumberValidAsync(searchableCaseNumber);

            if ((newModel.CaseSearchResults?.Length ?? 0) > 0)
            {
                _session.ScBookingInfo = new ScSessionBookingInfo
                {
                    CaseNumber = model.CaseNumber,
                    LocationPrefix = newModel.LocationPrefix,
                    CaseSearchResults = newModel.CaseSearchResults,
                    CaseRegistryId = model.CaseRegistryId,
                    CaseLocationName = newModel.CaseLocationName,
                };
            }

            return newModel;
        }

        public async Task SaveSearchForm(ScCaseSearchViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            // we need to do a second API call to get the selectedCourtFile because
            // if we passed it with hidden fields then there would be a security
            // vulnerability where an attacker could modify fairUseSort to get into
            // an earlier lottery round
            CourtFile selectedCourtFile = await GetCourtFile(model.SearchableCaseNumber);

            bookingInfo.PhysicalFileId = model.SelectedCaseId;
            bookingInfo.SelectedCourtClass = model.SelectedCourtClass;
            bookingInfo.FullCaseNumber = model.FullCaseNumber;
            bookingInfo.SelectedCourtClassName = model.SelectedCourtClassName;
            bookingInfo.SelectedCourtFile = selectedCourtFile;
            bookingInfo.LocationPrefix = model.LocationPrefix;

            _session.ScBookingInfo = bookingInfo;
        }

        public ScBookingTypeViewModel LoadBookingTypeForm()
        {
            var bookingInfo = _session.ScBookingInfo;

            // clear formulas (needed when user navigates back)
            if (bookingInfo.FairUseFormula != null || bookingInfo.RegularFormula != null)
            {
                bookingInfo.FairUseFormula = null;
                bookingInfo.RegularFormula = null;
                _session.ScBookingInfo = bookingInfo;
            }

            //Model instance
            return new ScBookingTypeViewModel
            {
                HearingTypeId = bookingInfo.HearingTypeId,
                HearingTypeName = bookingInfo.HearingTypeName,
                EstimatedTrialLength = bookingInfo.EstimatedTrialLength,
                IsHomeRegistry = bookingInfo.IsHomeRegistry,
                IsLocationChangeFiled = bookingInfo.IsLocationChangeFiled,
                TrialLocationRegistryId = bookingInfo.TrialLocationRegistryId,
                FutureTrialBooked = bookingInfo.SelectedCourtFile?.futureTrialHearing ?? false,
                SessionInfo = bookingInfo
            };
        }

        public async Task SaveBookingTypeFormAsync(ScBookingTypeViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            //set hearing type name
            bookingInfo.HearingTypeId = model.HearingTypeId;
            bookingInfo.HearingTypeName = ScHearingType.HearingTypeNameMap[model.HearingTypeId];

            if (model.HearingTypeId != ScHearingType.TRIAL)
            {
                bookingInfo.BookingLocationRegistryId =
                    await _cache.GetBookingLocationIdAsync(
                        bookingInfo.CaseRegistryId,
                        bookingInfo.HearingTypeId
                    ) ?? bookingInfo.CaseRegistryId;

                bookingInfo.BookingLocationName = await _cache.GetLocationNameAsync(
                    bookingInfo.BookingLocationRegistryId
                );

                bookingInfo.AvailableConferenceDates = await _client.AvailableDatesByLocationAsync(
                    bookingInfo.BookingLocationRegistryId,
                    bookingInfo.HearingTypeId
                );
            }
            else
            {
                bookingInfo.IsHomeRegistry = model.IsHomeRegistry;
                bookingInfo.IsLocationChangeFiled = model.IsLocationChangeFiled;
                bookingInfo.EstimatedTrialLength = model.EstimatedTrialLength;

                if (model.IsHomeRegistry is true)
                {
                    // trial is at the home registry
                    bookingInfo.TrialLocationRegistryId = bookingInfo.CaseRegistryId;
                }
                else if (model.IsHomeRegistry is false && model.IsLocationChangeFiled is true)
                {
                    // trial is somewhere besides the home registry
                    bookingInfo.TrialLocationRegistryId = model.TrialLocationRegistryId;
                }

                bookingInfo.BookingLocationName = await _cache.GetLocationNameAsync(
                    bookingInfo.TrialLocationRegistryId
                );

                FormulaLocation location = await _cache.GetFormulaLocationAsync(
                    bookingInfo.TrialFormulaType,
                    bookingInfo.TrialLocationRegistryId,
                    bookingInfo.SelectedCourtFile?.courtClassCode
                );

                if (location is not null)
                {
                    bookingInfo.BookingLocationRegistryId = location.BookingLocationID;
                }
            }

            _session.ScBookingInfo = bookingInfo;
        }

        public async Task<ScAvailableTimesViewModel> LoadAvailableTimesFormAsync()
        {
            var bookingInfo = _session.ScBookingInfo;

            //Model instance
            var model = new ScAvailableTimesViewModel
            {
                CaseNumber = bookingInfo.CaseNumber,
                HearingTypeId = bookingInfo.HearingTypeId,
                AvailableConferenceDates = bookingInfo.AvailableConferenceDates,
                ConferenceLocationRegistryId = bookingInfo.BookingLocationRegistryId,
                SelectedRegularTrialDate = bookingInfo.SelectedRegularTrialDate,
                SelectedFairUseTrialDates = bookingInfo.SelectedFairUseTrialDates,
                SessionInfo = bookingInfo
            };

            model = await LoadAvailableTimesFormulaInfoAsync(model, null);

            model.TrialFormulaType =
                bookingInfo.FairUseFormula is null && bookingInfo.RegularFormula is not null
                    ? ScFormulaType.RegularBooking
                    : bookingInfo.TrialFormulaType;

            return model;
        }

        public async Task<ScAvailableTimesViewModel> LoadAvailableTimesFormulaInfoAsync(
            ScAvailableTimesViewModel model,
            FormulaLocation fairUseFormula
        )
        {
            var bookingInfo = _session.ScBookingInfo;

            fairUseFormula ??= await _cache.GetFormulaLocationAsync(
                ScFormulaType.FairUseBooking,
                bookingInfo.TrialLocationRegistryId,
                bookingInfo.SelectedCourtFile?.courtClassCode ?? ""
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

        public async Task SaveAvailableTimesFormAsync(ScAvailableTimesViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            // check the schedule again to make sure the time slot wasn't taken by someone else
            AvailableDatesByLocation schedule = await _client.AvailableDatesByLocationAsync(
                bookingInfo.BookingLocationRegistryId,
                bookingInfo.HearingTypeId
            );

            if (model.ContainerId > 0)
            {
                model.TimeSlotExpired = !IsTimeStillAvailable(schedule, model.ContainerId);
                bookingInfo.ContainerId = model.ContainerId;
            }

            bookingInfo.SelectedConferenceDate = model.ParsedConferenceDate;
            bookingInfo.SelectedRegularTrialDate = model.SelectedRegularTrialDate;
            bookingInfo.SelectedFairUseTrialDates = model
                .SelectedFairUseTrialDates.Take(ScGeneral.ScMaxTrialDateSelections)
                .ToList();
            bookingInfo.TrialFormulaType = model.TrialFormulaType;

            _session.ScBookingInfo = bookingInfo;
        }

        // Returns booking types from the cache
        public async Task<List<string>> GetAvailableBookingTypesAsync()
        {
            var supportedTypes = ScHearingType.HearingTypeIdMap.Keys.Select(keyName => keyName);
            return (await _cache.GetAvailableBookingTypesAsync())
                .Intersect(supportedTypes)
                .ToList();
        }

        public async Task<string> GetLocationNameAsync(int registryId)
        {
            return await _cache.GetLocationNameAsync(registryId);
        }

        /// <summary>
        ///     Check if a time slot is still available for a court booking
        /// </summary>
        public static bool IsTimeStillAvailable(AvailableDatesByLocation schedule, int containerId)
        {
            //check if the container ID is still available
            return schedule.AvailableDates.Any(x => x.ContainerID == containerId);
        }

        /// <summary>
        ///     Gets a single court file from the search API
        /// </summary>
        /// <remarks>
        ///     searchableCaseNumber must include a court class for this to work as intended
        /// </remarks>
        private async Task<CourtFile> GetCourtFile(string searchableCaseNumber)
        {
            var searchResult = await _client.caseNumberValidAsync(searchableCaseNumber);
            if (!searchResult.Any())
            {
                return null;
            }
            return searchResult[0];
        }
    }
}
