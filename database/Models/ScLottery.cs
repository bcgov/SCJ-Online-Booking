using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.Data.Models
{
    public class ScLottery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookingLocationId { get; set; }

        public int HearingTypeId { get; set; }

        [MaxLength(10)]
        public string BookHearingCode { get; set; } = string.Empty;

        // BookingLocationId and FairUseBookingPeriodEndDate are unique together
        public DateTime FairUseBookingPeriodEndDate { get; set; }

        // this is just stored for future reporting purposes
        public DateTime FairUseBookingPeriodStartDate { get; set; }

        public DateTime InitiationTime { get; set; }
        public DateTime? CompletionTime { get; set; }

        public ICollection<ScLotteryBookingRequest> TrialBookingRequests { get; } =
            new List<ScLotteryBookingRequest>();
    }
}
