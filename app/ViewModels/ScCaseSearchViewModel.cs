using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SCJ.Booking.MVC.Utils;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels
{
    public class ScCaseSearchViewModel
    {
        private readonly string format = "yyyy-MM-dd";

        public ScCaseSearchViewModel()
        {
            //Default values
            TimeSlotExpired = false;
            CaseLocationName = string.Empty;
            CaseRegistryId = -1;
            BookingLocationName = string.Empty;
            HearingBookingRegistryId = -1;
            CaseNumber = string.Empty;
            HearingTypeId = -1;
            HearingTypeName = string.Empty;
            EstimatedTrialLength = null;
            IsHomeRegistry = null;
            IsLocationChangeFiled = null;
            TrialLocation = -1;
        }

        //Search fields
        public string CaseNumber { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public string BookingFormula { get; set; }
        public int? EstimatedTrialLength { get; set; }
        public bool? IsHomeRegistry { get; set; }
        public bool? IsLocationChangeFiled { get; set; }
        public int TrialLocation { get; set; }

        //Indicates if the case number is valid or not
        public bool IsValidCaseNumber
        {
            get { return (CourtFiles?.Length ?? 0) > 0; }
        }

        //Indicates if the time slot expired
        public bool TimeSlotExpired { get; set; }

        // Selected Registry ID
        [Required(ErrorMessage = "Please select the registry where the file was created.")]
        public int CaseRegistryId { get; set; }

        // Selected Registry Name
        public string CaseLocationName { get; set; }

        // Id of the registry where hearings are booked for the selected registry (varies based on HearingTypeId)
        public int HearingBookingRegistryId { get; set; }

        // Name of the registry where hearings are booked for the selected registry (varies based on HearingTypeId)
        public string BookingLocationName { get; set; }

        //Used when the time slot expired eg. January 7 from 2:45pm to 3:15pm
        public string TimeSlotFriendlyName { get; set; }

        //Selected court class in dropdown
        public string SelectedCourtClass { get; set; }

        //Contact person number for registry
        public string RegistryContactNumber { get; set; }

        public bool IsConfirmingCase = false;
        public string FullCaseNumber { get; set; }
        public string LocationPrefix { get; set; }

        //[Required(ErrorMessage = "Please choose a case")]
        public int SelectedCaseId { get; set; }
        public CourtFile[] CourtFiles { get; set; }
        public List<CourtFile> Cases
        {
            get
            {
                return CourtFiles
                    ?.Where(x =>
                        string.IsNullOrWhiteSpace(SelectedCourtClass)
                        || x.courtClassCode == SelectedCourtClass
                    )
                    .ToList();
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
            get { return SelectedCourtFile?.courtClassCode + SelectedCourtFile?.courtFileNumber; }
        }
        public string SelectedCourtClassName
        {
            get { return ScCourtClass.GetCourtClass(SelectedCourtFile?.courtClassCode); }
        }
        public string FileNumber
        {
            get { return LocationPrefix + " " + SelectedFileNumber; }
        }
        public List<int> AvailableConferenceTypeIds { get; set; }
    }
}
