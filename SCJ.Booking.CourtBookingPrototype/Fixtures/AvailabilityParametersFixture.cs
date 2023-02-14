using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public static class AvailabilityParametersFixture
    {
        public static AvailabilityParameter AugustParameters = new AvailabilityParameter
        {
            Id = 1,
            RegistryId = 1,
            HearingType = 9999,
            CourtClass = "M",
            HearingLength = 1
        };

        public static AvailabilityParameter SeptemberParameters = new AvailabilityParameter
        {
            Id = 2,
            RegistryId = 1,
            HearingType = 9999,
            CourtClass = "M",
            HearingLength = 1
        };
    }

    public class AvailabilityParameter
    {
        public int Id { get; set; }
        public int RegistryId { get; set; }
        public decimal HearingType { get; set; }
        public string CourtClass { get; set; }
        public string CourtClassName { get; set; }
        public decimal HearingLength { get; set; }
    }
}
