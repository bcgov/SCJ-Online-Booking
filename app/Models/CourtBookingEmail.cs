using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class CourtBookingEmail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CourtLevel { get; set; }
        [Required]
        public string ToEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public string BodyHtml { get; set; }
    }
}
