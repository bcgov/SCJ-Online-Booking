using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public static class CoAClassInfoFixture
    {
        public static CoAClassInfo CivilCase = new CoAClassInfo
        {
            CaseId = 37351,
            CaseType = "Civil"
        };

        public static CoAClassInfo CriminalCase = new CoAClassInfo
        {
            CaseId = 40368,
            CaseType = "Criminal"
        };

        public static CoAClassInfo NotFound = new CoAClassInfo
        {
            CaseId = 0,
            CaseType = "Not Found"
        };
    }
}
