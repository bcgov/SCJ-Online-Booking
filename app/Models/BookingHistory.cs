using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.MVC.Models
{
    public class BookingHistory
    {
        [Key]
        public int Id { get; set; }

        [StringLength(32)]
        [Required]
        public string SmGovUserGuid { get; set; }

        [Required]
        public long ContainerId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
