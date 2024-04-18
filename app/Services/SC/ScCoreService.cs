using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.Data;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;
using Serilog;

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
            ApplicationDbContext dbContext,
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
            // Load locations from cache
            var newModel = new ScCaseSearchViewModel
            {
                CaseRegistryId = model.CaseRegistryId,
                CaseNumber = model.CaseNumber,
                SelectedCourtClass = model.SelectedCourtClass,
                CaseLocationName = model.CaseLocationName
            };

            //search the current case number
            (string searchableCaseNumber, newModel.LocationPrefix) = await BuildCaseNumberAsync(
                model.CaseNumber,
                model.CaseRegistryId
            );
            newModel.CaseSearchResults = await _client.caseNumberValidAsync(searchableCaseNumber);

            if ((newModel.CaseSearchResults?.Length ?? 0) == 0)
            {
                //get contact information
                newModel.RegistryContactNumber = GetRegistryContactNumber(model.CaseRegistryId);
            }
            else
            {
                _session.ScBookingInfo = new ScSessionBookingInfo
                {
                    CaseNumber = model.CaseNumber.GetValueOrDefault(0),
                    LocationPrefix = newModel.LocationPrefix,
                    CaseSearchResults = newModel.CaseSearchResults,
                    CaseRegistryId = model.CaseRegistryId,
                    CaseLocationName = newModel.CaseLocationName,
                };
            }

            return newModel;
        }

        public void SaveSearchForm(ScCaseSearchViewModel model)
        {
            var bookingInfo = _session.ScBookingInfo;

            bookingInfo.PhysicalFileId = model.SelectedCaseId;
            bookingInfo.SelectedCourtClass = model.SelectedCourtClass;
            bookingInfo.FullCaseNumber = model.FullCaseNumber;
            bookingInfo.SelectedCourtClassName = model.SelectedCourtClassName;
            bookingInfo.SelectedCourtFile = model.SelectedCourtFile;

            _session.ScBookingInfo = bookingInfo;
        }

        public ScBookingTypeViewModel LoadBookingTypeForm()
        {
            var bookingInfo = _session.ScBookingInfo;

            //Model instance
            return new ScBookingTypeViewModel
            {
                HearingTypeId = bookingInfo.HearingTypeId,
                HearingTypeName = bookingInfo.HearingTypeName,
                EstimatedTrialLength = bookingInfo.EstimatedTrialLength,
                IsHomeRegistry = bookingInfo.IsHomeRegistry,
                IsLocationChangeFiled = bookingInfo.IsLocationChangeFiled,
                TrialLocationRegistryId = bookingInfo.TrialLocationRegistryId,
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

                FormulaLocation location = await GetFormulaLocationAsync(
                    bookingInfo.TrialFormulaType,
                    bookingInfo.TrialLocationRegistryId,
                    bookingInfo.SelectedCourtClass
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

            model = await SetFairUseFormulaInfoAsync(model);

            model.TrialFormulaType =
                bookingInfo.FairUseFormula is null && bookingInfo.RegularFormula is not null
                    ? ScFormulaType.RegularBooking
                    : bookingInfo.TrialFormulaType;

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
                bookingInfo.SelectedConferenceDateTicks = model.SelectedConferenceDate;
                model.TimeSlotExpired = !IsTimeStillAvailable(schedule, model.ContainerId);
                bookingInfo.ContainerId = model.ContainerId;
            }

            bookingInfo.SelectedConferenceDate = model.ParsedConferenceDate;
            bookingInfo.SelectedRegularTrialDate = model.SelectedRegularTrialDate;
            bookingInfo.SelectedFairUseTrialDates = model.SelectedFairUseTrialDates;
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
        ///     Get registry contact number
        /// </summary>
        private string GetRegistryContactNumber(int registryId)
        {
            //TODO:Implement logic to find contact number
            //Need to ask SCJ to add a new field to the locations API
            //for now temporary code to loop it up in a dictionary

            var numbers = new Dictionary<int, string>
            {
                { 1, "604-660-2853" },
                { 2, "250-356-1450" },
                { 3, "604-660-8551" },
                { 4, "250-614-2750" },
                { 6, "250-828-4351" },
                { 7, "250-741-5860" },
                { 9, "250-741-5860" },
                { 10, "604-795-8349" },
                { 11, "250-614-2750" },
                { 12, "250-356-1450" },
                { 13, "250-614-2750" },
                { 15, "250-828-4351" },
                { 17, "250-828-4351" },
                { 18, "250-470-6935" },
                { 20, "250-741-5860" },
                { 21, "250-828-4351" },
                { 22, "250-741-5860" },
                { 24, "250-741-5860" },
                { 25, "250-624-7474" },
                { 26, "250-470-6935" },
                { 27, "250-614-2750" },
                { 28, "250-828-4351" },
                { 29, "250-828-4351" },
                { 30, "250-828-4351" },
                { 31, "250-847-7482" },
                { 32, "250-624-7474" },
                { 33, "250-614-2750" },
                { 34, "250-470-6935" },
                { 82, "604-425-3711" }
            };

            return numbers.ContainsKey(registryId) ? numbers[registryId] : numbers[1];
        }

        /// <summary>
        ///     Fetch location-code for specific case ID and combine it with the case number
        /// </summary>
        private async Task<(string, string)> BuildCaseNumberAsync(int? caseId, int locationId)
        {
            //fetch location prefix
            string prefix = (await _cache.GetLocationAsync(locationId)).locationCode ?? "";

            //return location prefix + case number
            return ($"{prefix}{caseId}", prefix);
        }

        public async Task<ScAvailableTimesViewModel> SetFairUseFormulaInfoAsync(
            ScAvailableTimesViewModel model,
            FormulaLocation fairUseFormula = null
        )
        {
            var bookingInfo = _session.ScBookingInfo;

            fairUseFormula ??= await GetFormulaLocationAsync(
                ScFormulaType.FairUseBooking,
                bookingInfo.TrialLocationRegistryId,
                bookingInfo.SelectedCourtFile.courtClassCode
            );

            model.FairUseStartDate = fairUseFormula?.FairUseBookingPeriodStartDate;
            model.FairUseEndDate = fairUseFormula?.FairUseBookingPeriodEndDate;
            model.FairUseResultDate = fairUseFormula?.FairUseContactDate;
            model.FairUseNoticeDate = fairUseFormula?.FairUseBookingPeriodEndDate;

            return model;
        }

        public async Task<FormulaLocation> GetFormulaLocationAsync(
            string formula,
            int locationId,
            string courtClass
        )
        {
            var formulas = await _cache.AvailableTrialBookingFormulasByLocationAsync();

            // look for a special formula location for the specific courtClass
            var result = formulas.FirstOrDefault(f =>
                f.FormulaType == formula
                && f.LocationID == locationId
                && f.BookingHearingCode == courtClass
            );

            if (result != null)
            {
                return result;
            }

            // if there isn't a special formula location then use the general one
            var all = new[] { "All", "All Other" };

            return formulas.FirstOrDefault(f =>
                f.FormulaType == formula
                && f.LocationID == locationId
                && all.Contains(f.BookingHearingCode)
            );
        }

        /// <summary>
        ///     Check if a time slot is still available for a court booking
        /// </summary>
        public bool IsTimeStillAvailable(AvailableDatesByLocation schedule, int containerId)
        {
            //check if the container ID is still available
            return schedule.AvailableDates.Any(x => x.ContainerID == containerId);
        }
    }
}
