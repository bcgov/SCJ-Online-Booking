using System;
using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public class ScFormulaLocationsFixture
    {
        internal static FormulaLocation[] Locations =
        {
            new()
            {
                BookingHearingCode = "M",
                BookingLocationID = 1,
                EndDate = DateTime.Parse("2025-12-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-05-07 23:59:59.0000000"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-05-01 00:00:00.0000000"),
                FairUseContactDate = DateTime.Parse("2024-05-03 23:59:59.0000000"),
                FormulaType = "Fair-Use",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2025-12-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "M",
                BookingLocationID = 1,
                EndDate = DateTime.Parse("2026-05-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2024-12-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 41,
                EndDate = DateTime.Parse("2025-12-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-05-17 23:59:59.0000000"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-05-01 00:00:00.0000000"),
                FairUseContactDate = DateTime.Parse("2024-05-03 23:59:59.0000000"),
                FormulaType = "Fair-Use",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2025-12-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 41,
                EndDate = DateTime.Parse("2025-12-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-05-17 23:59:59.0000000"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-05-01 00:00:00.0000000"),
                FairUseContactDate = DateTime.Parse("2024-05-03 23:59:59.0000000"),
                FormulaType = "Fair-Use",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2025-12-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 41,
                EndDate = DateTime.Parse("2026-05-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2024-12-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 41,
                EndDate = DateTime.Parse("2026-05-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2024-12-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 2,
                EndDate = DateTime.Parse("2025-09-30 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VI",
                LocationID = 2,
                LocationName = "Victoria",
                StartDate = DateTime.Parse("2024-09-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 2,
                EndDate = DateTime.Parse("2025-09-30 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VI",
                LocationID = 2,
                LocationName = "Victoria",
                StartDate = DateTime.Parse("2024-09-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "All",
                BookingLocationID = 9,
                EndDate = DateTime.Parse("2024-11-30 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-05-07 23:59:59.0000000"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-05-01 00:00:00.0000000"),
                FairUseContactDate = DateTime.Parse("2024-05-01 23:59:59.0000000"),
                FormulaType = "Fair-Use",
                LocationCode = "CT",
                LocationID = 9,
                LocationName = "Courtenay",
                StartDate = DateTime.Parse("2024-11-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "All",
                BookingLocationID = 9,
                EndDate = DateTime.Parse("2025-04-30 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "CT",
                LocationID = 9,
                LocationName = "Courtenay",
                StartDate = DateTime.Parse("2024-08-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "M",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2024-07-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-05-17 23:59:59.0000000"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-05-12 00:00:00.0000000"),
                FairUseContactDate = DateTime.Parse("2024-05-20 23:59:59.0000000"),
                FormulaType = "Fair-Use",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2024-07-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-05-17 23:59:59.0000000"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-05-12 00:00:00.0000000"),
                FairUseContactDate = DateTime.Parse("2024-05-20 23:59:59.0000000"),
                FormulaType = "Fair-Use",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2024-07-31 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-05-10 23:59:59.0000000"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-05-01 00:00:00.0000000"),
                FairUseContactDate = DateTime.Parse("2024-05-20 23:59:59.0000000"),
                FormulaType = "Fair-Use",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2025-04-30 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2025-04-30 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01 00:00:00.0000000")
            },
            new()
            {
                BookingHearingCode = "M",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2025-04-30 23:59:59.0000000"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01 00:00:00.0000000")
            }
        };
    }
}
