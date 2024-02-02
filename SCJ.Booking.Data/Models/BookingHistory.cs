using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.Data.Models
{
    public class BookingHistory
    {
        [StringLength(36)]
        [Required]
        public string SmGovUserGuid { get; set; } = string.Empty;

        [Required]
        public long ContainerId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
