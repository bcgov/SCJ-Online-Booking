using SCJ.Booking.Data.Constants;
using SCJ.Booking.Data.Models;

namespace SCJ.Booking.TaskRunner.Utils
{
    public class LotteryEmailViewModel
    {
        public LotteryEmailViewModel(ScTrialBookingRequest bookingRequest)
        {
            EmailAddress = bookingRequest.Email;
            Phone = bookingRequest.Phone;
            FullCaseNumber =
                $"{bookingRequest.CaseRegistryCode} {bookingRequest.CourtClassCode}{bookingRequest.CaseNumber}";
            StyleOfCause = bookingRequest.StyleOfCause;
            CourtClassName = ScCourtClass.GetCourtClass(bookingRequest.CourtClassCode);

            TrialLength =
                bookingRequest.HearingLength == 1
                    ? "1 day"
                    : $"{bookingRequest.HearingLength} days";

            TrialLocationName = bookingRequest.TrialLocationName ?? "";

            FairUseDate =
                bookingRequest
                    .TrialDateSelections.FirstOrDefault(x =>
                        x.Rank == bookingRequest.AllocatedSelectionRank
                    )
                    ?.TrialStartDate.ToString("dddd MMMM d, yyyy") ?? "";

            TrialBookingId = bookingRequest.TrialBookingId;

            NextMonth = bookingRequest.FairUseBookingPeriodStartDate.AddMonths(1).ToString("MMMM");
        }

        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string FullCaseNumber { get; set; }
        public string StyleOfCause { get; set; }
        public string CourtClassName { get; set; }
        public string TrialLength { get; set; }
        public string TrialLocationName { get; set; }
        public string FairUseDate { get; set; }
        public string TrialBookingId { get; set; }
        public string NextMonth { get; set; }
    }
}
