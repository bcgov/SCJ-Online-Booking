using Microsoft.CodeAnalysis.Operations;
using SCJ.Booking.MVC.Models;
using System;
using System.Collections.Generic;

namespace SCJ.Booking.MVC.Services
{
    public interface IScFairUseService
    {
        TrialDate[] GetAvailableTrialDates(int caseRegistryId, decimal hearingType, string bucketLocationId, decimal hearingLength, int bookingYear, int bookingMonth);

        string BookTrial(decimal caseId, int caseRegistryId, string bucketLocationId, decimal hearingType,  decimal hearingLength, int ranking, int firstChoiceMonth, int firstChoiceYear, int secondChoiceMonth, int secondChoiceYear, int thirdChoiceMonth, int thirdChoiceYear, int fourthChoiceMonth, int fourthChoiceYear, int fifthChoiceMonth, int fifthChoiceYear);
        
        List<UnmetDemand> GetUnmetDemand(string bucketLocationId);

        UnmetDemand GetUnmetDemandByCaseId(decimal caseId);

        //return value is a string atm, will be revisited when actual API call has been flushed out more
        List<decimal> RecordUnmetDemand(List<UnmetDemand> unmetDemand);

        //List<decimal> RemoveUnmetDemand(List<decimal> metDemand);
    }
}
