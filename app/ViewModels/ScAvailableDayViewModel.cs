using System;
using System.Collections.Generic;

namespace SCJ.Booking.MVC.ViewModels
{
    public class ScAvailableDayViewModel
    {
        public DateTime Date { get; set; }
        public string Weekday { get; set; }
        public string FormattedDate { get; set; }
        public List<ScAvailableTimeViewModel> Times { get; set; }
    }
}
