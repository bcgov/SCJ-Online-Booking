using SCJ.OnlineBooking;
using System;

namespace SCJ.Booking.MVC.Utils
{
    public class ScSessionBookingInfo
    {
        public int CaseId { get; set; }
        public string CaseNumber { get; set; }
        public string FileNumber { get; set; }
        public string FullCaseNumber { get; set; }
        public string LocationPrefix { get; set; }

        public int CaseRegistryId { get; set; }
        public string CaseLocationName { get; set; }
        public int BookingRegistryId { get; set; }
        public string BookingLocationName { get; set; }

        public int ContainerId { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public string SelectedCaseDate { get; set; }

        //The result string returned by the SOAP API when the hearing was booked
        public string RawResult { get; set; }

        public bool IsBooked { get; set; }

        public CourtFile[] CourtFiles { get; set; }
        public string SelectedCourtClass { get; set; }
        public CourtFile SelectedCourtFile { get; set; }
        public string SelectedCourtClassName { get; set; }
        public AvailableDatesByLocation Results { get; set; }
        public int HearingLengthMinutes
        {
            get
            {
                return Results?.BookingDetails?.detailBookingLength ?? 0;
            }
        }

        //Full date for the booking
        public DateTime FullDate { get; set; }

        public string TimeSlotFriendlyName
        {
            get
            {
                return $"{FullDate:hh:mm tt} - {FullDate.AddMinutes(HearingLengthMinutes):hh:mm tt}";
            }
        }
        public string DateFriendlyName
        {
            get
            {
                return FullDate.ToString("dddd, MMMM dd, yyyy");
            }
        }

    }
}
