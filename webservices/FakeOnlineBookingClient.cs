using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace SCJ.SC.OnlineBooking
{
    /// <summary>
    ///     Test implementation of OnlineBookingClient
    /// </summary>
    public class FakeOnlineBookingClient : IOnlineBooking
    {
        public Task<int> caseNumberValidAsync(string caseNum)
        {
            throw new NotImplementedException();
        }

        public Task<Location[]> getLocationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AvailableDatesByLocation> AvailableDatesByLocationAsync(int locationID,
            int hearingTypeID)
        {
            throw new NotImplementedException();
        }

        public Task<BookingHearingResult> BookingHearingAsync(BookHearingInfo bookInfo)
        {
            throw new NotImplementedException();
        }
    }
}
