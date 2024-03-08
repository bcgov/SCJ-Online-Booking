using System;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.ViewModels
{
    public class ScCaseConfirmViewModel
    {
        //Case number
        public string CaseNumber { get; set; }

        //Hearing type
        public string HearingTypeName { get; set; }
        public int HearingTypeId { get; set; }

        //Booking time formula ("fairUseBooking" or "regularBooking")
        public string BookingFormula { get; set; }

        //Location for the booking
        public string CaseLocationName { get; set; }

        //Date for the booking
        public string Date { get; set; }

        //Time for booking
        public string Time { get; set; }

        //indicate if the time-slot was available on the time the user clicked on the confirm button
        public bool IsTimeSlotAvailable { get; set; }

        //indicate if the case was booked successfully
        public bool IsBooked { get; set; }

        //ContainerId for locations
        public int ContainerId { get; set; }

        //Location id
        public int CaseRegistryId { get; set; }

        //Full date for the booking
        public DateTime FullDate { get; set; }

        //User email address
        [Required(ErrorMessage = "Please provide your email address.")]
        [EmailAddress(
            ErrorMessage = "Please provide a valid email address (e.g. example@email.com)."
        )]
        public string EmailAddress { get; set; }

        //Phone number
        [Required(ErrorMessage = "Please provide your phone number.")]
        [RegularExpression(
            @"\d{3}[\-]{0,1}\d{3}[\-]{0,1}\d{4}",
            ErrorMessage = "Please provide a valid phone number (e.g. 250-333-3333)."
        )]
        public string Phone { get; set; }

        public string BookingLocationName { get; set; }
        public int BookingRegistryId { get; set; }

        public int? EstimatedTrialLength { get; set; }

        public int TrialLocation { get; set; }
        public string TrialLocationName { get; set; }
    }
}
