using System;
using System.Collections.Generic;
using System.Linq;
using SCJ.Booking.MVC.Utils;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels.SC
{
    public class ScConferenceAvailableSlotsViewModel
    {
        private const string DateFormat = "yyyy-MM-dd";

        public int HearingTypeId { get; set; } = -1;

        public bool TimeSlotExpired { get; set; } = false;

        // Id of the registry where hearings are booked for the selected registry (varies based on HearingTypeId)
        public int ConferenceLocationRegistryId { get; set; } = -1;

        public int ContainerId { get; set; } = -1;

        //Date selected in the swiper control
        public string SelectedConferenceDate { get; set; } = string.Empty;

        // Date saved with the form submission
        public string SelectedDate { get; set; }

        //Available dates
        public AvailableDatesByLocation AvailableConferenceDates { get; set; } = new();

        public int HearingLengthMinutes
        {
            get { return AvailableConferenceDates?.BookingDetails?.detailBookingLength ?? 0; }
        }

        public string[] AvailableDates =>
            AvailableConferenceDates
                ?.AvailableDates?.Select(x => x.Date_Time.ToString(DateFormat))
                .Distinct()
                .ToArray();

        public string FirstAvailableDate
        {
            get
            {
                return (AvailableConferenceDates?.AvailableDates?.Any() == true)
                    ? AvailableConferenceDates
                        .AvailableDates.Select(x => x.Date_Time.Date)
                        .OrderBy(x => x)
                        .FirstOrDefault()
                        .ToString(DateFormat)
                    : DateTime.Now.ToString(DateFormat);
            }
        }
        public string LastAvailableDate
        {
            get
            {
                return (AvailableConferenceDates?.AvailableDates?.Any() == true)
                    ? AvailableConferenceDates
                        .AvailableDates.Select(x => x.Date_Time.Date)
                        .OrderBy(x => x)
                        .LastOrDefault()
                        .ToString(DateFormat)
                    : DateTime.Now.ToString(DateFormat);
            }
        }
        private (int, int)[] AvailableYearsAndMonths =>
            AvailableConferenceDates
                ?.AvailableDates?.Select(x => (x.Date_Time.Year, x.Date_Time.Month))
                .Distinct()
                .ToArray();

        public List<string> DisabledDates
        {
            get
            {
                var result = new List<string>();
                if (AvailableYearsAndMonths?.Length > 0)
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

        //Full date for conference hearing bookings
        public DateTime ParsedConferenceDate =>
            !string.IsNullOrEmpty(SelectedConferenceDate)
                ? DateTime.Parse(SelectedConferenceDate)
                : DateTime.MinValue;

        // Session object
        public ScSessionBookingInfo SessionInfo { get; set; }
    }
}
