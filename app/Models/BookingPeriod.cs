using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class BookingPeriod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RegistryId { get; set; }

        [Required]
        public DateTime OpeningDate { get; set; }

        [Required]
        public DateTime ClosingDate { get; set; }
    }
}
