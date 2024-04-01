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
                EndDate = DateTime.Parse("2025-12-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-04-07T23:59:59.0000000Z"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-04-01T00:00:00.0000000Z"),
                FairUseContactDate = DateTime.Parse("2024-04-03T23:59:59.0000000Z"),
                FormulaType = "Fair-Use",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2025-12-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "M",
                BookingLocationID = 1,
                EndDate = DateTime.Parse("2026-05-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2024-12-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 41,
                EndDate = DateTime.Parse("2025-12-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-04-17T23:59:59.0000000Z"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-04-01T00:00:00.0000000Z"),
                FairUseContactDate = DateTime.Parse("2024-04-03T23:59:59.0000000Z"),
                FormulaType = "Fair-Use",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2025-12-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 41,
                EndDate = DateTime.Parse("2025-12-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-04-17T23:59:59.0000000Z"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-04-01T00:00:00.0000000Z"),
                FairUseContactDate = DateTime.Parse("2024-04-03T23:59:59.0000000Z"),
                FormulaType = "Fair-Use",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2025-12-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 41,
                EndDate = DateTime.Parse("2026-05-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2024-12-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 41,
                EndDate = DateTime.Parse("2026-05-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VA",
                LocationID = 1,
                LocationName = "Vancouver",
                StartDate = DateTime.Parse("2024-12-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 2,
                EndDate = DateTime.Parse("2025-09-30T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VI",
                LocationID = 2,
                LocationName = "Victoria",
                StartDate = DateTime.Parse("2024-09-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 2,
                EndDate = DateTime.Parse("2025-09-30T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "VI",
                LocationID = 2,
                LocationName = "Victoria",
                StartDate = DateTime.Parse("2024-09-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "All",
                BookingLocationID = 9,
                EndDate = DateTime.Parse("2024-11-30T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-04-07T23:59:59.0000000Z"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-04-01T00:00:00.0000000Z"),
                FairUseContactDate = DateTime.Parse("2024-04-01T23:59:59.0000000Z"),
                FormulaType = "Fair-Use",
                LocationCode = "CT",
                LocationID = 9,
                LocationName = "Courtenay",
                StartDate = DateTime.Parse("2024-11-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "All",
                BookingLocationID = 9,
                EndDate = DateTime.Parse("2025-04-30T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "CT",
                LocationID = 9,
                LocationName = "Courtenay",
                StartDate = DateTime.Parse("2024-08-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "M",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2024-07-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-04-17T23:59:59.0000000Z"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-04-12T00:00:00.0000000Z"),
                FairUseContactDate = DateTime.Parse("2024-04-20T23:59:59.0000000Z"),
                FormulaType = "Fair-Use",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2024-07-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-04-17T23:59:59.0000000Z"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-04-12T00:00:00.0000000Z"),
                FairUseContactDate = DateTime.Parse("2024-04-20T23:59:59.0000000Z"),
                FormulaType = "Fair-Use",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2024-07-31T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.Parse("2024-04-10T23:59:59.0000000Z"),
                FairUseBookingPeriodStartDate = DateTime.Parse("2024-04-01T00:00:00.0000000Z"),
                FairUseContactDate = DateTime.Parse("2024-04-20T23:59:59.0000000Z"),
                FormulaType = "Fair-Use",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "E",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2025-04-30T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "All Other",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2025-04-30T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01T00:00:00.0000000Z")
            },
            new()
            {
                BookingHearingCode = "M",
                BookingLocationID = 13,
                EndDate = DateTime.Parse("2025-04-30T23:59:59.0000000Z"),
                FairUseBookingPeriodEndDate = DateTime.MinValue,
                FairUseBookingPeriodStartDate = DateTime.MinValue,
                FairUseContactDate = DateTime.MinValue,
                FormulaType = "Regular",
                LocationCode = "FJ",
                LocationID = 13,
                LocationName = "Fort St. John",
                StartDate = DateTime.Parse("2024-08-01T00:00:00.0000000Z")
            }
        };
    }
}
