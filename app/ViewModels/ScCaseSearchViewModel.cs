using System.ComponentModel.DataAnnotations;
using SCJ.OnlineBooking;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SCJ.Booking.MVC.ViewModels
{
    public class ScCaseSearchViewModel
    {
        public ScCaseSearchViewModel()
        {
            //Default values
            Results = new AvailableDatesByLocation();
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
        public bool IsValidCaseNumber
        {
            get
            {
                return (CourtFiles?.Length ?? 0) > 0;
            }
        }

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

        //Full date for the booking
        public DateTime FullDate
        {
            get
            {
                var result = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(SelectedCaseDate) &&
                    long.TryParse(SelectedCaseDate, out long ticks))
                {
                    result = new DateTime(ticks);
                }
                return result;
            }
        }

        //Selected court class in dropdown
        public string SelectedCourtClass { get; set; }

        //Contact person number for registry
        public string RegistryContactNumber { get; set; }

        public bool IsConfirmingCase = false;
        public string FullCaseNumber { get; set; }
        public string LocationPrefix { get; set; }
        public int SelectedCaseId { get; set; }
        public CourtFile[] CourtFiles { get; set; }
        public List<CourtFile> Cases
        {
            get
            {
                return CourtFiles?.Where(x =>
                    string.IsNullOrWhiteSpace(SelectedCourtClass) ||
                    x.courtClassCode == SelectedCourtClass).ToList();
            }
        }
        public CourtFile SelectedCourtFile
        {
            get
            {
                return CourtFiles?.Where(x => x.physicalFileId == SelectedCaseId).FirstOrDefault();
            }
        }
        public string SelectedFileNumber
        {
            get
            {
                return SelectedCourtFile?.courtClassCode + SelectedCourtFile?.courtFileNumber;
            }
        }
        public string SelectedCourtClassName
        {
            get
            {
                return GetCourtClass(SelectedCourtFile?.courtClassCode);
            }
        }
        public string FileNumber
        {
            get
            {
                return LocationPrefix + " " + SelectedFileNumber;
            }
        }
        public List<int> AvailableConferenceTypeIds { get; set; }    
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
            return $"Unknown Court Class for {value}?";
        }
    }
}
