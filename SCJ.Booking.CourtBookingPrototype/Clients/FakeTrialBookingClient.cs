using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCJ.Booking.CourtBookingPrototype.Enumerations;
using SCJ.Booking.CourtBookingPrototype.Fixtures;
using SCJ.Booking.CourtBookingPrototype.Models;

namespace SCJ.Booking.CourtBookingPrototype.Clients
{
    public class FakeTrialBookingClient : ITrialBooking
    {
        public const int AugustMonth = 8;
        public const int SeptemberMonth = 9;
        public const int OctoberMonth = 10;
        public const int NovemberMonth = 11;
        public const int DecemberMonth = 12;
        public const int JanuaryMonth = 1;

        public string BookTrial(int caseId, DateTime date, int registryId, decimal hearingType, decimal hearingLength)
        {
            return "success";
        }

        public TrialDate[] GetAvailableTrialDates(int registryId, decimal hearingType, string courtClass, decimal hearingLength, int bookingYear = 0, int bookingMonth = 0)
        {
            return GetFakeTrialDates(bookingMonth);
        }

        //returns all the unmet demand grouped into the number of unmet demand (ie all unmet demand with a count of 2 will be grouped together
        public List<List<UnmetDemand>> GetUnmetDemand()
        {
            var results = new List<List<UnmetDemand>>();

            var unmetDemand = UnmetDemandFixture.UnmetDemand.OrderByDescending(x => x.Count);
            if (unmetDemand.Count() > 0)
            {
                //find the highest unmet demands and decrement downwards to get the order of which to perform the lottery
                var highestUnmetDemand = unmetDemand.First();
                for (int i = highestUnmetDemand.Count; i > 0; i--)
                    results.Add(unmetDemand.Where(x => x.Count == i).ToList());
            }

            return results;
        }

        public string RecordUnmetDemand(int caseBookingRequestId, int bookingPeriodId)
        {
            var matchingUnmetDemand = UnmetDemandFixture.UnmetDemand.Where(x => x.CaseBookingRequestId == caseBookingRequestId).FirstOrDefault();
            if (matchingUnmetDemand != null)
                matchingUnmetDemand.Count++;
            else
                UnmetDemandFixture.UnmetDemand.Add(new UnmetDemand()
                {
                    CaseBookingRequestId = caseBookingRequestId,
                    BookingPeriodId = bookingPeriodId,
                    Count = 1
                });

            return "success";
        }

        public string RemoveUnmetDemand(int unmetDemandId)
        {
            UnmetDemandFixture.UnmetDemand.RemoveAll(x=>x.Id == unmetDemandId);
            return "success";
        }

        public int UpdateTrialSlotsAvailable(DateTime date, int registryId, decimal hearingType, string courtClass, decimal hearingLength)
        {
            throw new NotImplementedException();
        }

        private static TrialDate[] GetFakeTrialDates(int bookingMonth)
        {
            var trialDates = new List<TrialDate>();
            AvailabilityDate[] testDates = new AvailabilityDate[0];
            switch (bookingMonth)
            {
                case AugustMonth:
                    testDates = AvailabilityDatesFixture.AugustDates;
                    break;
                case SeptemberMonth:
                    testDates = AvailabilityDatesFixture.SeptemberDates;
                    break;
                case OctoberMonth:
                    testDates = AvailabilityDatesFixture.OctoberDates;
                    break;
                case NovemberMonth:
                    testDates = AvailabilityDatesFixture.NovemberDates;
                    break;
                case DecemberMonth:
                    testDates = AvailabilityDatesFixture.DecemberDates;
                    break;
                case JanuaryMonth:
                    testDates = AvailabilityDatesFixture.JanuaryDates;
                    break;
            }

            foreach (var date in testDates)
            {
                trialDates.Add(new TrialDate
                {
                    TrialType = date.TrialType,
                    BookingSlotsAvailable = date.NumberOfSlots,
                    InitialBookingSlotsAvailable = date.NumberOfSlots,
                    Date = date.Date
                });
            }

            return trialDates.ToArray();
        }
    }
}
