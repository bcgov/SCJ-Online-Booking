using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels
{
    public class CaseSearchViewModel
    {
        public CaseSearchViewModel()
        {
            //Default values
            Results = new AvailableDatesByLocation();
            IsValidCaseNumber = false;
            TimeSlotExpired = false;
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

        //Indicates if the case number is valid or not
        public bool IsValidCaseNumber { get; set; }

        //Indicates if the time slot expired
        public bool TimeSlotExpired { get; set; }

        // Selected Registry ID
        [Required(ErrorMessage = "Please select the registry where the file was created.")]
        public int SelectedRegistryId { get; set; }

        // Selected Registry Name
        public string SelectedRegistryName { get; set; }

        public SelectList Registry { get; set; }

        //Used when the time slot expired eg. January 7 from 2:45pm to 3:15pm
        public string TimeSlotFriendlyName { get; set; }

        //Date properties
        public int ContainerId { get; set; }

        //Date selected in the swiper
        public string SelectedCaseDate { get; set; }

        //Indicator if user can book a date
        public bool IsUserAllowedToBook { get; set; }
    }
}
