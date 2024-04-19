using System;
using System.Collections.Generic;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Utils
{
    public class CoaSessionBookingInfo
    {
        public string CaseNumber { get; set; }
        public int CaseId { get; set; }
        public string CaseType { get; set; }
        public bool? FactumFiled { get; set; }
        public bool? DateIsAgreed { get; set; }
        public bool? IsFullDay { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string RegistryName { get; set; }

        //The result string returned by the SOAP API when the hearing was booked
        public string RawResult { get; set; }
        public bool IsBooked { get; set; }
        public CoAClassInfo[] CaseList { get; set; }
        public List<string> SelectedCases { get; set; }
        public List<string> RelatedCaseList { get; set; }
        public bool? IsAppealHearing { get; set; }
        public List<string> SelectedApplicationTypes { get; set; }
        public bool? IsHalfHour { get; set; }

        public string FriendlyError
        {
            get
            {
                if (RawResult == null)
                {
                    return string.Empty;
                }
                return RawResult.Replace("Fail - ", "");
            }
        }
    }
}
