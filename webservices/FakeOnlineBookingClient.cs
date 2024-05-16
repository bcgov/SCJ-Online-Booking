using System;
using System.Linq;
using System.Threading.Tasks;
using SCJ.Booking.RemoteAPIs.Fixtures;

// ReSharper disable once CheckNamespace
namespace SCJ.OnlineBooking
{
    /// <summary>
    ///     Test implementation of OnlineBookingClient
    /// </summary>
    public class FakeOnlineBookingClient : IOnlineBooking
    {
        // todo: added futureTrialHearing and fairUseSort
        public async Task<CourtFile[]> caseNumberValidAsync(string caseNum)
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
            if (caseNum == "KE111" || caseNum == "KEM111" || caseNum == "KEG111")
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
                        courtClassCode = "G",
                        courtFileNumber = "111",
                        courtLevelCode = "S",
                        CEISLocationId = 83.0001m,
                        physicalFileId = 1063m,
                        styleOfCause = "GILLESPIE, JANET",
                        fairUseSort = 3,
                        futureTrialHearing = false
                    }
                };
            }

            return result.ToList().OrderBy(x => x.styleOfCause).ToArray();
        }

        public async Task<Location[]> getLocationsAsync()
        {
            await Task.Delay(100);

            return ScLocationFixture.All;
        }

        public async Task<AvailableDatesByLocation> AvailableDatesByLocationAsync(
            int locationID,
            int hearingTypeID
        )
        {
            await Task.Delay(100);
            var result = ScAvailableDatesByLocationFixture.AvailableDatesResult;
            result.AvailableDates = result.AvailableDates
                .Where(x => x.Date_Time > DateTime.Now)
                .ToArray();
            return result;
        }

        public async Task<BookingHearingResult> BookingHearingAsync(BookHearingInfo bookInfo)
        {
            await Task.Delay(100);
            //return new BookingHearingResult
            //{
            //    bookingResult = "Failed - Hearing Booked test"
            //};
            return ScBookingHearingResultFixture.Success;
        }

        public async Task<COACaseList> CoACaseNumberValidAsync(string caseNum)
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

        public async Task<CoAAvailableDates> COAAvailableDatesAsync()
        {
            await Task.Delay(100);

            var result = CoAAvailableDatesFixture.Dates;
            result.AvailableDates = result.AvailableDates
                .Where(x => x.scheduleDate > DateTime.Now)
                .ToArray();
            return result;
        }

        public async Task<BookingHearingResult> CoAQueueHearingAsync(
            CoABookingHearingInfo bookingInfo
        )
        {
            await Task.Delay(100);

            return ScBookingHearingResultFixture.Success;
        }

        public async Task<CoAChambersAvailableDates> CoAAvailableDatesChambersAsync()
        {
            await Task.Delay(100);

            var result = CoAAvailableDatesFixture.ChambersDates;
            result.AvailableDates = result.AvailableDates
                .Where(x => x.scheduleDate > DateTime.Now)
                .ToArray();
            return result;
        }

        public async Task<BookingHearingResult> CoAChambersQueueHearingAsync(
            CoAChambersBookingHearingInfo bookingInfo
        )
        {
            await Task.Delay(100);

            return ScBookingHearingResultFixture.Success;
        }

        public async Task<CoAChambersApplications[]> CoAChambersApplicationsListAsync(string type)
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

        public async Task<FormulaLocation[]> AvailableTrialBookingFormulasByLocationAsync(
            string locationID,
            string formula
        )
        {
            await Task.Delay(100);
            if (string.IsNullOrEmpty(locationID) && string.IsNullOrEmpty(formula))
            {
                return ScFormulaLocationsFixture.Locations;
            }

            if (string.IsNullOrEmpty(locationID))
            {
                return ScFormulaLocationsFixture.Locations
                    .Where(l => l.FormulaType == formula)
                    .ToArray();
            }

            if (string.IsNullOrEmpty(formula))
            {
                return ScFormulaLocationsFixture.Locations
                    .Where(l => l.LocationID == int.Parse(locationID))
                    .ToArray();
            }

            return ScFormulaLocationsFixture.Locations
                .Where(l => l.FormulaType == formula && l.LocationID == int.Parse(locationID))
                .ToArray();
        }

        public async Task<AvailableTrialDatesResult> AvailableTrialDatesByLocationAsync(
            AvailableTrialDatesRequestInfo requestInfo
        )
        {
            await Task.Delay(100);
            // todo: need to alter the response to match the request
            return ScAvailableTrialDatesFixture.Dates;
        }

        public async Task<BookingHearingResult> BookTrialHearingAsync(
            BookTrialHearingInfo bookingInfo
        )
        {
            await Task.Delay(100);
            return ScBookingHearingResultFixture.Success;
        }

        public async Task<string[]> GetAvailableBookingTypesAsync()
        {
            await Task.Delay(100);
            return ScAvailableBookingTypesFixture.BookingTypes;
        }
    }
}
