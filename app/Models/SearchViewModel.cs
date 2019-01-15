using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SCJ.Booking.MVC.Models
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Please provide a Case Action Number.")]
        public string CaseNumber { get; set; }

        [Required(ErrorMessage = "Please choose the type of conference hearing.")]
        public Utils.Enums.ConferenceHearingType ConferenceType { get; set; }

        public string SelectedRegistryId { get; set; }

        [Required(ErrorMessage = "Please select the registry where the file was created.")]
        public SelectList Registry { get; set; }
    }
}
