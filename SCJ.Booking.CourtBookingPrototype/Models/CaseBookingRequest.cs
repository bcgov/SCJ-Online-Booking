using System;
using System.Collections.Generic;

namespace SCJ.Booking.CourtBookingPrototype.Models
{
    public partial class CaseBookingRequest
    {
        public int Id { get; set; }
        public Guid SmGovUserGuid { get; set; }
        public decimal PhysicalFileId { get; set; }
        public int BookingPeriodId { get; set; }
        public decimal TrialLength { get; set; }
        public int HearingType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<DateSelection> DateSelections { get; set; }
    }
}
