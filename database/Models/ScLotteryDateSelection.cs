using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.Data.Models
{
    public class ScLotteryDateSelection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ScLotteryBookingRequest BookingRequest { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public int Rank { get; set; }

        [MaxLength(255)]
        public string? BookingResult { get; set; }
    }
}
