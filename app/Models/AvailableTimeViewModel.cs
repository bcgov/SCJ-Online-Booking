using System;

namespace SCJ.Booking.MVC.Models
{
    public class AvailableTimeViewModel
    {
        public int ContainerId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
