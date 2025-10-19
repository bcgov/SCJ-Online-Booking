using System;
using System.Linq;
using System.Threading.Tasks;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.RemoteAPIs.Fixtures;

// ReSharper disable once CheckNamespace
namespace SCJ.OnlineBooking
{
    /// <summary>
    ///     Test implementation of OnlineBookingClient
    /// </summary>
    public class FakeOnlineBookingClient : IOnlineBooking
    {
        public async Task<CourtFile[]> scCaseNumberValidAsync(string caseNum)
        {
            await Task.Delay(100);

            var result = Array.Empty<CourtFile>();

            //VAE23222 -- Vancouver (VA) / Family Court / #23222
            if (caseNum == "VAE23222" || caseNum == "VA23222")
            {
                result = new[]
                {
                    new CourtFile
                    {
                        courtClassCode = "E",
                        courtFileNumber = "23222",
                        courtLevelCode = "S",
                        CEISLocationId = 9067.0001m,
                        physicalFileId = 3879m,
                        styleOfCause = "DOE, Jane v TESTING, John",
                        fairUseSort = 1,
                        futureTrialHearing = false
                    }
                };
            }

            //KE111 -- Kelowna (KE) / Any Court (empty string) / #111
            if (caseNum == "KE111" || caseNum == "KEM111" || caseNum == "KES111")
            {
                result = new[]
                {
                    new CourtFile
                    {
                        courtClassCode = "M",
                        courtFileNumber = "111",
                        courtLevelCode = "S",
                        CEISLocationId = 83.0001m,
                        physicalFileId = 2109m,
                        styleOfCause = null,
                        fairUseSort = 0,
                        futureTrialHearing = true
                    },
                    new CourtFile
                    {
                        courtClassCode = "S",
                        courtFileNumber = "111",
                        courtLevelCode = "S",
                        CEISLocationId = 83.0001m,
                        physicalFileId = 1063m,
                        styleOfCause = "SIMPSON, Marge v SIMPSON, Homer",
                        fairUseSort = 3,
                        futureTrialHearing = false
                    }
                };
            }

            return result.ToList().OrderBy(x => x.styleOfCause).ToArray();
        }

        public async Task<Location[]> scGetLocationsAsync()
        {
            await Task.Delay(100);

            return ScLocationFixture.All;
        }

        public async Task<AvailableDatesByLocation> scConfAvailableDatesByLocationAsync(
            int locationID,
            int hearingTypeID
        )
        {
            await Task.Delay(100);
            var result = ScAvailableDatesByLocationFixture.AvailableDatesResult;
            result.AvailableDates = result
                .AvailableDates.Where(x => x.Date_Time > DateTime.Now)
                .ToArray();
            return result;
        }

        public async Task<BookingHearingResult> scConfBookHearingAsync(BookHearingInfo bookInfo)
        {
            await Task.Delay(100);
            return ScBookingHearingResultFixture.Success;
        }

        public async Task<COACaseList> coaCaseNumberValidAsync(string caseNum)
        {
            await Task.Delay(100);

            if (caseNum.ToUpper() == "CA39029")
            {
                return CoAClassInfoFixture.CivilCase;
            }

            if (caseNum.ToUpper() == "CA42024")
            {
                return CoAClassInfoFixture.CriminalCase;
            }

            //Fake case with a child
            if (caseNum.ToUpper() == "CA39000")
            {
                return CoAClassInfoFixture.CivilCaseWithChildCA39000;
            }

            //Fake case with a parent
            if (caseNum.ToUpper() == "CA39001")
            {
                return CoAClassInfoFixture.CivilCaseWithParentCA39001;
            }

            //Fake case with a parent
            if (caseNum.ToUpper() == "CA39002")
            {
                return CoAClassInfoFixture.CivilCaseWithParentCA39002;
            }

            return CoAClassInfoFixture.NotFound;
        }

        public async Task<CoAAvailableDates> coaAvailableAppealDatesAsync()
        {
            await Task.Delay(100);

            var result = CoAAvailableDatesFixture.Dates;
            result.AvailableDates = result
                .AvailableDates.Where(x => x.scheduleDate > DateTime.Now)
                .ToArray();
            return result;
        }

        public async Task<BookingHearingResult> coaQueueAppealHearingAsync(
            CoABookingHearingInfo bookingInfo
        )
        {
            await Task.Delay(100);

            return ScBookingHearingResultFixture.Success;
        }

        public async Task<CoAChambersAvailableDates> coaAvailableCHDatesAsync()
        {
            await Task.Delay(100);

            var result = CoAAvailableDatesFixture.ChambersDates;
            result.AvailableDates = result
                .AvailableDates.Where(x => x.scheduleDate > DateTime.Now)
                .ToArray();
            return result;
        }

        public async Task<BookingHearingResult> coaQueueCHHearingAsync(
            CoAChambersBookingHearingInfo bookingInfo
        )
        {
            await Task.Delay(100);

            return ScBookingHearingResultFixture.Success;
        }

