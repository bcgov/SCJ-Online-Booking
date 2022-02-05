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
        public async Task<CourtFile[]> caseNumberValidAsync(string caseNum)
        {
            await Task.Delay(100);

            var result = Array.Empty<CourtFile>();

            //CRE23222 -- Campbell River (CR) / Family Court / #23222
            if (caseNum == "CRE23222" || caseNum == "CR23222")
            {
                result = new[]
                {
                    new CourtFile
                    {
                        courtClassCode = "E",
                        courtFileNumber = "23222",
                        courtLevelCode = "S",
                        locationId = 9067.0001m,
                        physicalFileId = 3879m,
                        styleOfCause = "DOE, Jane v TESTING, John"
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
                        locationId = 83.0001m,
                        physicalFileId = 2109m,
                        styleOfCause = null
                    },
                    new CourtFile
                    {
                        courtClassCode = "G",
                        courtFileNumber = "111",
                        courtLevelCode = "S",
                        locationId = 83.0001m,
                        physicalFileId = 1063m,
                        styleOfCause = "GILLESPIE, JANET"
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
            int locationID, int hearingTypeID)
        {
            await Task.Delay(100);
            var result = ScAvailableDatesByLocationFixture.AvailableDatesResult;
            result.AvailableDates = result.AvailableDates.Where(
                x => x.Date_Time > DateTime.Now).ToArray();
            return result;
        }

        public async Task<BookingHearingResult> BookingHearingAsync(BookHearingInfo bookInfo)
        {
            await Task.Delay(100);

            return ScBookingHearingResultFixture.Success;
        }

        public async Task<SCJ.OnlineBooking.COACaseList> CoACaseNumberValidAsync(string caseNum)
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

            return CoAAvailableDatesFixture.Dates;
        }

        public async Task<BookingHearingResult> CoAQueueHearingAsync(
            CoABookingHearingInfo bookingInfo)
        {
            await Task.Delay(100);

            return ScBookingHearingResultFixture.Success;
        }
    }
}
