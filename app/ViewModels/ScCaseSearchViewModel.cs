using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels
{
    public class ScCaseSearchViewModel
    {
        public ScCaseSearchViewModel()
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
            HearingTypeId = -1;
            HearingTypeName = string.Empty;
        }

        //Search fields
        public string CaseNumber { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }

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

        public SelectList RegistryOptions { get; set; }

        //Used when the time slot expired eg. January 7 from 2:45pm to 3:15pm
        public string TimeSlotFriendlyName { get; set; }

        //Date properties
        public int ContainerId { get; set; }

        //Date selected in the swiper
        public string SelectedCaseDate { get; set; }

        //Selected court class in dropdown
        public string SelectedCourtClass { get; set; }

        //Contact person number for registry
        public string RegistryContactNumber { get; set; }

    }
}
