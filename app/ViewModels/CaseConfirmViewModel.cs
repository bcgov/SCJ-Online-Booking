using System;

namespace SCJ.Booking.MVC.ViewModels
{
    public class CaseConfirmViewModel
    {
        //Case number
        public string CaseNumber { get; set; }

        //Conference type
        public string HearingTypeName { get; set; }

        //Location for the booking
        public string LocationName { get; set; }

        //Date for the booking
        public string Date { get; set; }

        //Time for booking 
        public string Time { get; set; }

        //indicate if the time-slot was available on the time the user clicked on the confirm button
        public bool IsTimeSlotAvailable { get; set; }

        //indicate if the case was booked successfully
        public bool IsBooked { get; set; }

        //The result string returned by the SOAP API when the hearing was booked
        public string RawResult { get; set; }

        //ContainerId for locations
        public int ContainerId { get; set; }

        //Location id
        public int LocationId { get; set; }

        //Full date for the booking
        public DateTime FullDate { get; set; }

        //User email address
        public string EmailAddress { get; set; }

        //Is user known?
        public bool IsUserKnown { get; set; }

        //Phone number
        public string Phone { get; set; }
    }
}
