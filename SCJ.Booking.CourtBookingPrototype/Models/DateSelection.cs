using System;
using System.Collections.Generic;

namespace SCJ.Booking.CourtBookingPrototype.Models
{
    public partial class DateSelection
    {
        public int Id { get; set; }
        public int CaseBookingRequestId { get; set; }
        public DateTime Date { get; set; }
        public int PreferenceOrder { get; set; }

        public virtual CaseBookingRequest CaseBookingRequest { get; set; }
    }
}
