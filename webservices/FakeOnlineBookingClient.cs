using System.Threading.Tasks;
using SCJ.Booking.RemoteAPIs.Fixtures;

// ReSharper disable once CheckNamespace
namespace SCJ.SC.OnlineBooking
{
    /// <summary>
    ///     Test implementation of OnlineBookingClient
    /// </summary>
    public class FakeOnlineBookingClient : IOnlineBooking
    {
        public async Task<int> caseNumberValidAsync(string caseNum)
        {
            await Task.Delay(100);

            if (caseNum == "VA147619")
            {
                return 234076; // returns the CaseID
            }

            return 0;
        }

        public async Task<Location[]> getLocationsAsync()
        {
            await Task.Delay(100);

            return Locations.All;
        }

        public async Task<AvailableDatesByLocation> AvailableDatesByLocationAsync(int locationID,
            int hearingTypeID)
        {
            await Task.Delay(100);

            return AvailableDates.VancouverTmc;
        }

        public async Task<BookingHearingResult> BookingHearingAsync(BookHearingInfo bookInfo)
        {
            await Task.Delay(100);

            return BookingResults.Success;
        }
    }
}
