using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class CaseBookingRequest
    {
        [Key]
        public int Id { get; set; }

        [StringLength(36)]
        [Required]
        public Guid SmGovUserGuid { get; set; }

        [Required]
        public decimal CaseId { get; set; }

        [Required]
        public int BookingPeriodId { get; set; }

        [Required]
        public decimal TrialLength { get; set; }

        [Required]
        public int HearingType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
