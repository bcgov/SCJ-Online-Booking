using System.Collections.Generic;

namespace SCJ.Booking.MVC.ViewModels
{
    public class TrialEmailViewModel
    {
        public string EmailAddress { get; set; }
        public string Phone { get; set; }

        public string LocationPrefix { get; set; }
        public string CourtFileNumber { get; set; }
        public string StyleOfCause { get; internal set; }
        public string CourtClassName { get; internal set; }
        public string TrialLength { get; set; }
        public string CaseLocationName { get; set; }
        public string BookingLocationName { get; set; }
        public string TrialLocationName { get; set; }
        public string ResultDate { get; set; } // @TODO: ??

        // selected single date for regular trial bookings
        public string RegularDate { get; set; }

        // multiple requested dates for "fair use" lottery
        public List<string> FairUseDates { get; set; }
    }
}
