using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public static class BookingHearingResultFixture
    {
        public static BookingHearingResult SupremeCourtFailure = new BookingHearingResult
        {
            // Failure messages always start with "Fail"

            // this message is returned when the date is too close in the future.
            // MicroServe has agreed to change the messages to something more user friendly before launch,
            // based on feedback from OpenRoad QA/UX
            bookingResult =
                "Fail - The date for filing the Plaintiff's Civil Trial Brief has passed for booking this date.\n"
        };


        public static BookingHearingResult Success = new BookingHearingResult
        {
            // Success messages always start with "Success"

            bookingResult = "Success - Hearing Booked"
        };

        public static BookingHearingResult CoAFailure = new BookingHearingResult
        {
            bookingResult =
                "Fail - Booking could not be completed. Please contact scheduling or select a different date/time."
        };
    }
}
