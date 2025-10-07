using System;
using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public class ScFormulaLocationsFixture
    {
        private static readonly int BookingDays = 14;

        private static DateTime StartDate
        {
            get
            {
                var futureMonth = DateTime.Now.AddMonths(18);
                return new DateTime(futureMonth.Year, futureMonth.Month, 1);
            }
        }

        private static DateTime BookingStartDate
        {
            get { return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); }
        }

        internal static FormulaLocation[] Locations(int hearingTypeId)
        {
            return
            [
                new()
                {
                    BookingHearingCode = "M",
                    BookingLocationID = 1,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = BookingStartDate
                        .AddDays(BookingDays)
                        .AddSeconds(-1),
                    FairUseBookingPeriodStartDate = BookingStartDate,
                    FairUseContactDate = BookingStartDate.AddDays(BookingDays + 1).AddSeconds(-1),
                    FormulaType = "Fair-Use",
                    LocationCode = "VA",
                    LocationID = 1,
                    LocationName = "Vancouver",
                    StartDate = StartDate,
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "M",
                    BookingLocationID = 1,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "VA",
                    LocationID = 1,
                    LocationName = "Vancouver",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "E",
                    BookingLocationID = 41,
                    EndDate = StartDate.AddMonths(1).AddSeconds(-1),
                    FairUseBookingPeriodEndDate = BookingStartDate
                        .AddDays(BookingDays)
                        .AddSeconds(-1),
                    FairUseBookingPeriodStartDate = BookingStartDate,
                    FairUseContactDate = BookingStartDate.AddDays(BookingDays + 1).AddSeconds(-1),
                    FormulaType = "Fair-Use",
                    LocationCode = "VA",
                    LocationID = 1,
                    LocationName = "Vancouver",
                    StartDate = StartDate,
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "All Other",
                    BookingLocationID = 41,
                    EndDate = StartDate.AddMonths(1).AddSeconds(-1),
                    FairUseBookingPeriodEndDate = BookingStartDate
                        .AddDays(BookingDays)
                        .AddSeconds(-1),
                    FairUseBookingPeriodStartDate = BookingStartDate,
                    FairUseContactDate = BookingStartDate.AddDays(BookingDays + 1).AddSeconds(-1),
                    FormulaType = "Fair-Use",
                    LocationCode = "VA",
                    LocationID = 1,
                    LocationName = "Vancouver",
                    StartDate = StartDate,
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "All Other",
                    BookingLocationID = 41,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "VA",
                    LocationID = 1,
                    LocationName = "Vancouver",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "E",
                    BookingLocationID = 41,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "VA",
                    LocationID = 1,
                    LocationName = "Vancouver",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "All Other",
                    BookingLocationID = 2,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "VI",
                    LocationID = 2,
                    LocationName = "Victoria",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "E",
                    BookingLocationID = 2,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "VI",
                    LocationID = 2,
                    LocationName = "Victoria",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "All",
                    BookingLocationID = 18,
                    EndDate = StartDate.AddMonths(1).AddSeconds(-1),
                    FairUseBookingPeriodEndDate = BookingStartDate
                        .AddDays(BookingDays)
                        .AddSeconds(-1),
                    FairUseBookingPeriodStartDate = BookingStartDate,
                    FairUseContactDate = BookingStartDate.AddDays(BookingDays + 1).AddSeconds(-1),
                    FormulaType = "Fair-Use",
                    LocationCode = "KE",
                    LocationID = 18,
                    LocationName = "Kelowna",
                    StartDate = StartDate,
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "All",
                    BookingLocationID = 18,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "KE",
                    LocationID = 18,
                    LocationName = "Kelowna",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "M",
                    BookingLocationID = 13,
                    EndDate = StartDate.AddMonths(1).AddSeconds(-1),
                    FairUseBookingPeriodEndDate = BookingStartDate
                        .AddDays(BookingDays)
                        .AddSeconds(-1),
                    FairUseBookingPeriodStartDate = BookingStartDate,
                    FairUseContactDate = BookingStartDate.AddDays(BookingDays + 1).AddSeconds(-1),
                    FormulaType = "Fair-Use",
                    LocationCode = "FJ",
                    LocationID = 13,
                    LocationName = "Fort St. John",
                    StartDate = StartDate,
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "E",
                    BookingLocationID = 13,
                    EndDate = StartDate.AddMonths(1).AddSeconds(-1),
                    FairUseBookingPeriodEndDate = BookingStartDate
                        .AddDays(BookingDays)
                        .AddSeconds(-1),
                    FairUseBookingPeriodStartDate = BookingStartDate,
                    FairUseContactDate = BookingStartDate.AddDays(BookingDays + 1).AddSeconds(-1),
                    FormulaType = "Fair-Use",
                    LocationCode = "FJ",
                    LocationID = 13,
                    LocationName = "Fort St. John",
                    StartDate = StartDate,
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "All Other",
                    BookingLocationID = 13,
                    EndDate = StartDate.AddMonths(1).AddSeconds(-1),
                    FairUseBookingPeriodEndDate = BookingStartDate
                        .AddDays(BookingDays)
                        .AddSeconds(-1),
                    FairUseBookingPeriodStartDate = BookingStartDate,
                    FairUseContactDate = BookingStartDate.AddDays(BookingDays + 1).AddSeconds(-1),
                    FormulaType = "Fair-Use",
                    LocationCode = "FJ",
                    LocationID = 13,
                    LocationName = "Fort St. John",
                    StartDate = StartDate,
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "E",
                    BookingLocationID = 13,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "FJ",
                    LocationID = 13,
                    LocationName = "Fort St. John",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "All Other",
                    BookingLocationID = 13,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "FJ",
                    LocationID = 13,
                    LocationName = "Fort St. John",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                },
                new()
                {
                    BookingHearingCode = "M",
                    BookingLocationID = 13,
                    EndDate = StartDate.AddMonths(-4),
                    FairUseBookingPeriodEndDate = DateTime.MinValue,
                    FairUseBookingPeriodStartDate = DateTime.MinValue,
                    FairUseContactDate = DateTime.MinValue,
                    FormulaType = "Regular",
                    LocationCode = "FJ",
                    LocationID = 13,
                    LocationName = "Fort St. John",
                    StartDate = DateTime.Now.Date.AddDays(60),
                    HearingTypeId = hearingTypeId
                }
            ];
        }
    }
}
