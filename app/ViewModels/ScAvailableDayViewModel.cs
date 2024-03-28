using System;
using System.Collections.Generic;

namespace SCJ.Booking.MVC.ViewModels
{
    public class ScAvailableDayViewModel
    {
        public DateTime Date { get; set; }
        public string Weekday { get; set; }
        public string FormattedDate { get; set; }
        public List<HearingTime> Times { get; set; }
    }

    public class HearingTime
    {
        public int ContainerId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
