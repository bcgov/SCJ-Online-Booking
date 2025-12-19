using System;

namespace SCJ.Booking.UnitTest.Helpers
{
    public class CsvMapping
    {
        public int BookingLocationId { get; set; }
        public string BookHearingCode { get; set; }
        public int HearingTypeId { get; set; }
        public string CaseNumber { get; set; }
        public string CaseRegistryCode { get; set; }
        public int CaseRegistryId { get; set; }
        public decimal CeisPhysicalFileId { get; set; }
        public string CourtClassCode { get; set; }
        public string StyleOfCause { get; set; }
        public int HearingLength { get; set; }
        public int FairUseSort { get; set; }
        public DateTime FairUseBookingPeriodStartDate { get; set; }
        public DateTime FairUseBookingPeriodEndDate { get; set; }
        public DateTime LotteryStartDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RequestedByName { get; set; }
        public string LotteryEntryId { get; set; }
        public int HearingLocationId { get; set; }
        public string HearingLocationName { get; set; }
        public int? LongChambersHearingSubTypeId { get; set; }
        public string LongChambersHearingSubTypeName { get; set; }
        public DateTime? Selection1 { get; set; }
        public DateTime? Selection2 { get; set; }
        public DateTime? Selection3 { get; set; }
        public DateTime? Selection4 { get; set; }
        public DateTime? Selection5 { get; set; }
    }
}
