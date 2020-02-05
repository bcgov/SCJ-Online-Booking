using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.ViewModels
{
    public class CoaCaseConfirmViewModel
    {
        public string CaseNumber { get; set; }
        public List<string> SelectedCases { get; set; }
        public int CaseId { get; set; }
        public string CaseType { get; set; }
        public bool IsValidCaseNumber { get; set; }
        public bool IsMainCase { get; set; }
        public List<string> RelatedCases { get; set; }
        public bool? CertificateOfReadiness { get; set; }
        public bool? DateIsAgreed { get; set; }
        //public bool? LowerCourtOrder { get; set; }
        public bool? IsFullDay { get; set; }
        //public SelectList HearingTypes { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        //public Dictionary<DateTime, List<DateTime>> Results { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string SubmitButton { get; set; }
        public bool TimeSlotExpired { get; set; }

        //indicate if the time-slot was available on the time the user clicked on the confirm button
        public bool IsTimeSlotAvailable { get; set; }

        //indicate if the case was booked successfully
        public bool IsBooked { get; set; }

        //User email address
        [Required(ErrorMessage = "Please provide a valid email address.")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        public string EmailAddress { get; set; }

        //Phone number
        [Required(ErrorMessage = "Please provide a valid phone number.")]
        [RegularExpression(@"\d{3}[\-]{0,1}\d{3}[\-]{0,1}\d{4}", ErrorMessage = "Please provide a valid phone number.")]
        public string Phone { get; set; }
    }
}
