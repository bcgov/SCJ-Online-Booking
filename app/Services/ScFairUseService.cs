using SCJ.Booking.MVC.Data;
using SCJ.Booking.MVC.Extensions;
using SCJ.Booking.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SCJ.Booking.MVC.Enumerations;
using Microsoft.Graph;

namespace SCJ.Booking.MVC.Services
{
    public class ScFairUseService : IScFairUseService
    {
        private readonly ApplicationDbContext _dbContext;

        public ScFairUseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void PerformFairUseAlgorithm<T>(int caseRegistryId, int trialRegistryId, decimal hearingType, string courtClass, decimal hearingLength, int bookingYear, int bookingMonth)
        {
            //get all trial date slots from SCSS
            TrialDate[] trialDates = GetAvailableTrialDates(caseRegistryId, hearingType, courtClass, hearingLength, bookingYear, bookingMonth);

            //get all previous unmet demand for the supply group
            List<List<UnmetDemand>> previousUnmetDemand = GetUnmetDemand(courtClass, caseRegistryId);

            //get all the booking requests
            var caseBookingRequests = _dbContext.CaseBookingRequests.ToList();

            //get all date selections
            var dateSelections = _dbContext.DateSelections.ToList();

            //list of all booking requests that were unable to be booked
            var newUnmetDemand = new List<UnmetDemand>();

            //list of all unmet demand that was met
            var completedUnmetDemand = new List<decimal>();

            #region book unmet demand
            foreach (var unmetDemandTier in previousUnmetDemand)
            {
                unmetDemandTier.Shuffle();

                foreach (var unmetDemand in unmetDemandTier)
                {
                    var matchingCaseBookingRequest = caseBookingRequests.Where(x => x.CaseId == unmetDemand.CaseId).FirstOrDefault();
                    if (matchingCaseBookingRequest != null)
                    {
                        //set a flag to indicate if we could create a booking for any of the date selections
                        bool successfulBooking = false;

                        //figure out what type of trial the booking request is asking for
                        TrialType trialType = TrialType.SixteenPlusDay;
                        switch (matchingCaseBookingRequest.TrialLength)
                        {
                            case 1:
                                trialType = TrialType.OneDay;
                                break;
                            case 2:
                                trialType = TrialType.TwoDay;
                                break;
                            case 3:
                                trialType = TrialType.ThreeDay;
                                break;
                            case 4:
                                trialType = TrialType.FourDay;
                                break;
                            case 5:
                                trialType = TrialType.FiveDay;
                                break;
                            case decimal n when (n > 5 && n < 16):
                                trialType = TrialType.SixToFifteenDay;
                                break;
                        }

                        //get all the selected dates of this booking request and run through them to try and book a date
                        var matchingDateSelections = dateSelections.Where(x => x.CaseBookingRequestId == matchingCaseBookingRequest.Id && x.Date.Month == bookingMonth).OrderBy(x => x.PreferenceOrder);

                        foreach (var dateSelection in matchingDateSelections)
                        {
                            //check if date has availability and try to book date
                            var matchingTrialDate = trialDates.Where(x => x.Date == dateSelection.Date && x.TrialType == trialType).FirstOrDefault();
                            if (matchingTrialDate != null && matchingTrialDate.BookingSlotsAvailable > 0)
                            {
                                var result = BookTrial(matchingCaseBookingRequest.CaseId, dateSelection.Date, caseRegistryId, trialRegistryId, hearingType, courtClass, hearingLength);
                                if (result == "success")    
                                {
                                    successfulBooking = true;
                                    caseBookingRequests.Remove(matchingCaseBookingRequest);
                                    break;
                                }
                            }
                        }
                        
                        if (!successfulBooking)
                            newUnmetDemand.Add(unmetDemand);
                        else
                            completedUnmetDemand.Add(unmetDemand.CaseId);
                    }
                }
            }

            //send the list of completed unmet demand to SCSS to be deleted
            RemoveUnmetDemand(completedUnmetDemand);
            #endregion

            #region book for normal slots
            //run lottery to determine the order
            caseBookingRequests.Shuffle();

            foreach (var bookingRequest in caseBookingRequests)
            {
                //set a flag to indicate if we could create a booking for any of the date selections
                bool successfulBooking = false;

                //figure out what type of trial the booking request is asking for
                TrialType trialType = TrialType.SixteenPlusDay;
                switch (bookingRequest.TrialLength)
                {
                    case 1:
                        trialType = TrialType.OneDay;
                        break;
                    case 2:
                        trialType = TrialType.TwoDay;
                        break;
                    case 3:
                        trialType = TrialType.ThreeDay;
                        break;
                    case 4:
                        trialType = TrialType.FourDay;
                        break;
                    case 5:
                        trialType = TrialType.FiveDay;
                        break;
                    case decimal n when (n > 5 && n < 16):
                        trialType = TrialType.SixToFifteenDay;
                        break;
                }

                //get all the selected dates of this booking request and run through them to try and book a date
                var matchingDateSelections = dateSelections.Where(x => x.CaseBookingRequestId == bookingRequest.Id && x.Date.Month == bookingMonth).OrderBy(x => x.PreferenceOrder);
                foreach (var dateSelection in matchingDateSelections)
                {
                    //check if date has availability and try to book date
                    var matchingTrialDate = trialDates.Where(x => x.Date == dateSelection.Date && x.TrialType == trialType).FirstOrDefault();
                    if (matchingTrialDate != null && matchingTrialDate.BookingSlotsAvailable > 0)
                    {
                        var result = BookTrial(bookingRequest.Id, dateSelection.Date, caseRegistryId, trialRegistryId, hearingType, courtClass, hearingLength);
                        if (result == "success")    //will always be able to successfully book since there is no API call atm
                        {
                            successfulBooking = true;
                            break;
                        }
                    }
                }
                
                if (!successfulBooking)
                {
                    newUnmetDemand.Add(new UnmetDemand(){
                        CaseId = bookingRequest.Id,
                        BookingPeriodId = bookingRequest.BookingPeriodId,
                        BookingLength = bookingRequest.TrialLength
                    });
                }
            }

            //send new unmet demand to SCSS
            RecordUnmetDemand(newUnmetDemand);
            #endregion
        }

        public TrialDate[] GetAvailableTrialDates(int registryId, decimal hearingType, string courtClass, decimal hearingLength, int bookingYear, int bookingMonth)
        {
            //call to SCSS API then convert JSON to array of TrialDate objects
            throw new NotImplementedException();
        }

        public string BookTrial(decimal caseId, DateTime date, int caseRegistryId, int trialRegistryId, decimal hearingType, string courtClass, decimal hearingLength)
        {
            //calls SCSS API to book a trial.  Checks a return value if the booking was successful or not.
            throw new NotImplementedException();
        }

        public List<List<UnmetDemand>> GetUnmetDemand(string courtClass, int caseRegistryId)
        {
            var results = new List<List<UnmetDemand>>();

            //API call to SCSS gets a unsorted list of unmet demand

            return results;
        }

        public List<decimal> RecordUnmetDemand(List<UnmetDemand> unmetDemand)
        {
            throw new NotImplementedException();
        }

        public List<decimal> RemoveUnmetDemand(List<decimal> metDemand)
        {
            throw new NotImplementedException();
        }
    }
}
