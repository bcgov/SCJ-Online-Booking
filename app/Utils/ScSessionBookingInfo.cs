using System;
using System.Collections.Generic;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Utils
{
    public class ScSessionBookingInfo
    {
        public int PhysicalFileId { get; set; }
        public int CaseNumber { get; set; }
        public string FullCaseNumber { get; set; }
        public string LocationPrefix { get; set; }

        public int CaseRegistryId { get; set; }
        public string CaseLocationName { get; set; }

        // this is the alternate trial location the user selected from the dropdown
        public int TrialLocationRegistryId { get; set; }

        // this is the internal id that is used for booking trials and conferences
        public int BookingLocationRegistryId { get; set; }

        public string BookingLocationName { get; set; }
        public int ContainerId { get; set; }
        public int HearingTypeId { get; set; }
        public string TrialFormulaType { get; set; }
        public string HearingTypeName { get; set; }
        public int? EstimatedTrialLength { get; set; }
        public bool? IsHomeRegistry { get; set; }
        public bool? IsLocationChangeFiled { get; set; }
        public string SelectedConferenceDateTicks { get; set; }

        public DateTime? SelectedRegularTrialDate { get; set; }
        public List<DateTime> SelectedFairUseTrialDates { get; set; } = new List<DateTime>() { };

        //The result string returned by the SOAP API when the booking was booked
        public string ApiBookingResultMessage { get; set; }

        public bool IsBooked { get; set; }

        public CourtFile[] CaseSearchResults { get; set; }
        public string SelectedCourtClass { get; set; }
        public CourtFile SelectedCourtFile { get; set; }
        public string SelectedCourtClassName { get; set; }
        public AvailableDatesByLocation AvailableConferenceDates { get; set; }

        public int ConferenceLengthMinutes
        {
            get { return AvailableConferenceDates?.BookingDetails?.detailBookingLength ?? 0; }
        }

        //Full date for the booking
        public DateTime SelectedConferenceDate { get; set; }

        public string FormattedConferenceTime
        {
            get
            {
                return $"{SelectedConferenceDate:h:mm tt}–{SelectedConferenceDate.AddMinutes(ConferenceLengthMinutes):h:mm tt}".ToLower();
            }
        }
        public string FormattedConferenceDate
        {
            get { return SelectedConferenceDate.ToString("dddd, MMMM dd, yyyy"); }
        }

        public string FriendlyError
        {
            get
            {
                if (ApiBookingResultMessage == null)
                {
                    return string.Empty;
                }
                return ApiBookingResultMessage.Replace("Fail - ", "");
            }
        }

        public FormulaLocation RegularFormula { get; set; }
        public FormulaLocation FairUseFormula { get; set; }
        public int UnmetDemandMonths { get; set; }
    }
}
