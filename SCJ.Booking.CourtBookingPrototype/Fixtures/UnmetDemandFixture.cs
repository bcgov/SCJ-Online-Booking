using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public static class UnmetDemandFixture
    {
        private static List<UnmetDemand> _unmetDemand { get; set; }
        public static List<UnmetDemand> UnmetDemand {
            get
            {
                if (_unmetDemand == null)
                    _unmetDemand = new List<UnmetDemand>();

                return _unmetDemand;
            }
        }
    }

    public class UnmetDemand
    {
        public int Id { get; set; }
        public int CaseBookingRequestId { get; set; }
        public int BookingPeriodId { get; set; }
        public int Count { get; set; }
    }
}
