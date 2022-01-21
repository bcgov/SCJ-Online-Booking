using System.ComponentModel.DataAnnotations;
using SCJ.OnlineBooking;
using System.Collections.Generic;
using System.Linq;

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
            CaseLocationName = string.Empty;
            CaseRegistryId = -1;
            BookingLocationName = string.Empty;
            BookingRegistryId = -1;
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
        public int HearingLengthMinutes 
        {
            get
            {
                return Results?.BookingDetails?.detailBookingLength ?? 0;
            }
        }

        //Indicates if the case number is valid or not
        public bool IsValidCaseNumber { get; set; }

        //Indicates if the time slot expired
        public bool TimeSlotExpired { get; set; }

        // Selected Registry ID
        [Required(ErrorMessage = "Please select the registry where the file was created.")]
        public int CaseRegistryId { get; set; }

        // Selected Registry Name
        public string CaseLocationName { get; set; }

        // Id of the registry where hearings are booked for the selected registry (varies based on HearingTypeId)
        public int BookingRegistryId { get; set; }

        // Name of the registry where hearings are booked for the selected registry (varies based on HearingTypeId)
        public string BookingLocationName { get; set; }

        //Used when the time slot expired eg. January 7 from 2:45pm to 3:15pm
        public string TimeSlotFriendlyName { get; set; }

        //Date properties
        public int ContainerId { get; set; }

        //Date selected in the swiper control
        public string SelectedCaseDate { get; set; }

        //Selected court class in dropdown
        public string SelectedCourtClass { get; set; }

        //Contact person number for registry
        public string RegistryContactNumber { get; set; }

        public bool IsConfirmingCase = false;
        public string FullCaseNumber { get; set; }
        public int SelectedCaseId { get; set; }
        public CourtFile[] CourtFiles { get; set; }
        public bool HasCourtFiles
        {
            get
            {
                return (CourtFiles?.Length ?? 0) > 0;
            }
        }
        public List<CourtFile> Cases
        {
            get
            {
                return CourtFiles?.Where(x =>
                    string.IsNullOrWhiteSpace(SelectedCourtClass) ||
                    x.courtClassCode == SelectedCourtClass).ToList();
            }
        }

        public string GetCourtClass(string value)
        {
            switch (value)
            {
                case "B":
                    return "Bankruptcy";
                case "E":
                    return "Family Law Proceedings (incl. Divorce Act)";
                case "H":
                    return "Foreclosure";
                case "M":
                    return "Motor vehicle";
            }
            return $"[Unknown Court Class for {value}?]";
        }
    }
}
