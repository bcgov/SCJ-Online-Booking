using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public bool? FactumFiled { get; set; }
        public bool? DateIsAgreed { get; set; }
        public bool? IsFullDay { get; set; }
        public int? HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public Dictionary<DateTime, List<DateTime>> Results { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string SubmitButton { get; set; }
        public bool TimeSlotExpired { get; set; }
        public CoAClassInfo[] CaseList { get; set; }
        public List<string> SelectedCases { get; set; }
        public CoAChambersApplications[] ChambersApplicationTypes { get; set; }
        public List<string> SelectedApplicationTypes { get; set; }
        public bool? IsHalfHour { get; set; }

        public bool Step1Complete
        {
            get
            {
                if (string.IsNullOrEmpty(CaseNumber) || CaseNumber.ToUpper().Trim() == "CA")
                {
                    return false;
                }

                if (IsValidCaseNumber is null or false)
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

        public bool Step2Complete => IsAppealHearing.HasValue && DateIsAgreed is true;

        public bool Step3Complete
        {
            get
            {
                //true value means the hearing is an appeals hearing
                if (IsAppealHearing is true)
                {
                    if (FactumFiled is null or false)
                    {
                        return false;
                    }

                    if (!IsFullDay.HasValue)
                    {
                        return false;
                    }
                }
                //false means chambers hearing
                else
                {
                    if (SelectedApplicationTypes == null)
                    {
                        return false;
                    }

                    if (!SelectedApplicationTypes.Any())
                    {
                        return false;
                    }

                    if (!IsHalfHour.HasValue)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public string MinimumAvailabilityNeeded
        {
            get
            {
                if (IsAppealHearing is true)
                {
                    return IsFullDay is true ? "Full Day" : "";
                }
                return IsHalfHour is false ? "One hour" : "";
            }
        }

        public string HearingLengthText
        {
            get
            {
                if (IsAppealHearing is true)
                {
                    return IsFullDay is true ? "a full day" : "a half day";
                }
                return IsHalfHour is true ? "half an hour" : "one hour";
            }
        }

        public string HearingRoomType
        {
            get
            {
                if (IsAppealHearing.GetValueOrDefault(false))
                {
                    return "Appeal Hearing";
                }
                return "Chambers Hearing";
            }
        }
    }
}
