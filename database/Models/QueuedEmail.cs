using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.Data.Models
{
    public class QueuedEmail
    {
        public QueuedEmail()
        {
            CourtLevel = "SC";
            ToEmail = "";
            Subject = "";
            Body = "";
            TimeStamp = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CourtLevel { get; set; }

        [Required]
        public string ToEmail { get; set; }

        [Required]
        public string Subject { get; set; }
        public string Body { get; set; }

        public DateTime TimeStamp { get; set; }
        public bool IsLotteryResult { get; set; }
    }
}
