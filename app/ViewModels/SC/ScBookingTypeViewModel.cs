using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SCJ.Booking.MVC.Utils;

namespace SCJ.Booking.MVC.ViewModels.SC
{
    public class ScBookingTypeViewModel
    {
        public ScBookingTypeViewModel()
        {
            //Default values
            HearingTypeId = -1;
            HearingTypeName = string.Empty;
            EstimatedTrialLength = null;
            IsHomeRegistry = null;
            IsLocationChangeFiled = null;
            AlternateLocationRegistryId = -1;
        }

        //Booking type fields
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }

        [Display(Name = "Estimated Length of Trial")]
        public int? EstimatedTrialLength { get; set; }
        public int ChambersHearingSubType { get; set; }
        public int? EstimatedChambersLength { get; set; }
        public bool? IsHomeRegistry { get; set; }
        public bool? IsLocationChangeFiled { get; set; }
        public int AlternateLocationRegistryId { get; set; }
        public List<string> AvailableBookingTypes { get; set; }
        public bool FutureTrialBooked { get; set; } = false;
        public bool HasExistingTrialRequest { get; set; } = false;

        // Session object
        public ScSessionBookingInfo SessionInfo { get; set; }
    }
}
