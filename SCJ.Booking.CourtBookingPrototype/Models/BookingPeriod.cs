using System;
using System.Collections.Generic;

namespace SCJ.Booking.CourtBookingPrototype.Models
{
    public partial class BookingPeriod
    {
        public int Id { get; set; }
        public int RegistryId { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
