using SCJ.Booking.CourtBookingPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public static class BookingPeriodsFixture
    {
        public static BookingPeriod[] BookingPeriods =
        {
            new BookingPeriod
            {
                Id = 1,
                RegistryId = 1,
                OpeningDate = new DateTime(2024, 8, 1),
                ClosingDate = new DateTime(2024, 8, 31)
            },
            new BookingPeriod
            {
                Id = 2,
                RegistryId = 1,
                OpeningDate = new DateTime(2024, 9, 1),
                ClosingDate = new DateTime(2024, 9, 30)
            }
        };
    }
}
