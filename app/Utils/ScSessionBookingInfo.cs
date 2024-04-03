using System;
using System.Collections.Generic;
using SCJ.OnlineBooking;

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
        public int HearingBookingRegistryId { get; set; }
        public string BookingLocationName { get; set; }

        public int ContainerId { get; set; }
        public int HearingTypeId { get; set; }
        public string BookingFormula { get; set; }
        public string HearingTypeName { get; set; }
        public int? EstimatedTrialLength { get; set; }
        public bool? IsHomeRegistry { get; set; }
        public bool? IsLocationChangeFiled { get; set; }
        public int TrialLocation { get; set; }
        public string SelectedCaseDate { get; set; }

        public DateTime? SelectedRegularTrialDate { get; set; }
        public List<DateTime> SelectedFairUseTrialDates { get; set; } = new List<DateTime>() { };

        //The result string returned by the SOAP API when the hearing was booked
        public string RawResult { get; set; }

        public bool IsBooked { get; set; }

        public CourtFile[] CourtFiles { get; set; }
        public string SelectedCourtClass { get; set; }
        public CourtFile SelectedCourtFile { get; set; }
        public string SelectedCourtClassName { get; set; }
        public AvailableDatesByLocation Results { get; set; }
        public List<int> AvailableConferenceTypeIds { get; set; }
        public List<string> AvailableBookingTypes { get; set; }

        public int HearingLengthMinutes
        {
            get { return Results?.BookingDetails?.detailBookingLength ?? 0; }
        }

        //Full date for the booking
        public DateTime FullDate { get; set; }

        public string TimeSlotFriendlyName
        {
            get
            {
                return $"{FullDate:h:mm tt}–{FullDate.AddMinutes(HearingLengthMinutes):h:mm tt}".ToLower();
            }
        }
        public string DateFriendlyName
        {
            get { return FullDate.ToString("dddd, MMMM dd, yyyy"); }
        }

        public string FriendlyError
        {
            get
            {
                if (this.RawResult == null)
                {
                    return string.Empty;
                }
                return this.RawResult.Replace("Fail - ", "");
            }
        }
    }
}
