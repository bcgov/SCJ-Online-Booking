namespace SCJ.Booking.MVC.ViewModels
{
    public class ScHearingEmailViewModel
    {
        public string EmailAddress { get; set; }
        public string TypeOfConference { get; set; }
        public string CaseLocationName { get; set; }
        public string BookingLocationName { get; set; }
        public string CourtFileNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Phone { get; set; }
        public string StyleOfCause { get; internal set; }
    }
}
