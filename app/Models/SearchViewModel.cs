using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Please provide a Case Action Number.")]
        public string CaseNumber { get; set; }

        [Required(ErrorMessage = "Please choose the type of conference hearing.")]
        public Utils.Enums.ConferenceHearingType ConferenceType { get; set; }
    }
}
