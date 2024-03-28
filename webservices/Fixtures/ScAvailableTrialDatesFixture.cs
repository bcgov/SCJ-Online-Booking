using System;
using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public class ScAvailableTrialDatesFixture
    {
        internal static AvailableTrialDatesResult Dates =
            new()
            {
                AvailableTrialDates = new AvailableTrialDates
                {
                    AvailablesDatesInfo = new[]
                    {
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = DateTime.Parse("2025-12-06T00:00:00.0000000"),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = DateTime.Parse("2025-12-08T00:00:00.0000000"),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = DateTime.Parse("2025-12-10T00:00:00.0000000"),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = DateTime.Parse("2025-12-11T00:00:00.0000000"),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = DateTime.Parse("2025-12-18T00:00:00.0000000"),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = DateTime.Parse("2025-12-25T00:00:00.0000000"),
                            AvailableQuantity = 4
                        }
                    },
                    BookingHearingCode = "E",
                    BookingLocationID = 41,
                    FormulaType = "Fair-Use",
                    LocationCode = "VA",
                    LocationID = 1,
                    LocationName = "Vancouver"
                }
            };
    }
}
