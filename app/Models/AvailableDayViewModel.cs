using System;
using System.Collections.Generic;

namespace SCJ.Booking.MVC.Models
{
    public class AvailableDayViewModel
    {
        public DateTime Date { get; set; }
        public List<AvailableTimeViewModel> Times { get; set; }
    }
}
