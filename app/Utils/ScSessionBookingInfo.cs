using System;
using System.Collections.Generic;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Utils
{
    /// <summary>
    /// Holds all state collected across the Supreme Court booking steps
    /// (search, type, date/time, confirmation, API booking).
    /// </summary>
    public class ScSessionBookingInfo
    {
        /*********************************************
          STEP 1: SEARCH & CASE SELECTION
         *********************************************/

        /// <summary>Registry ID of the case’s home (originating) registry.</summary>
        public int CaseRegistryId { get; set; }

        /// <summary>Numeric portion of the case number entered by the user.</summary>
        public string CaseNumber { get; set; }

        /// <summary>CEIS physical file ID for the selected case.</summary>
        public int PhysicalFileId { get; set; }

        /// <summary>Full formatted case number (location prefix + class prefix + numeric part).</summary>
        public string FullCaseNumber { get; set; }

        /// <summary>Two-letter location prefix (registry code).</summary>
        public string LocationPrefix { get; set; }

        /// <summary>Display name of the home registry (where the file was created).</summary>
        public string CaseLocationName { get; set; }

        /// <summary>Raw results returned from the case search.</summary>
        public CourtFile[] CaseSearchResults { get; set; }

        /// <summary>Full court file object picked from CaseSearchResults.</summary>
        public CourtFile SelectedCourtFile { get; set; }

        /// <summary>Court class code of the selected case (e.g. “M”, “E”).</summary>
        public string SelectedCourtClass { get; set; }

        /// <summary>Full descriptive court class name.</summary>
        public string SelectedCourtClassName { get; set; }

        /*********************************************
          STEP 2: BOOKING TYPE
         *********************************************/

        /// <summary>Selected hearing type ID.</summary>
        public int HearingTypeId { get; set; }

        /// <summary>Display name of the selected hearing type.</summary>
        public string HearingTypeName { get; set; }

        /// <summary>Selected long chambers sub‑type ID.</summary>
        public int ChambersHearingSubTypeId { get; set; }

        /// <summary>Display name of the selected long chambers sub‑type.</summary>
        public string ChambersHearingSubTypeName { get; set; } = "";

        /// <summary>Requested booking length in days (trial or long chambers).</summary>
        public int? BookingLength { get; set; }

        /// <summary>True if using home registry; false if alternate; null until chosen.</summary>
        public bool? IsHomeRegistry { get; set; }

        /// <summary>True if a location change application has been filed; null until chosen.</summary>
        public bool? IsLocationChangeFiled { get; set; }

        /// <summary>
        /// Alternate registry ID chosen when not booking at the home registry. This
        /// is set to the CaseRegistryId value if IsHomeRegistry is true.
        /// </summary>
        public int AlternateLocationRegistryId { get; set; }

        /// <summary>Display name of the registry where the appearance is scheduled.</summary>
        public string BookingLocationName { get; set; }

        /******************************************
          CONFERENCE BOOKING
         ******************************************/

        /// <summary>Available conference dates and time slots.</summary>
        public AvailableDatesByLocation AvailableConferenceDates { get; set; }

        /// <summary>Selected conference date & time chosen by user.</summary>
        public DateTime SelectedConferenceDate { get; set; }

        /// <summary>Selected conference time‑slot container ID.</summary>
        public int ContainerId { get; set; }

        /// <summary>
        /// This is used for conference bookings over Zoom, where the judge is in a different
        /// location than the parties.
        /// </summary>
        public int BookingLocationRegistryId { get; set; }

        /// <summary>Conference length in minutes (from available slot details).</summary>
        public int ConferenceLengthMinutes =>
            AvailableConferenceDates?.BookingDetails?.detailBookingLength ?? 0;

        /// <summary>Formatted time range for the selected conference slot.</summary>
        public string FormattedConferenceTime =>
            SelectedConferenceDate == default
                ? string.Empty
                : $"{SelectedConferenceDate:h:mm tt}–{SelectedConferenceDate.AddMinutes(ConferenceLengthMinutes):h:mm tt}".ToLower();

        /// <summary>Formatted conference date.</summary>
        public string FormattedConferenceDate =>
            SelectedConferenceDate == default
                ? string.Empty
                : SelectedConferenceDate.ToString("dddd MMMM d, yyyy");

        /******************************************
          TRIAL & LONG CHAMBERS BOOKING
         ******************************************/

        /// <summary>
        /// Value will be "Regular" (books immediately) or "Fair-Use" (has a lottery).
        /// A formula = rule set for a hearing type at a location, including specific
        /// booking window dates.
        /// </summary>
        public string FormulaType { get; set; }

        /// <summary>Chosen date for a regular (non‑lottery) booking.</summary>
        public DateTime? SelectedRegularDate { get; set; }

        /// <summary>List of selected lottery dates in order of preference.</summary>
        public List<DateTime> SelectedFairUseDates { get; set; } = new();

        /// <summary>
        /// Dates and settings for immediate bookings based on the selected
        /// location, court class, and hearing type (trial or chambers).
        /// </summary>
        public FormulaLocation RegularFormula { get; set; }

        /// <summary>
        /// Dates and settings for lottery bookings based on the selected
        /// location, court class, and hearing type (trial or chambers).
        /// </summary>
        public FormulaLocation FairUseFormula { get; set; }

        /******************************************
          API BOOKING REQUEST & RESPONSE
         ******************************************/

        /// <summary>Generated ID passed to API and stored in SCSS for troubleshooting lottery cases.</summary>
        public string LotteryEntryId { get; set; }

        /// <summary>True if Supreme Court Scheduling System (SCSS) booking succeeded via the API.</summary>
        public bool IsBooked { get; set; }

        /// <summary>Raw result message returned by the SCSS booking API.</summary>
        public string ApiBookingResultMessage { get; set; }

        /// <summary>Booking error message with leading “Fail - ” removed.</summary>
        public string FriendlyError =>
            ApiBookingResultMessage == null
                ? string.Empty
                : ApiBookingResultMessage.Replace("Fail - ", "");
    }
}
