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
        public async Task<int> caseNumberValidAsync(string caseNum)
        {
            await Task.Delay(100);

            if (caseNum == "VAM147619")
            {
                return 234076; // returns the CaseID
            }

            return 0;
        }

        public async Task<Location[]> getLocationsAsync()
        {
            await Task.Delay(100);

            return ScLocationFixture.All;
        }

        public async Task<AvailableDatesByLocation> AvailableDatesByLocationAsync(int locationID,
            int hearingTypeID)
        {
            await Task.Delay(100);

            return ScAvailableDatesByLocationFixture.AvailableDatesResult;
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
