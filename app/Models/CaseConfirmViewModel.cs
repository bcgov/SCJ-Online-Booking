namespace SCJ.Booking.MVC.Models
{
    public class CaseConfirmViewModel
    {
        //Case number
        public string CaseNumber { get; set; }

        //Conference type
        public string TypeOfConferenceHearing { get; set; }

        //Location for the booking
        public string LocationName { get; set; }

        //Date for the booking
        public string Date { get; set; }

        //Time for booking 
        public string Time { get; set; }

        //indicate if the timeslot was available on the time the user clicked on the confirm button
        public bool IsTimeslotAvailable { get; set; }

        //indicate if the case was booked sucessfully
        public bool IsBooked { get; set; }        
    }
}
