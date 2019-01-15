using System;

namespace SCJ.Booking.MVC.Models
{
    public class AvailableTimeViewModel
    {
        public int ContainerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
