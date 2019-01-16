using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.SC.OnlineBooking;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class CaseConfirmViewModel
    {
        public string CaseNumber { get; set; }

        public string TypeOfConferenceHearing { get; set; }

        public string LocationName { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }
    }
}
