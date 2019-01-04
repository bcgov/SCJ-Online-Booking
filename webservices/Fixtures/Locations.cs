using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public static class Locations
    {
        // this is the partial list of locations copied from the dev environment.
        // staging and production will have more locations.
        public static Location[] All =
        {
            new Location
            {
                locationCode = "KA",
                locationID = 17,
                locationName = "Kamloops"
            },
            new Location
            {
                locationCode = "VA",
                locationID = 1,
                locationName = "Vancouver"
            },
            new Location
            {
                locationCode = "VI",
                locationID = 2,
                locationName = "Victoria"
            }
        };
    }
}
