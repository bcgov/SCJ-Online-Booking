using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels
{
    public class CoaCaseConfirmViewModel
    {
        public string CaseNumber { get; set; }
        public int CaseId { get; set; }
        public string CaseType { get; set; }
        public bool IsValidCaseNumber { get; set; }
        public bool? FactumFiled { get; set; }
        public bool? DateIsAgreed { get; set; }
        public bool? IsFullDay { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
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
        [RegularExpression(
            @"\d{3}[\-]{0,1}\d{3}[\-]{0,1}\d{4}",
            ErrorMessage = "Please provide a valid phone number."
        )]
        public string Phone { get; set; }
        public CoAClassInfo[] CaseList { get; set; }
        public List<string> SelectedCases { get; set; }
        public List<string> RelatedCaseList { get; set; }
        public bool IsAppealHearing { get; set; }
        public bool? IsHalfHour { get; set; }
        public List<string> SelectedApplicationTypes { get; set; }
        public List<string> SelectedApplicationTypeNames { get; set; }
    }
}
