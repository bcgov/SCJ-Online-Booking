using System;

namespace SCJ.Booking.MVC.Utils
{
    public class CoaSessionBookingInfo
    {
        public int CaseId { get; set; }
        public string CaseNumber { get; set; }
        public string FullCaseNumber { get; set; }
        public int LocationId { get; set; }
        public string RegistryName { get; set; }
        public int HearingLengthMinutes { get; set; }
        public int ContainerId { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public string TimeSlotFriendlyName { get; set; }
        public string SelectedCaseDate { get; set; }
        public DateTime SelectedDate { get; set; }

        //The result string returned by the SOAP API when the hearing was booked
        public string RawResult { get; set; }

        public bool IsBooked { get; set; }

        public string DateFriendlyName { get; set; }

        public string CaseType { get; internal set; }
        public bool? CertificateOfReadiness { get; set; }
        public bool? DateIsAgreed { get; set; }
        public bool? LowerCourtOrder { get; set; }
        public bool? IsFullDay { get; set; }
        public string HearingType { get; set; }
    }
}
