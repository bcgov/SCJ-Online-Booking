using System;

namespace SCJ.Booking.MVC.ViewModels
{
    public class ScAvailableTimeViewModel
    {
        public int ContainerId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
