using System.Collections.Generic;

namespace SCJ.Booking.MVC.ViewModels.COA
{
    public class CoaEmailViewModel
    {
        public string EmailAddress { get; set; }
        public string TypeOfConference { get; set; }
        public string CourtFileNumber { get; set; }
        public string Date { get; set; }
        public string Phone { get; set; }
        public string HearingLength { get; set; }
        public string CaseType { get; internal set; }
        public List<string> RelatedCaseList { get; set; }
        public string RelatedCasesString { get; set; }
        public List<string> SelectedApplicationTypeNames { get; set; }
        public string HearingTypeName { get; set; }
        public bool DateIsAgreed { get; set; }
    }
}
