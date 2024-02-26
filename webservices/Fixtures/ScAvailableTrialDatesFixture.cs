using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public class ScAvailableTrialDatesFixture
    {
        public static AvailableTrialDatesResult Dates = new AvailableTrialDatesResult
        {
            AvailableTrialDates = new AvailableTrialDates
            {
                AvailablesDatesInfo = new AvailableTrialDatesInfo[]
                {
                    new AvailableTrialDatesInfo
                    {
                        AvailableDate = DateTime.Parse("2025-08-11T00:00:00.0000000"),
                        AvailableQuantity = 4
                    },
                    new AvailableTrialDatesInfo
                    {
                        AvailableDate = DateTime.Parse("2025-08-18T00:00:00.0000000"),
                        AvailableQuantity = 4
                    },
                    new AvailableTrialDatesInfo
                    {
                        AvailableDate = DateTime.Parse("2025-08-25T00:00:00.0000000"),
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
