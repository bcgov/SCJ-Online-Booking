using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels
{
    public class ScCaseSearchViewModel
    {
        private readonly string format = "yyyy-MM-dd";

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
            EstimatedTrialLength = null;
            IsHomeRegistry = null;
            IsLocationChangeFiled = null;
            TrialLocation = string.Empty;
        }

        //Search fields
        public string CaseNumber { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public int? EstimatedTrialLength { get; set; }
        public bool? IsHomeRegistry { get; set; }
        public bool? IsLocationChangeFiled { get; set; }
        public string TrialLocation { get; set; }

        //Available dates
        public AvailableDatesByLocation Results { get; set; }
        public int HearingLengthMinutes
        {
            get { return Results?.BookingDetails?.detailBookingLength ?? 0; }
        }

        public int GetSelectedDateIndex(string selectedDate)
        {
            return AvailableDates != null ? AvailableDates.ToList().IndexOf(selectedDate) : 0;
        }

        public string[] AvailableDates
        {
            get
            {
                var result = Results
                    ?.AvailableDates?.Select(x => x.Date_Time.ToString(format))
                    .Distinct()
                    .ToArray();
                return result;
            }
        }
        public string FirstAvailableDate
        {
            get
            {
                var result = DateTime.Now.ToString(format);
                if (Results?.AvailableDates != null)
                {
                    result = Results
                        .AvailableDates.Select(x => x.Date_Time.Date)
                        .OrderBy(x => x)
                        .FirstOrDefault()
                        .ToString(format);
                }
                return result;
            }
        }
        public string LastAvailableDate
        {
            get
            {
                var result = DateTime.Now.ToString(format);
                if (Results?.AvailableDates != null)
                {
                    result = Results
                        .AvailableDates.Select(x => x.Date_Time.Date)
                        .OrderBy(x => x)
                        .LastOrDefault()
                        .ToString(format);
                }
                return result;
            }
        }
        private (int, int)[] AvailableYearsAndMonths
        {
            get
            {
                var result = Results
                    ?.AvailableDates?.Select(x => (x.Date_Time.Year, x.Date_Time.Month))
                    .Distinct()
                    .ToArray();
                return result;
            }
        }
        public List<string> DisabledDates
        {
            get
            {
                var result = new List<string>();
                if (AvailableYearsAndMonths != null && AvailableYearsAndMonths.Length > 0)
                {
                    foreach (var yearAndMonth in AvailableYearsAndMonths)
                    {
                        result.AddRange(
                            Enumerable
                                .Range(
                                    1,
                                    DateTime.DaysInMonth(yearAndMonth.Item1, yearAndMonth.Item2)
                                )
                                .Select(day =>
                                    new DateTime(
                                        yearAndMonth.Item1,
                                        yearAndMonth.Item2,
                                        day
                                    ).ToString(format)
                                )
                        );
                    }
                    result = result.Where(x => !AvailableDates.Contains(x)).ToList();
                }
                return result;
            }
        }

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
        public int BookingRegistryId { get; set; }

        // Name of the registry where hearings are booked for the selected registry (varies based on HearingTypeId)
        public string BookingLocationName { get; set; }

        //Used when the time slot expired eg. January 7 from 2:45pm to 3:15pm
        public string TimeSlotFriendlyName { get; set; }

        //[Required(ErrorMessage = "Please choose from one of the available times")]
        public int ContainerId { get; set; }

        //Date selected in the swiper control
        public string SelectedCaseDate { get; set; }
        public string SelectedDate { get; set; }

        //Full date for the booking
        public DateTime FullDate
        {
            get
            {
                var result = DateTime.MinValue;
                if (
                    !string.IsNullOrWhiteSpace(SelectedCaseDate)
                    && long.TryParse(SelectedCaseDate, out long ticks)
                )
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
            get { return GetCourtClass(SelectedCourtFile?.courtClassCode); }
        }
        public string FileNumber
        {
            get { return LocationPrefix + " " + SelectedFileNumber; }
        }
        public List<int> AvailableConferenceTypeIds { get; set; }
        public List<string> AvailableBookingTypes { get; set; }

        public string GetCourtClass(string value)
        {
            switch (value)
            {
                case "B":
                    return "Bankruptcy";
                case "E":
                    return "Family Law Proceedings";
                case "H":
                    return "Foreclosure";
                case "L":
                    return "Enforcement/Legislated Statute";
                case "M":
                    return "Motor Vehicle Accidents";
                case "N":
                    return "Adoption";
                case "P":
                    return "Probate";
                case "S":
                    return "Supreme Civil (General)";
                case "V":
                    return "Caveat";
                case "D":
                    return "Divorce";
                case "C":
                    return "Small Claims";
            }
            return $"Unknown Court Class for {value}?";
        }
    }
}
