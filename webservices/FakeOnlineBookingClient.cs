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

        public Task<CoAClassInfo> CoACaseNumberValidAsync(string caseNum)
        {
            throw new System.NotImplementedException();
        }

        public Task<CoAAvailableDates> COAAvailableDatesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<BookingHearingResult> CoAQueueHearingAsync(CoABookingHearingInfo bookingInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}
