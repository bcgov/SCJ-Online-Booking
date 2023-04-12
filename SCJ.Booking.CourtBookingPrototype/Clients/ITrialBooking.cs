using Microsoft.Win32;
using SCJ.Booking.CourtBookingPrototype.Fixtures;
using SCJ.Booking.CourtBookingPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Clients
{
    //will need to refactor outputs of functions to be async
    public interface ITrialBooking
    {
        //hearingType is 9999 until updated by Lorne
        //return array should be an object with date and number of booking slots for that date
        TrialDate[] GetAvailableTrialDates(int registryId, decimal hearingType, string courtClass, decimal hearingLength, int bookingYear = 0, int bookingMonth = 0);

        string BookTrial(int caseId, DateTime date, int registryId, decimal hearingType, decimal hearingLength);

        int UpdateTrialSlotsAvailable(DateTime date, int registryId, decimal hearingType, string courtClass, decimal hearingLength);

        List<List<UnmetDemand>> GetUnmetDemand();

        //return value is a string atm, will be revisited when actual API call has been flushed out more
        string RecordUnmetDemand(int caseBookingRequestId, int bookingPeriodId);

        string RemoveUnmetDemand(int caseBookingRequestId);
    }
}
