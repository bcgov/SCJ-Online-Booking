using SCJ.Booking.CourtBookingPrototype.Enumerations;
using SCJ.Booking.CourtBookingPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public class DateSelectionFixture
    {
        //the average amount of bookings for each trial length based on data provided by Lorne
        #region Default Booking Numbers
        private static int DefaultNumberOfSixteenPlusDayBookings = 4;
        private static int DefaultNumberOfFifteenToSixDayBookings = 12;
        private static int DefaultNumberOfFiveDayBookings = 141;
        private static int DefaultNumberOfFourDayBookings = 5;
        private static int DefaultNumberOfThreeDayBookings = 6;
        private static int DefaultNumberOfTwoDayBookings = 1;
        private static int DefaultNumberOfOneDayBookings = 1;
        #endregion

        private static List<DateSelection> _dateSelections;
        public static List<DateSelection> DateSelections
        {
            get {
                if(_dateSelections == null || _dateSelections.Count <= 0)
                {
                    _dateSelections = new List<DateSelection>();

                    //create all the dates from the CaseBookingRequests
                    int dateSelectionIdCounter = 1;

                    #region August dates
                    foreach (var bookingRequest in CaseBookingRequestsFixture.AugustSixteenPlusDayCaseBookingRequests)
                    {
                        var augustDatesForSixteenPlus = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                        for (int x = 0; x < augustDatesForSixteenPlus.Length; x++)
                        {
                            _dateSelections.Add(new DateSelection
                            {
                                Id = dateSelectionIdCounter++,
                                CaseBookingRequestId = bookingRequest.Id,
                                Date = augustDatesForSixteenPlus[x].Date,
                                PreferenceOrder = x + 1
                            });
                        }
                    }
                    
                    foreach (var bookingRequest in CaseBookingRequestsFixture.AugustFifteenToSixDayCaseBookingRequests)
                    {
                        var augustDatesForSixToFifteen = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                        for (int x = 0; x < augustDatesForSixToFifteen.Length; x++)
                        {
                            _dateSelections.Add(new DateSelection
                            {
                                Id = dateSelectionIdCounter++,
                                CaseBookingRequestId = bookingRequest.Id,
                                Date = augustDatesForSixToFifteen[x].Date,
                                PreferenceOrder = x + 1
                            });
                        }
                    }
                    
                    foreach (var bookingRequest in CaseBookingRequestsFixture.AugustFiveDayCaseBookingRequests)
                    {
                        var augustDatesForFive = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                        for (int x = 0; x < augustDatesForFive.Length; x++)
                        {
                            _dateSelections.Add(new DateSelection
                            {
                                Id = dateSelectionIdCounter++,
                                CaseBookingRequestId = bookingRequest.Id,
                                Date = augustDatesForFive[x].Date,
                                PreferenceOrder = x + 1
                            });
                        }
                    }
                    
                    foreach (var bookingRequest in CaseBookingRequestsFixture.AugustFourDayCaseBookingRequests)
                    {
                        var augustDatesForFour = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                        for (int x = 0; x < augustDatesForFour.Length; x++)
                        {
                            _dateSelections.Add(new DateSelection
                            {
                                Id = dateSelectionIdCounter++,
                                CaseBookingRequestId = bookingRequest.Id,
                                Date = augustDatesForFour[x].Date,
                                PreferenceOrder = x + 1
                            });
                        }
                    }
                    
                    foreach (var bookingRequest in CaseBookingRequestsFixture.AugustThreeDayCaseBookingRequests)
                    {
                        var augustDatesForThree = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                        for (int x = 0; x < augustDatesForThree.Length; x++)
                        {
                            _dateSelections.Add(new DateSelection
                            {
                                Id = dateSelectionIdCounter++,
                                CaseBookingRequestId = bookingRequest.Id,
                                Date = augustDatesForThree[x].Date,
                                PreferenceOrder = x + 1
                            });
                        }
                    }

                    foreach (var bookingRequest in CaseBookingRequestsFixture.AugustTwoDayCaseBookingRequests)
                    {
                        var augustDatesForTwo = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                        for (int x = 0; x < augustDatesForTwo.Length; x++)
                        {
                            _dateSelections.Add(new DateSelection
                            {
                                Id = dateSelectionIdCounter++,
                                CaseBookingRequestId = bookingRequest.Id,
                                Date = augustDatesForTwo[x].Date,
                                PreferenceOrder = x + 1
                            });
                        }
                    }

                    foreach (var bookingRequest in CaseBookingRequestsFixture.AugustOneDayCaseBookingRequests)
                    {
                        var augustDatesForOne = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                        for (int x = 0; x < augustDatesForOne.Length; x++)
                        {
                            _dateSelections.Add(new DateSelection
                            {
                                Id = dateSelectionIdCounter++,
                                CaseBookingRequestId = bookingRequest.Id,
                                Date = augustDatesForOne[x].Date,
                                PreferenceOrder = x + 1
                            });
                        }
                    }
                    #endregion


                }

                return _dateSelections;
            }
        }
    }
}
