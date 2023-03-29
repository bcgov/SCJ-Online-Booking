using SCJ.Booking.MVC.Data;
using SCJ.Booking.MVC.Extensions;
using SCJ.Booking.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SCJ.Booking.MVC.Utils;
using SCJ.Booking.MVC.ViewModels;

namespace SCJ.Booking.MVC.Services
{
    public class ScFairUseService : IScFairUseService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IViewRenderService _viewRenderService;

        public ScFairUseService(ApplicationDbContext dbContext, IViewRenderService viewRenderService)
        {
            _dbContext = dbContext;
            _viewRenderService = viewRenderService;
        }

        public void PerformFairUseAlgorithm<T>(int caseRegistryId, int trialRegistryId, decimal hearingType, string bucketLocationId, decimal hearingLength, int bookingYear, int bookingMonth)
        {
            //get all trial date slots from SCSS
            TrialDate[] trialDates = GetAvailableTrialDates(caseRegistryId, hearingType, bucketLocationId, hearingLength, bookingYear, bookingMonth);

            //get all previous unmet demand for the supply group
            var tieredPreviousUnmetDemand = SortUnmetDemandIntoTiers(GetUnmetDemand(bucketLocationId));

            //get all the booking requests
            var caseBookingRequests = _dbContext.CaseBookingRequests.ToList();

            //get all date selections
            var dateSelections = _dbContext.DateSelections.ToList();

            //list of all booking requests that were unable to be booked
            var newUnmetDemand = new List<UnmetDemand>();

            var courtBookingEmails = _dbContext.Set<CourtBookingEmail>();

            #region book unmet demand
            foreach (var unmetDemandTier in tieredPreviousUnmetDemand)
            {
                unmetDemandTier.Shuffle();

                foreach (var unmetDemand in unmetDemandTier)
                {
                    //find the matching booking request for this unmet demand, if any
                    var matchingCaseBookingRequest = caseBookingRequests.FirstOrDefault(x => x.CaseId == unmetDemand.CaseId);
                    if (matchingCaseBookingRequest != null)
                    {
                        //get the ranking and the selected dates
                        int ranking = unmetDemandTier.FindIndex(x => x.CaseId == unmetDemand.CaseId);
                        var choices = dateSelections
                            .Where(x =>
                                x.CaseBookingRequestId == matchingCaseBookingRequest.Id &&
                                x.Date.Month == bookingMonth).OrderBy(x => x.PreferenceOrder)
                            .Select(x => x.Date).ToArray();

                        var result = BookTrial(matchingCaseBookingRequest.CaseId, caseRegistryId, bucketLocationId, hearingType, hearingLength, ranking, choices);
                        if (result == "success")
                        {
                            //remove the booking request from the booking request master list so it isn't booked twice
                            caseBookingRequests.Remove(matchingCaseBookingRequest);

                            //TODO figure out which templates are needed for each hearing type code as well as if new viewmodel required
                            string viewName = $"Email-{ScHearingType.GetHearingType(Convert.ToInt32(hearingType))}.cshtml";
                            EmailViewModel emailViewModel = new EmailViewModel
                            {
                                EmailAddress = matchingCaseBookingRequest.Email,
                                Date = $"{bookingMonth}, {bookingYear}"
                            };
                            var viewResult = _viewRenderService.RenderToStringAsync(viewName, emailViewModel);

                            //create a booking email
                            courtBookingEmails.AddAsync(new CourtBookingEmail
                            {
                                CourtLevel = "SC",
                                ToEmail = matchingCaseBookingRequest.Email,
                                Subject = "",
                                //need to figure out which template is for which body
                            });
                        }
                        else
                            newUnmetDemand.Add(unmetDemand);
                    }
                }
            }
            #endregion

            #region book for normal slots
            //run lottery to determine the order
            caseBookingRequests.Shuffle();

            foreach (var bookingRequest in caseBookingRequests)
            {
                //get the ranking and the selected dates
                int ranking = caseBookingRequests.FindIndex(x => x.CaseId == bookingRequest.CaseId);
                var choices = dateSelections
                    .Where(x =>
                        x.CaseBookingRequestId == bookingRequest.Id &&
                        x.Date.Month == bookingMonth).OrderBy(x => x.PreferenceOrder)
                    .Select(x => x.Date).ToArray();

                var result = BookTrial(bookingRequest.CaseId, caseRegistryId, bucketLocationId, hearingType, hearingLength, ranking, choices);
                if (result != "success"){
                    newUnmetDemand.Add(new UnmetDemand(){
                        CaseId = bookingRequest.Id,
                        BookingPeriodId = bookingRequest.BookingPeriodId,
                        BookingLength = bookingRequest.TrialLength
                    });
                }
            }

            //send new unmet demand to SCSS
            RecordUnmetDemand(newUnmetDemand);

            //save all the court booking emails to our db
            _dbContext.SaveChangesAsync();
            #endregion
        }

        public TrialDate[] GetAvailableTrialDates(int caseRegistryId, decimal hearingType, string bucketLocationId, decimal hearingLength, int bookingYear, int bookingMonth)
        {
            //call to SCSS API then convert JSON to array of TrialDate objects
            throw new NotImplementedException();
        }

        public string BookTrial(decimal caseId, int caseRegistryId, string bucketLocationId, decimal hearingType, decimal hearingLength, int ranking, DateTime[] choices)
        {
            //calls SCSS API to book a trial.  Checks a return value if the booking was successful or not.
            throw new NotImplementedException();
        }

        public List<UnmetDemand> GetUnmetDemand(string bucketLocationId)
        {
            throw new NotImplementedException();
        }

        public UnmetDemand GetUnmetDemandByCaseId(decimal caseId)
        {
            throw new NotImplementedException();
        }

        public List<decimal> RecordUnmetDemand(List<UnmetDemand> unmetDemand)
        {
            throw new NotImplementedException();
        }

        private List<List<UnmetDemand>> SortUnmetDemandIntoTiers(List<UnmetDemand> unsortedUmetDemand)
        {
            var results = new List<List<UnmetDemand>>();

            var unmetDemand = unsortedUmetDemand.OrderByDescending(x => x.Count);
            if (unmetDemand.Count() > 0)
            {
                //find the highest unmet demands and decrement downwards to get the order of which to perform the lottery
                var highestUnmetDemand = unmetDemand.First();
                for (int i = highestUnmetDemand.Count; i > 0; i--)
                    results.Add(unmetDemand.Where(x => x.Count == i).ToList());
            }

            return results;
        }
    }
}
