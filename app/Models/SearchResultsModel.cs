using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.SC.OnlineBooking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class SearchResultsViewModel
    {
        public SearchResultsViewModel()
        {
            Results = new List<AvailableDatesByLocation>();
        }

        //Search fields
        [Required(ErrorMessage = "Please provide a Case Action Number.")]
        public string CaseNumber { get; set; }

        [Required(ErrorMessage = "Please choose the type of conference hearing.")]
        public Utils.Enums.ConferenceHearingType ConferenceType { get; set; }

        //Available dates
        public List<AvailableDatesByLocation> Results { get; set; }

        //Indicator to show if there are results but no times available
        public bool NoAvailableTimes { get; set; }

        //Indicates if the case number is valid or not
        public bool IsValidCaseNumber { get; set; }

        public string SelectedRegistryId { get; set; }

        [Required(ErrorMessage = "Please select the registry where the file was created.")]
        public SelectList Registry { get; set; }
    }
}
