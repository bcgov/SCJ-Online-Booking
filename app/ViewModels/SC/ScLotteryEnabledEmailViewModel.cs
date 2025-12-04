using System.Collections.Generic;
using System.Linq;
using SCJ.Booking.MVC.Utils;

namespace SCJ.Booking.MVC.ViewModels.SC
{
    public class ScLotteryEnabledEmailViewModel
    {
        public ScLotteryEnabledEmailViewModel(ScSessionBookingInfo bookingInfo)
        {
            // format fair use dates
            FairUseDates = bookingInfo
                .SelectedFairUseDates.Select(date => date.ToString("dddd MMMM d, yyyy"))
                .ToList();

            // format regular use date
            RegularDate = "";
            if (bookingInfo.SelectedRegularDate.HasValue)
            {
                RegularDate = bookingInfo.SelectedRegularDate.Value.ToString("dddd MMMM d, yyyy");
            }

            HearingLength =
                bookingInfo.BookingLength == 1 ? "1 day" : $"{bookingInfo.BookingLength} days";
        }

        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string LocationPrefix { get; set; }
        public string FullCaseNumber { get; set; }
        public string StyleOfCause { get; internal set; }
        public string CourtClassName { get; internal set; }
        public string HearingLength { get; set; }
        public string CaseLocationName { get; set; }
        public string BookingLocationName { get; set; }
        public string HearingLocationName { get; set; }
        public string ResultDate { get; set; }

        // selected single date for regular trial bookings
        public string RegularDate { get; set; }

        // multiple requested dates for "fair use" lottery
        public List<string> FairUseDates { get; set; }
        public string LotteryEntryId { get; set; }
        public string RegistryContactNumber { get; set; }
        public string ChambersHearingSubTypeName { get; set; }
    }
}
