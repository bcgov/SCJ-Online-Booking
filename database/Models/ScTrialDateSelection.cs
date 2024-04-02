using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.Data.Models
{
    public class ScTrialDateSelection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ScTrialBookingRequest? BookingRequest { get; set; }

        public DateTime TrialStartDate { get; set; }

        public int Rank { get; set; }
    }
}
