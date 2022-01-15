using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Utils
{
    public class ScSessionBookingInfo
    {
        public int CaseId { get; set; }
        public string CaseNumber { get; set; }
        public string FullCaseNumber { get; set; }
        public int CaseRegistryId { get; set; }
        public string CaseLocationName { get; set; }
        public int BookingRegistryId { get; set; }
        public string BookingLocationName { get; set; }
        public int HearingLengthMinutes { get; set; }
        public int ContainerId { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public string TimeSlotFriendlyName { get; set; }
        public string SelectedCaseDate { get; set; }

        //The result string returned by the SOAP API when the hearing was booked
        public string RawResult { get; set; }

        public bool IsBooked { get; set; }

        public string DateFriendlyName { get; set; }

        public CourtFile[] CourtFiles { get; set; }
        public string SelectedCourtClass { get; set; }
    }
}
