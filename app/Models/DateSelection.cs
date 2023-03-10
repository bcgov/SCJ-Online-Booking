using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class DateSelection
    {
        [Key]
        public int Id { get; set; }
        public int CaseBookingRequestId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PreferenceOrder { get; set; }

        public virtual CaseBookingRequest CaseBookingRequest { get; set; }
    }
}
