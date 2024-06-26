using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SCJ.Booking.Data.Constants;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels.SC
{
    public class ScCaseSearchViewModel
    {
        public ScCaseSearchViewModel()
        {
            //Default values
            CaseLocationName = string.Empty;
            CaseRegistryId = -1;
            BookingLocationName = string.Empty;
            CaseNumber = null;
        }

        //Search fields
        [Display(Name = "Action Number")]
        public int? CaseNumber { get; set; }

        // Selected Registry ID
        [Required(ErrorMessage = "Please select the registry where the file was created.")]
        public int CaseRegistryId { get; set; }

        // Selected Registry Name
        public string CaseLocationName { get; set; }

        // Name of the registry where hearings are booked for the selected registry (varies based on HearingTypeId)
        public string BookingLocationName { get; set; }

        //Selected court class in dropdown
        public string SelectedCourtClass { get; set; }

        //Contact person number for registry
        public string RegistryContactNumber { get; set; }

        public bool IsConfirmingCase = false;
        public string LocationPrefix { get; set; }

        //[Required(ErrorMessage = "Please choose a case")]
        public int SelectedCaseId { get; set; }
        public CourtFile[] CaseSearchResults { get; set; }
        public List<CourtFile> Cases
        {
            get
            {
                return CaseSearchResults
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
                return CaseSearchResults
                    ?.Where(x => x.physicalFileId == SelectedCaseId)
                    .FirstOrDefault();
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
        public string FullCaseNumber => $"{LocationPrefix} {SelectedFileNumber}";
    }
}
