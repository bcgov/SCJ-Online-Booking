using SCJ.Booking.CourtBookingPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public static class RegistrySettingsFixture
    {
        public static bool DefaultUsesLottery = true;
        public static int DefaultNumberOfPicksPerUser = 5;

        public static RegistrySetting[] RegistrySettings =
        {
            new RegistrySetting
            {
                Id = 1,
                RegistryId = "VA",
                UsesLottery = DefaultUsesLottery,
                MaximumDateSelections = DefaultNumberOfPicksPerUser,
                MonthlyBookingWeek = 1,
                MonthlyBookingDay = 1,
                BookingStartTime = new DateTime(2023, 2, 2, 9, 0, 0),
                BookingEndTime = new DateTime(2023, 2, 2, 12, 0, 0)
            },
            new RegistrySetting
            {
                Id = 2,
                RegistryId = "VA",
                UsesLottery = DefaultUsesLottery,
                MaximumDateSelections = DefaultNumberOfPicksPerUser,
                MonthlyBookingWeek = 1,
                MonthlyBookingDay = 1,
                BookingStartTime = new DateTime(2023, 3, 2, 9, 0, 0),
                BookingEndTime = new DateTime(2023, 3, 2, 12, 0, 0)
            }
        };
    }
}
