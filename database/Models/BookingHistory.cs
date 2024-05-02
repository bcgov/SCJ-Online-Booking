using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.Data.Models
{
    /// <summary>
    ///     This is a simple logging table for analyzing unusual user activity
    ///     and collecting statistics
    /// </summary>
    public class BookingHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        [MaxLength(20)]
        public string? BookingLocationName { get; set; }

        [Required]
        [MaxLength(3)]
        // "COA" or "SC"
        public string? CourtLevel { get; set; }

        [MaxLength(8)]
        // "Criminal" or "Civil" (COA only)
        public string? CoaCaseType { get; set; }

        [MaxLength(8)]
        // "Appeal" or "Chambers" (COA only)
        public string? CoaConferenceType { get; set; }

        // Hearing Type Id (SC only)
        public int? ScHearingType { get; set; }

        [MaxLength(8)]
        // "Fair-Use" or "Regular" (SC 9001 only)
        public string? ScFormulaType { get; set; }

        [Required]
        public OidcUser? User { get; set; }
    }
}
