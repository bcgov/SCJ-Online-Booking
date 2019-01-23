using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCJ.Booking.MVC.Models
{

    public class BookingHistory
    {
        [StringLength(36)]
        [Required]
        public string SmGovUserGuid { get; set; }

        [Required]
        public long ContainerId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
