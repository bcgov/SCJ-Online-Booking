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
            },
            new BookingPeriod
            {
                Id = 3,
                RegistryId = 1,
                OpeningDate = new DateTime(2024, 10, 1),
                ClosingDate = new DateTime(2024, 10, 31)
            },
            new BookingPeriod
            {
                Id = 4,
                RegistryId = 1,
                OpeningDate = new DateTime(2024, 11, 1),
                ClosingDate = new DateTime(2024, 11, 30)
            },
            new BookingPeriod
            {
                Id = 5,
                RegistryId = 1,
                OpeningDate = new DateTime(2024, 12, 1),
                ClosingDate = new DateTime(2024, 12, 31)
            },
            new BookingPeriod
            {
                Id = 6,
                RegistryId = 1,
                OpeningDate = new DateTime(2025, 1, 1),
                ClosingDate = new DateTime(2025, 1, 31)
            },
        };
    }
}
