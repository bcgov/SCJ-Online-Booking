using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs
{
    public static class OnlineBookingClientFactory
    {
        /// <summary>
        ///     Factory method for creating online booking clients
        /// </summary>
        public static IOnlineBooking GetClient(bool fake)
        {
            return fake
                ? (IOnlineBooking) new FakeOnlineBookingClient()
                : new OnlineBookingClient();
        }
    }
}
