using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public static class ScBookingHearingResultFixture
    {
        internal static BookingHearingResult SupremeCourtFailure =
            new()
            {
                // Failure messages always start with "Fail"

                // this message is returned when the date is too close in the future.
                // MicroServe has agreed to change the messages to something more user friendly before launch,
                // based on feedback from OpenRoad QA/UX
                bookingResult = "Fail - Hearing booking was randomly failed by the fake API"
            };

        internal static BookingHearingResult Success =
            new()
            {
                // Success messages always start with "Success"

                bookingResult = "Success - Hearing Booked"
            };

        internal static BookingHearingResult CoAFailure =
            new()
            {
                bookingResult =
                    "Fail - Booking could not be completed. Please contact scheduling or select a different date/time."
            };
    }
}
