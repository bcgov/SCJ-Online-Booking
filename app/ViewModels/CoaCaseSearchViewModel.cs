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
                if (!Step1Complete)
                {
                    return false;
                }

                var dateIsAgreed = DateIsAgreed ?? false;

                switch (CaseType)
                {
                    case CoaCaseType.Civil:
                    {
                        var certificateOrReadiness = CertificateOfReadiness ?? false;

                        if (!dateIsAgreed || !certificateOrReadiness)
                        {
                            return false;
                        }

                        break;
                    }
                    case CoaCaseType.Criminal:
                    {
                        //var lowerCourtOrder = LowerCourtOrder ?? false;

                        if (!dateIsAgreed)
                        {
                            return false;
                        }

                        break;
                    }
                }

                return IsFullDay != null;
            }
        }
    }
}
