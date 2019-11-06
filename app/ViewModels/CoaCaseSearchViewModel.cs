using System;
using System.ComponentModel.DataAnnotations;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels
{
    public class CoaCaseSearchViewModel
    {
        public CoaCaseSearchViewModel()
        {
            //Default values
            CaseNumber = string.Empty;
        }

        [Required(ErrorMessage = "Please provide a Court File Number.")]
        public string CaseNumber { get; set; }

        public string CaseType { get; set; }
        public bool? CertificateOfReadiness { get; set; }
        public bool? DateIsAgreed { get; set; }
        public bool? LowerCourtOrder { get; set; }
        public bool? IsFullDay { get; set; }
        public string HearingType { get; set; }


        public bool IsValidCaseNumber { get; internal set; }
        public CoAAvailableDates Results { get; internal set; }
        public DateTime? SelectedDate { get; set; }
        public bool TimeSlotExpired { get; internal set; }
        public string TimeSlotFriendlyName { get; internal set; }
        public int HearingTypeId { get; internal set; }
        public string SubmitButton { get; set; }
    }
}