        public async Task<CoAChambersApplications[]> coaCHApplicationListAsync(string type)
        {
            await Task.Delay(100);

            if (type == "Criminal")
            {
                return CoAChambersApplicationsFixture.CoAChambersApplicationsCriminal;
            }

            if (type == "Civil")
            {
                return CoAChambersApplicationsFixture.CoAChambersApplicationsCivil;
            }

            return null;
        }

        public async Task<FormulaLocation[]> scAvailableFormulasByHearingTypeAndLocationAsync(
            string locationID,
            string formula,
            string hearingTypeId
        )
        {
            await Task.Delay(100);

            if (string.IsNullOrEmpty(hearingTypeId))
            {
                var results = ScFormulaLocationsFixture
                    .Locations(9001)
                    .Concat(ScFormulaLocationsFixture.Locations(9012));

                // filter the results to exclude anything that doesn't match the other filters
                if (!string.IsNullOrEmpty(locationID))
                {
                    results = results.Where(x => x.BookingLocationID == int.Parse(locationID));
                }

                if (!string.IsNullOrEmpty(formula))
                {
                    results = results.Where(x => x.FormulaType == formula);
                }

                return results.ToArray();
            }

            if (string.IsNullOrEmpty(locationID) && string.IsNullOrEmpty(formula))
            {
                return ScFormulaLocationsFixture.Locations(int.Parse(hearingTypeId));
            }

            if (string.IsNullOrEmpty(locationID))
            {
                return ScFormulaLocationsFixture
                    .Locations(int.Parse(hearingTypeId))
                    .Where(l => l.FormulaType == formula)
                    .ToArray();
            }

            if (string.IsNullOrEmpty(formula))
            {
                return ScFormulaLocationsFixture
                    .Locations(int.Parse(hearingTypeId))
                    .Where(l => l.LocationID == int.Parse(locationID))
                    .ToArray();
            }

            return ScFormulaLocationsFixture
                .Locations(int.Parse(hearingTypeId))
                .Where(l => l.FormulaType == formula && l.LocationID == int.Parse(locationID))
                .ToArray();
        }

        public async Task<AvailableTrialDatesResult> scAvailableDatesByHearingTypeAndLocationAsync(
            AvailableTrialDatesRequestInfo requestInfo
        )
        {
            await Task.Delay(100);
            // todo: need to alter the response to match the request
            return ScAvailableTrialDatesFixture.Dates;
        }

        public async Task<BookingHearingResult> scTrialBookHearingAsync(
            BookTrialHearingInfo bookingInfo
        )
        {
            if (bookingInfo.FormulaType == ScFormulaType.FairUseBooking)
            {
                Random r = new Random();
                int n = r.Next(0, 100);

                if (bookingInfo.HearingType == ScHearingType.TRIAL)
                {
                    // fair use trial bookings fail 85% of the time for test purposes
                    return n > 85
                        ? ScBookingHearingResultFixture.Success
                        : ScBookingHearingResultFixture.SupremeCourtFailure;
                }

                if (bookingInfo.HearingType == ScHearingType.UNMET_DEMAND)
                {
                    // unmet demand bookings fail 10% of the time for test purposes
                    return n > 10
                        ? ScBookingHearingResultFixture.Success
                        : ScBookingHearingResultFixture.SupremeCourtFailure;
                }
            }

            await Task.Delay(100);
            return ScBookingHearingResultFixture.Success;
        }

        public async Task<string[]> scGetAvailableBookingTypesAsync()
        {
            await Task.Delay(100);
            return ScAvailableBookingTypesFixture.BookingTypes;
        }

        public async Task<BookingHearingResult> scCHBookHearingAsync(
            BookingSCCHHearingInfo bookingInfo
        )
        {
            if (bookingInfo.FormulaType == ScFormulaType.FairUseBooking)
            {
                Random r = new Random();
                int n = r.Next(0, 100);

                if (bookingInfo.HearingTypeId != ScHearingType.LONG_CHAMBERS)
                {
                    // fair use trial bookings fail 85% of the time for test purposes
                    return n > 85
                        ? ScBookingHearingResultFixture.Success
                        : ScBookingHearingResultFixture.SupremeCourtFailure;
                }

                if (bookingInfo.HearingTypeId == ScHearingType.UNMET_DEMAND)
                {
                    // unmet demand bookings fail 10% of the time for test purposes
                    return n > 10
                        ? ScBookingHearingResultFixture.Success
                        : ScBookingHearingResultFixture.SupremeCourtFailure;
                }
            }

            await Task.Delay(100);
            return ScBookingHearingResultFixture.Success;
        }

        public async Task<SCCHHearingSubTypeDetails[]> scCHHearingSubTypeAsync()
        {
            await Task.Delay(100);

            return ScLongChambersHearingSubTypes.All;
        }
    }
}
