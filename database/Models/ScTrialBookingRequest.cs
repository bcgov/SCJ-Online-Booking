using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.Data.Models
{
    public class ScTrialBookingRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public OidcUser? User { get; set; }
        public ScLottery? Lottery { get; set; }
        public int CaseRegistryId { get; set; }

        [MaxLength(2)]
        public string CaseRegistryCode { get; set; } = "";

        public string CaseNumber { get; set; }
        public decimal CeisPhysicalFileId { get; set; }

        [MaxLength(255)]
        public string StyleOfCause { get; set; } = "";

        public int TrialLocationId { get; set; }
        public int BookingLocationId { get; set; }
        public string? TrialLocationName { get; set; }
        public DateTime FairUseBookingPeriodEndDate { get; set; }
        public DateTime FairUseBookingPeriodStartDate { get; set; }
        public DateTime LotteryStartDate { get; set; }

        [MaxLength(1)]
        public string CourtClassCode { get; set; } = "";

        [MaxLength(10)]
        public string BookHearingCode { get; set; } = "";

        public int HearingLength { get; set; }
        public int FairUseSort { get; set; }

        [MaxLength(40)]
        public string RequestedByName { get; set; } = ""; // comes from Keycloak claims

        [MaxLength(20)]
        public string Phone { get; set; } = "";

        [MaxLength(40)]
        public string Email { get; set; } = "";

        public DateTime CreationTimestamp { get; set; }
        public string TrialBookingId { get; set; } = "";
        public int LotteryPosition { get; set; }
        public bool IsProcessed { get; set; } = false;
        public int AllocatedSelectionRank { get; set; }
        public ICollection<ScTrialDateSelection> TrialDateSelections { get; } =
            new List<ScTrialDateSelection>();

        [MaxLength(255)]
        public string? UnmetDemandBookingResult { get; set; }

        public DateTime? ProcessingTimestamp { get; set; }
    }
}
