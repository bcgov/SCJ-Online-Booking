using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace SCJ.Booking.Data.Models
{
    public class ScTrialBookingRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public OidcUser? User { get; set; }
        public int UnmetDemandMonths { get; set; }
        public decimal CeisPhysicalFileId { get; set; }

        [MaxLength(20)]
        public string? FullCaseNumber { get; set; }

        [MaxLength(255)]
        public string? StyleOfCause { get; set; }
        public int LocationId { get; set; }
        public int BookingLocationId { get; set; }
        public string? TrialLocationName { get; set; }
        public DateTime FairUseBookingPeriodEndDate { get; set; }
        public DateTime FairUseBookingPeriodStartDate { get; set; }
        public DateTime FairUseContactDate { get; set; }
        public DateTime TrialPeriodStartDate { get; set; }
        public DateTime TrialPeriodEndDate { get; set; }

        [MaxLength(1)]
        public string? CourtClassCode { get; set; }

        [MaxLength(30)]
        public string? CourtClassName { get; set; }

        [MaxLength(10)]
        public string? BookHearingCode { get; set; }
        public int HearingLength { get; set; }

        [MaxLength(40)]
        public string? RequestedByName { get; set; } // comes from Keycloak claims

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(40)]
        public string? Email { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public DateTime? LotteryBeginTimestamp { get; set; }

        public int LotteryPosition { get; set; }

        public bool IsProcessed { get; set; } = false;

        public DateTime? AllocatedSelectionTrialStartDate { get; set; }
        public int AllocatedSelectionRank { get; set; }

        public ICollection<ScTrialDateSelection> TrialDateSelections { get; } =
            new List<ScTrialDateSelection>();
    }
}
