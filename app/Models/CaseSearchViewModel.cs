using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.SC.OnlineBooking;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class CaseSearchViewModel
    {
        public CaseSearchViewModel()
        {
            //Default values
            Results = new AvailableDatesByLocation();
            NoAvailableTimes = false;
            IsValidCaseNumber = false;
            TimeslotExpired = false;
            SelectedRegistryName = string.Empty;
            SelectedRegistryId = -1;
            ContainerId = -1;
            SelectedCaseDate = string.Empty;
            CaseNumber = string.Empty;
            ConferenceType = Utils.Enums.ConferenceHearingType.TRIAL_MANAGEMENT_CONFERENCE;
        }

        //Search fields
        [Required(ErrorMessage = "Please provide a Case Action Number.")]
        public string CaseNumber { get; set; }

        [Required(ErrorMessage = "Please choose the type of conference hearing.")]
        public Utils.Enums.ConferenceHearingType ConferenceType { get; set; }

        //Available dates
        public AvailableDatesByLocation Results { get; set; }

        //Indicator to show if there are results but no times available
        public bool NoAvailableTimes { get; set; }

        //Indicates if the case number is valid or not
        public bool IsValidCaseNumber { get; set; }

        //Indicates if the timeslot expired
        public bool TimeslotExpired { get; set; }

        //Location ID and Name
        public int SelectedRegistryId { get; set; }

        public string SelectedRegistryName { get; set; }

        [Required(ErrorMessage = "Please select the registry where the file was created.")]
        public SelectList Registry { get; set; }

        //Used when the timeslot expired eg. January 7 from 2:45pm to 3:15pm
        public string TimeslotFriendlyName { get; set; }

        //Date properties
        public int ContainerId { get; set; }

        //Date selected in the swiper
        public string SelectedCaseDate { get; set; }
    }
}
