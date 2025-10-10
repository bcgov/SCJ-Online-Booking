using System;
using System.Collections.Generic;
using System.Linq;
using SCJ.Booking.MVC.Utils;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels.SC
{
    public class ScAvailableSlotsViewModel
    {
        private const string DateFormat = "yyyy-MM-dd";

        public ScAvailableSlotsViewModel()
        {
            //Default values
            AvailableConferenceDates = new AvailableDatesByLocation();
            TimeSlotExpired = false;
            ConferenceLocationRegistryId = -1;
            ContainerId = -1;
            SelectedConferenceDate = string.Empty;
            CaseNumber = string.Empty;
            HearingTypeId = -1;
            FormulaType = string.Empty;
            AvailableRegularDates = new List<DateTime> { };
            AvailableFairUseDates = new List<DateTime> { };
            FairUseStartDate = null;
            FairUseEndDate = null;
            FairUseResultDate = null;
            FairUseSelectionDate = null;
            RegistryContactNumber = string.Empty;
        }

        //Search fields
        public string CaseNumber { get; set; }
        public int HearingTypeId { get; set; }
        public string FormulaType { get; set; }

        //Available dates
        public AvailableDatesByLocation AvailableConferenceDates { get; set; }
        public int HearingLengthMinutes
        {
            get { return AvailableConferenceDates?.BookingDetails?.detailBookingLength ?? 0; }
        }

        public string[] AvailableDates
        {
            get
            {
                var result = AvailableConferenceDates
                    ?.AvailableDates?.Select(x => x.Date_Time.ToString(DateFormat))
                    .Distinct()
                    .ToArray();
                return result;
            }
        }
        public string FirstAvailableDate
        {
            get
            {
                var result = DateTime.Now.ToString(DateFormat);
                if (
                    AvailableConferenceDates?.AvailableDates != null
                    && AvailableConferenceDates.AvailableDates.Any()
                )
                {
                    result = AvailableConferenceDates
                        .AvailableDates.Select(x => x.Date_Time.Date)
                        .OrderBy(x => x)
                        .FirstOrDefault()
                        .ToString(DateFormat);
                }
                return result;
            }
        }
        public string LastAvailableDate
        {
            get
            {
                var result = DateTime.Now.ToString(DateFormat);
                if (
                    AvailableConferenceDates?.AvailableDates != null
                    && AvailableConferenceDates.AvailableDates.Any()
                )
                {
                    result = AvailableConferenceDates
                        .AvailableDates.Select(x => x.Date_Time.Date)
                        .OrderBy(x => x)
                        .LastOrDefault()
                        .ToString(DateFormat);
                }
                return result;
            }
        }
        private (int, int)[] AvailableYearsAndMonths
        {
            get
            {
                var result = AvailableConferenceDates
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
                                    ).ToString(DateFormat)
                                )
                        );
                    }
                    result = result.Where(x => !AvailableDates.Contains(x)).ToList();
                }
                return result;
            }
        }

        //Indicates if the time slot expired
        public bool TimeSlotExpired { get; set; }

        // Id of the registry where hearings are booked for the selected registry (varies based on HearingTypeId)
        public int ConferenceLocationRegistryId { get; set; }

        //[Required(ErrorMessage = "Please choose from one of the available times")]
        public int ContainerId { get; set; }

        //Date selected in the swiper control
        public string SelectedConferenceDate { get; set; }
        public string SelectedDate { get; set; }

        //Full date for conference hearing bookings
        public DateTime ParsedConferenceDate
        {
            get
            {
                var result = DateTime.MinValue;

                if (!string.IsNullOrEmpty(SelectedConferenceDate))
                {
                    result = DateTime.Parse(SelectedConferenceDate);
                }
                return result;
            }
        }

        public List<DateTime> AvailableRegularDates { get; set; }
        public List<DateTime> AvailableFairUseDates { get; set; }
        public DateTime? SelectedRegularDate { get; set; }
        public List<DateTime> SelectedFairUseDates { get; set; } = new();
        public DateTime? FairUseStartDate { get; set; }
        public DateTime? FairUseEndDate { get; set; }
        public DateTime? FairUseResultDate { get; set; }
        public DateTime? FairUseSelectionDate { get; set; }

        // Session object
        public ScSessionBookingInfo SessionInfo { get; set; }
        public string RegistryContactNumber { get; set; }
    }
}
