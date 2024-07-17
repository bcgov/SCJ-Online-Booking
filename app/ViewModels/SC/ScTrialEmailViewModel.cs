using System.Collections.Generic;
using System.Linq;
using SCJ.Booking.MVC.Utils;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels.SC
{
    public class ScTrialEmailViewModel
    {
        public ScTrialEmailViewModel(ScSessionBookingInfo bookingInfo)
        {
            // format fair use dates
            FairUseDates = bookingInfo
                .SelectedFairUseTrialDates.Select(date => date.ToString("dddd MMMM d, yyyy"))
                .ToList();

            // format regular use date
            RegularDate = "";
            if (bookingInfo.SelectedRegularTrialDate.HasValue)
            {
                RegularDate = bookingInfo.SelectedRegularTrialDate.Value.ToString(
                    "dddd MMMM d, yyyy"
                );
            }

            TrialLength =
                bookingInfo.EstimatedTrialLength == 1
                    ? "1 day"
                    : $"{bookingInfo.EstimatedTrialLength} days";
        }

        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string LocationPrefix { get; set; }
        public string FullCaseNumber { get; set; }
        public string StyleOfCause { get; internal set; }
        public string CourtClassName { get; internal set; }
        public string TrialLength { get; set; }
        public string CaseLocationName { get; set; }
        public string BookingLocationName { get; set; }
        public string TrialLocationName { get; set; }
        public string ResultDate { get; set; }

        // selected single date for regular trial bookings
        public string RegularDate { get; set; }

        // multiple requested dates for "fair use" lottery
        public List<string> FairUseDates { get; set; }
        public string TrialBookingId { get; set; }
        public string RegistryContactNumber { get; set; }
    }
}
