using System;
using System.Collections.Generic;

namespace SCJ.Booking.CourtBookingPrototype.Models
{
    public partial class RegistrySetting
    {
        public int Id { get; set; }
        public string RegistryId { get; set; }
        public bool UsesLottery { get; set; }
        public int MaximumDateSelections { get; set; }
        public int MonthlyBookingWeek { get; set; }
        public int MonthlyBookingDay { get; set; }
        public DateTime? BookingEndTime { get; set; }
        public DateTime? BookingStartTime { get; set; }

        public virtual ICollection<BookingPeriod> BookingPeriods { get; set; }
    }
}
