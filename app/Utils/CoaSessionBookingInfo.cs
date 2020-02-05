using SCJ.OnlineBooking;
using System;
using System.Collections.Generic;

namespace SCJ.Booking.MVC.Utils
{
    public class CoaSessionBookingInfo
    {
        public string CaseNumber { get; set; }
        public int CaseId { get; set; }
        public string CaseType { get; set; }
        public bool? CertificateOfReadiness { get; set; }
        public bool? DateIsAgreed { get; set; }
        //public bool? LowerCourtOrder { get; set; }
        public bool? IsFullDay { get; set; }
        //public SelectList HearingTypes { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public DateTime? SelectedDate { get; set; }

        public string FullCaseNumber { get; set; }
        public int LocationId { get; set; }
        public string RegistryName { get; set; }
        public string HearingLength { get; set; }
        public int ContainerId { get; set; }
        //The result string returned by the SOAP API when the hearing was booked
        public string RawResult { get; set; }
        public bool IsBooked { get; set; }
        public bool IsMainCase { get; set; }
        public string RelatedCases { get; set; }
        public string DateFriendlyName { get; set; }
        public string TimeSlotFriendlyName { get; set; }
        public CoAClassInfo[] CaseList { get; set; }
        public List<string> SelectedCases { get; set; }

    }
}
