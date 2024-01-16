using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SCJ.Booking.MVC.Utils;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels
{
    public class CoaCaseSearchViewModel
    {
        public CoaCaseSearchViewModel()
        {
            //Default values
            CaseNumber = string.Empty;
        }

        [Required(ErrorMessage = "Please provide a Court File Number.")]
        public string CaseNumber { get; set; }
        public int CaseId { get; set; }
        public string CaseType { get; set; }
        public bool? IsValidCaseNumber { get; set; }
        public bool? IsAppealHearing { get; set; }
        public bool? CertificateOfReadiness { get; set; }
        public bool? DateIsAgreed { get; set; }

        //public bool? LowerCourtOrder { get; set; }
        public bool? IsFullDay { get; set; }
        public int? HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public Dictionary<DateTime, List<DateTime>> Results { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string SubmitButton { get; set; }
        public bool TimeSlotExpired { get; set; }
        public CoAClassInfo[] CaseList { get; set; }
        public List<string> SelectedCases { get; set; }
        public CoAChambersApplications[] ChambersApplicationTypes {  get; set; }
        public List<string> SelectedApplicationTypes { get; set; }
        public bool? HalfHourRequired { get; set; }

        public bool Step1Complete
        {
            get
            {
                if (string.IsNullOrEmpty(CaseNumber) || CaseNumber.ToUpper().Trim() == "CA")
                {
                    return false;
                }

                if (!(IsValidCaseNumber ?? false))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(CaseType))
                {
                    return false;
                }

                return true;
            }
        }

        public bool Step2Complete
        {
            get
            {
                var dateIsAgreed = DateIsAgreed ?? false;

                if (!IsAppealHearing.HasValue || !dateIsAgreed)
                {
                    return false;
                }

                return true;
            }
        }

        public bool Step3Complete
        {
            get
            {
                //true value means the hearing is an appeals hearing
                if(IsAppealHearing != null && IsAppealHearing.Value)
                {
                    var certificateOrReadiness = CertificateOfReadiness ?? false;

                    if (!certificateOrReadiness)
                    {
                        return false;
                    }

                    if (IsFullDay is null)
                    {
                        return false;
                    }
                }
                //false means chambers hearing
                else 
                {
                    if(SelectedApplicationTypes != null)
                    {
                        if(SelectedApplicationTypes.Count <= 0)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                    if(HalfHourRequired == null)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
