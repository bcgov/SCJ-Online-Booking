using System.Collections.Generic;

namespace SCJ.Booking.MVC.ViewModels
{
    public class EmailViewModel
    {
        public string EmailAddress { get; set; }
        public string TypeOfConference { get; set; }
        public string RegistryName { get; set; }
        public string CourtFileNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Phone { get; set; }

        //For Court of Appeal
        public string HearingLength { get; set; }
        public string CaseType { get; internal set; }
        public List<string> RelatedCaseList { get; set; }
    }
}
