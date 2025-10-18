using SCJ.Booking.Data.Constants;
using SCJ.Booking.Data.Models;

namespace SCJ.Booking.TaskRunner.Utils
{
    public class LotteryEmailViewModel
    {
        public LotteryEmailViewModel(ScLotteryBookingRequest bookingRequest)
        {
            EmailAddress = bookingRequest.Email;
            Phone = bookingRequest.Phone;
            FullCaseNumber =
                $"{bookingRequest.CaseRegistryCode} {bookingRequest.CourtClassCode}{bookingRequest.CaseNumber}";
            StyleOfCause = bookingRequest.StyleOfCause;
            CourtClassName = ScCourtClass.GetCourtClass(bookingRequest.CourtClassCode);

            HearingLength =
                bookingRequest.HearingLength == 1
                    ? "1 day"
                    : $"{bookingRequest.HearingLength} days";

            HearingLocationName = bookingRequest.LocationName ?? "";

            AllocatedStartDate =
                bookingRequest
                    .DateSelections.FirstOrDefault(x =>
                        x.Rank == bookingRequest.AllocatedSelectionRank
                    )
                    ?.StartDate.ToString("dddd MMMM d, yyyy") ?? "";

            LotteryEntryId = bookingRequest.LotteryEntryId;

            NextMonth = bookingRequest.FairUseBookingPeriodStartDate.AddMonths(1).ToString("MMMM");

            ChambersHearingSubTypeName = bookingRequest.LongChambersHearingSubTypeName;
        }

        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string FullCaseNumber { get; set; }
        public string StyleOfCause { get; set; }
        public string CourtClassName { get; set; }
        public string HearingLength { get; set; }
        public string HearingLocationName { get; set; }
        public string AllocatedStartDate { get; set; }
        public string LotteryEntryId { get; set; }
        public string NextMonth { get; set; }
        public string ChambersHearingSubTypeName { get; set; }
    }
}
