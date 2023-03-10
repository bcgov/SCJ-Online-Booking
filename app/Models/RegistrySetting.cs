using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCJ.Booking.MVC.Models
{
    public class RegistrySetting
    {
        [Key]
        public int Id { get; set; }
        public int RegistryId { get; set; }
        public bool UsesLottery { get; set; }
        public int MaximumDateSelections { get; set; }
        public int MonthlyBookingWeek { get; set; }
        public int MonthlyBookingDay { get; set; }
        public DateTime? BookingEndTime { get; set; }
        public DateTime? BookingStartTime { get; set; }

        public virtual ICollection<BookingPeriod> BookingPeriods { get; set; }
    }
}
