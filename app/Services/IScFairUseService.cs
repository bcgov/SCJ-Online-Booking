using SCJ.Booking.MVC.Models;
using System;
using System.Collections.Generic;

namespace SCJ.Booking.MVC.Services
{
    public interface IScFairUseService
    {
        TrialDate[] GetAvailableTrialDates(int registryId, decimal hearingType, string courtClass, decimal hearingLength, int bookingYear, int bookingMonth);

        string BookTrial(decimal caseId, DateTime date, int caseRegistryId, int trialRegistryId, decimal hearingType, string courtClass, decimal hearingLength);

        List<List<UnmetDemand>> GetUnmetDemand(string courtClass, int caseRegistryId);

        //return value is a string atm, will be revisited when actual API call has been flushed out more
        List<decimal> RecordUnmetDemand(List<UnmetDemand> unmetDemand);

        List<decimal> RemoveUnmetDemand(List<decimal> metDemand);
    }
}
