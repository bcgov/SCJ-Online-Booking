using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCJ.Booking.CourtBookingPrototype.Models;

namespace SCJ.Booking.CourtBookingPrototype.Clients
{
    public class FakeTrialBookingClient : ITrialBooking
    {
        public async Task<string> BookTrial(decimal caseId, DateTime date, int registryId, decimal hearingType, decimal hearingLength)
        {
            throw new NotImplementedException();
        }

        public async Task<TrialDate[]> GetAvailableTrialDates(int registryId, decimal hearingType, string courtClass, decimal hearingLength)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetUnmetDemand(decimal caseId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RecordUnmetDemand(decimal caseId, int bookingPeriodId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateTrialSlotsAvailable(DateTime date, int registryId, decimal hearingType, string courtClass, decimal hearingLength)
        {
            throw new NotImplementedException();
        }
    }
}
