using System;
using System.ServiceModel;
using Microsoft.AspNetCore.SignalR;
using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public class ScAvailableTrialDatesFixture
    {
        private static DateTime StartDate
        {
            get
            {
                var futureMonth = DateTime.Now.AddMonths(18);
                var startDate = new DateTime(futureMonth.Year, futureMonth.Month, 1);
                while (startDate.DayOfWeek != DayOfWeek.Monday)
                {
                    startDate = startDate.AddDays(1);
                }
                return startDate;
            }
        }

        internal static AvailableTrialDatesResult Dates =
            new()
            {
                AvailableTrialDates = new AvailableTrialDates
                {
                    AvailablesDatesInfo = new[]
                    {
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = StartDate.AddDays(0),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = StartDate.AddDays(1),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = StartDate.AddDays(4),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = StartDate.AddDays(7),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = StartDate.AddDays(8),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = StartDate.AddDays(14),
                            AvailableQuantity = 4
                        },
                        new AvailableTrialDatesInfo
                        {
                            AvailableDate = StartDate.AddDays(21),
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
