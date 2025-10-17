using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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

            newModel.CaseSearchResults = await _client.scCaseNumberValidAsync(searchableCaseNumber);

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
                EstimatedTrialLength = bookingInfo.BookingLength,
                EstimatedChambersLength = bookingInfo.BookingLength,
                ChambersHearingSubType = bookingInfo.ChambersHearingSubTypeId,
                IsHomeRegistry = bookingInfo.IsHomeRegistry,
                IsLocationChangeFiled = bookingInfo.IsLocationChangeFiled,
                AlternateLocationRegistryId = bookingInfo.AlternateLocationRegistryId,
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

            if (model.HearingTypeId == ScHearingType.TRIAL)
            {
                bookingInfo.IsHomeRegistry = model.IsHomeRegistry;
                bookingInfo.IsLocationChangeFiled = model.IsLocationChangeFiled;
                bookingInfo.BookingLength = model.EstimatedTrialLength;

                if (model.IsHomeRegistry is true)
                {
                    // trial is at the home registry
                    bookingInfo.AlternateLocationRegistryId = bookingInfo.CaseRegistryId;
                }
                else if (model.IsHomeRegistry is false && model.IsLocationChangeFiled is true)
                {
                    // trial is somewhere besides the home registry
                    bookingInfo.AlternateLocationRegistryId = model.AlternateLocationRegistryId;
                }

                bookingInfo.BookingLocationName = await _cache.GetLocationNameAsync(
                    bookingInfo.AlternateLocationRegistryId
                );

                FormulaLocation location = await _cache.GetFormulaLocationAsync(
                    bookingInfo.FormulaType,
                    bookingInfo.AlternateLocationRegistryId,
                    bookingInfo.SelectedCourtFile?.courtClassCode,
                    bookingInfo.HearingTypeId
                );

                if (location is not null)
                {
                    bookingInfo.BookingLocationRegistryId = location.BookingLocationID;
                }
            }
            else if (model.HearingTypeId == ScHearingType.LONG_CHAMBERS)
            {
                bookingInfo.IsHomeRegistry = model.IsHomeRegistry;
                bookingInfo.IsLocationChangeFiled = model.IsLocationChangeFiled;
                bookingInfo.BookingLength = model.EstimatedChambersLength;
                bookingInfo.ChambersHearingSubTypeId = model.ChambersHearingSubType;
                bookingInfo.ChambersHearingSubTypeName = GetChambersHearingSubTypeName(
                    model.ChambersHearingSubType
                );

                if (model.IsHomeRegistry is true)
                {
                    // trial is at the home registry
                    bookingInfo.AlternateLocationRegistryId = bookingInfo.CaseRegistryId;
                }
                else if (model.IsHomeRegistry is false && model.IsLocationChangeFiled is true)
                {
                    // trial is somewhere besides the home registry
                    bookingInfo.AlternateLocationRegistryId = model.AlternateLocationRegistryId;
                }

                bookingInfo.BookingLocationName = await _cache.GetLocationNameAsync(
                    bookingInfo.AlternateLocationRegistryId
                );

                FormulaLocation location = await _cache.GetFormulaLocationAsync(
                    bookingInfo.FormulaType,
                    bookingInfo.AlternateLocationRegistryId,
                    bookingInfo.SelectedCourtFile?.courtClassCode,
                    bookingInfo.HearingTypeId
                );

                if (location is not null)
                {
                    bookingInfo.BookingLocationRegistryId = location.BookingLocationID;
                }
            }
            else
            {
                bookingInfo.BookingLocationRegistryId =
                    await _cache.GetBookingLocationIdAsync(
                        bookingInfo.CaseRegistryId,
                        bookingInfo.HearingTypeId
                    ) ?? bookingInfo.CaseRegistryId;

                bookingInfo.BookingLocationName = await _cache.GetLocationNameAsync(
                    bookingInfo.BookingLocationRegistryId
                );

                bookingInfo.AvailableConferenceDates =
                    await _client.scConfAvailableDatesByLocationAsync(
                        bookingInfo.BookingLocationRegistryId,
                        bookingInfo.HearingTypeId
                    );
            }

            _session.ScBookingInfo = bookingInfo;
        }

        /// <summary>
        ///     Returns booking types from the cache
        /// </summary>
        public async Task<List<string>> GetAvailableBookingTypesAsync()
        {
            var supportedTypes = ScHearingType.HearingTypeIdMap.Keys.Select(keyName => keyName);
            try
            {
                return (await _cache.GetAvailableBookingTypesAsync())
                    .Intersect(supportedTypes)
                    .ToList();
            }
            catch (CommunicationException)
            {
                if (IsLocalDevEnvironment)
                {
                    throw new ConfigurationErrorsException(
                        "scGetAvailableBookingTypesAsync() failed. Check API_ENDPOINT connection or set USE_FAKE_API=true for localdev."
                    );
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<string> GetLocationNameAsync(int registryId)
        {
            return await _cache.GetLocationNameAsync(registryId);
        }

        /// <summary>
        ///     Gets a single court file from the search API
        /// </summary>
        /// <remarks>
        ///     searchableCaseNumber must include a court class for this to work as intended
        /// </remarks>
        private async Task<CourtFile> GetCourtFile(string searchableCaseNumber)
        {
            var searchResult = await _client.scCaseNumberValidAsync(searchableCaseNumber);
            if (!searchResult.Any())
            {
                return null;
            }
            return searchResult[0];
        }

        /// <summary>
        ///    Returns the ChambersHearingSubTypeName for the given subTypeId
        /// </summary>
        private string GetChambersHearingSubTypeName(int? subTypeId)
        {
            var subTypes = _cache.GetChambersHearingSubTypeDictionary();
            string chambersHearingSubTypeName = "";
            if (subTypeId.HasValue && subTypeId > 0 && subTypes.ContainsKey(subTypeId.Value))
            {
                chambersHearingSubTypeName = subTypes[subTypeId.Value] as string;
            }
            return chambersHearingSubTypeName;
        }
    }
}
