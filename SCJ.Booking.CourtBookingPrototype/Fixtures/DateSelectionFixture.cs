using SCJ.Booking.CourtBookingPrototype.Clients;
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

                    try
                    {
                        FileStream fileStream = null;
                        string newFilePath = $"{Program.WorkingDirectory}/Outputs/CaseBookingRequest-dates-" + DateTime.Now.ToString("MM-dd-yyyy-H-mm-ss") + ".csv";
                        fileStream = new FileStream(newFilePath, FileMode.OpenOrCreate);

                        using (var writer = new StreamWriter(fileStream))
                        {
                            //create all the dates from the CaseBookingRequests
                            int dateSelectionIdCounter = 1;

                            #region August dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustSixteenPlusDayCaseBookingRequests)
                            {
                                string dates = "";
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
                                    dates = dates + augustDatesForSixteenPlus[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustFifteenToSixDayCaseBookingRequests)
                            {
                                string dates = "";
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
                                    dates = dates + augustDatesForSixToFifteen[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustFiveDayCaseBookingRequests)
                            {
                                string dates = "";
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
                                    dates = dates + augustDatesForFive[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustFourDayCaseBookingRequests)
                            {
                                string dates = "";
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
                                    dates = dates + augustDatesForFour[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustThreeDayCaseBookingRequests)
                            {
                                string dates = "";
                                var augustDatesForThree = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                for (int x = 0; x < augustDatesForThree.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = augustDatesForThree[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + augustDatesForThree[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustTwoDayCaseBookingRequests)
                            {
                                string dates = "";
                                var augustDatesForTwo = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                for (int x = 0; x < augustDatesForTwo.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = augustDatesForTwo[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + augustDatesForTwo[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustOneDayCaseBookingRequests)
                            {
                                string dates = "";
                                var augustDatesForOne = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                for (int x = 0; x < augustDatesForOne.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = augustDatesForOne[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + augustDatesForOne[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
                            #endregion

                            #region September dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberSixteenPlusDayCaseBookingRequests)
                            {
                                string dates = "";
                                var septemberDatesForSixteenPlus = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                                for (int x = 0; x < septemberDatesForSixteenPlus.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = septemberDatesForSixteenPlus[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + septemberDatesForSixteenPlus[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberFifteenToSixDayCaseBookingRequests)
                            {
                                string dates = "";
                                var septemberDatesForSixToFifteen = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                                for (int x = 0; x < septemberDatesForSixToFifteen.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = septemberDatesForSixToFifteen[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + septemberDatesForSixToFifteen[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberFiveDayCaseBookingRequests)
                            {
                                string dates = "";
                                var septemberDatesForFive = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                                for (int x = 0; x < septemberDatesForFive.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = septemberDatesForFive[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + septemberDatesForFive[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberFourDayCaseBookingRequests)
                            {
                                string dates = "";
                                var septemberDatesForFour = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                                for (int x = 0; x < septemberDatesForFour.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = septemberDatesForFour[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + septemberDatesForFour[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberThreeDayCaseBookingRequests)
                            {
                                string dates = "";
                                var septemberDatesForThree = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                for (int x = 0; x < septemberDatesForThree.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = septemberDatesForThree[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + septemberDatesForThree[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberTwoDayCaseBookingRequests)
                            {
                                string dates = "";
                                var septemberDatesForTwo = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                for (int x = 0; x < septemberDatesForTwo.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = septemberDatesForTwo[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + septemberDatesForTwo[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberOneDayCaseBookingRequests)
                            {
                                string dates = "";
                                var septemberDatesForOne = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                for (int x = 0; x < septemberDatesForOne.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = septemberDatesForOne[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + septemberDatesForOne[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
                            #endregion

                            #region October dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberSixteenPlusDayCaseBookingRequests)
                            {
                                string dates = "";
                                var octoberDatesForSixteenPlus = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                                for (int x = 0; x < octoberDatesForSixteenPlus.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = octoberDatesForSixteenPlus[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + octoberDatesForSixteenPlus[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberFifteenToSixDayCaseBookingRequests)
                            {
                                string dates = "";
                                var octoberDatesForSixToFifteen = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                                for (int x = 0; x < octoberDatesForSixToFifteen.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = octoberDatesForSixToFifteen[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + octoberDatesForSixToFifteen[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberFiveDayCaseBookingRequests)
                            {
                                string dates = "";
                                var octoberDatesForFive = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                                for (int x = 0; x < octoberDatesForFive.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = octoberDatesForFive[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + octoberDatesForFive[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberFourDayCaseBookingRequests)
                            {
                                string dates = "";
                                var octoberDatesForFour = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                                for (int x = 0; x < octoberDatesForFour.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = octoberDatesForFour[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + octoberDatesForFour[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberThreeDayCaseBookingRequests)
                            {
                                string dates = "";
                                var octoberDatesForThree = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                for (int x = 0; x < octoberDatesForThree.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = octoberDatesForThree[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + octoberDatesForThree[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberTwoDayCaseBookingRequests)
                            {
                                string dates = "";
                                var octoberDatesForTwo = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                for (int x = 0; x < octoberDatesForTwo.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = octoberDatesForTwo[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + octoberDatesForTwo[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberOneDayCaseBookingRequests)
                            {
                                string dates = "";
                                var octoberDatesForOne = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                for (int x = 0; x < octoberDatesForOne.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = octoberDatesForOne[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + octoberDatesForOne[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    bookingRequest.PhysicalFileId,
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
                            #endregion

                            #region November dates
                            //foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberSixteenPlusDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var novemberDatesForSixteenPlus = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                            //    for (int x = 0; x < novemberDatesForSixteenPlus.Length; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = novemberDatesForSixteenPlus[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + novemberDatesForSixteenPlus[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberFifteenToSixDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var novemberDatesForSixToFifteen = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                            //    for (int x = 0; x < novemberDatesForSixToFifteen.Length; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = novemberDatesForSixToFifteen[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + novemberDatesForSixToFifteen[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberFiveDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var novemberDatesForFive = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                            //    for (int x = 0; x < novemberDatesForFive.Length; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = novemberDatesForFive[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + novemberDatesForFive[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberFourDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var novemberDatesForFour = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                            //    for (int x = 0; x < novemberDatesForFour.Length; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = novemberDatesForFour[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + novemberDatesForFour[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberThreeDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var novemberDatesForThree = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                            //    for (int x = 0; x < novemberDatesForThree.Length && x < 5; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = novemberDatesForThree[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + novemberDatesForThree[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberTwoDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var novemberDatesForTwo = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                            //    for (int x = 0; x < novemberDatesForTwo.Length && x < 5; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = novemberDatesForTwo[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + novemberDatesForTwo[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberOneDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var novemberDatesForOne = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                            //    for (int x = 0; x < novemberDatesForOne.Length && x < 5; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = novemberDatesForOne[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + novemberDatesForOne[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}
                            #endregion

                            #region December dates
                            //foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberSixteenPlusDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var decemberDatesForSixteenPlus = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                            //    for (int x = 0; x < decemberDatesForSixteenPlus.Length; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = decemberDatesForSixteenPlus[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + decemberDatesForSixteenPlus[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberFifteenToSixDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var decemberDatesForSixToFifteen = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                            //    for (int x = 0; x < decemberDatesForSixToFifteen.Length; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = decemberDatesForSixToFifteen[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + decemberDatesForSixToFifteen[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberFiveDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var decemberDatesForFive = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                            //    for (int x = 0; x < decemberDatesForFive.Length; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = decemberDatesForFive[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + decemberDatesForFive[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberFourDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var decemberDatesForFour = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                            //    for (int x = 0; x < decemberDatesForFour.Length; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = decemberDatesForFour[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + decemberDatesForFour[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberThreeDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var decemberDatesForThree = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                            //    for (int x = 0; x < decemberDatesForThree.Length && x < 5; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = decemberDatesForThree[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + decemberDatesForThree[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberTwoDayCaseBookingRequests)
                            //{
                            //    string dates = "";
                            //    var decemberDatesForTwo = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                            //    for (int x = 0; x < decemberDatesForTwo.Length && x < 5; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = decemberDatesForTwo[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + decemberDatesForTwo[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length - 1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}

                            //foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberOneDayCaseBookingRequests)
                            //{
                            //    string dates = "";

                            //    var decemberDatesForOne = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                            //    for (int x = 0; x < decemberDatesForOne.Length && x < 5; x++)
                            //    {
                            //        _dateSelections.Add(new DateSelection
                            //        {
                            //            Id = dateSelectionIdCounter++,
                            //            CaseBookingRequestId = bookingRequest.Id,
                            //            Date = decemberDatesForOne[x].Date,
                            //            PreferenceOrder = x + 1
                            //        });
                            //        dates = dates + decemberDatesForOne[x].Date.ToString("dd-MMMM-yyyy") + ",";
                            //    }

                            //    dates = dates.Substring(0, dates.Length-1);
                            //    writer.WriteLine(String.Format(
                            //        "{0},{1},{2}",
                            //        bookingRequest.PhysicalFileId,
                            //        bookingRequest.TrialLength,
                            //        dates
                            //    ));
                            //}
                            #endregion
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

                return _dateSelections;
            }
        }

        public static void AddUnmetDemandDateSelectionsForNextMonth(int caseBookingRequestId, TrialType trialType, int month)
        {
            AvailabilityDate[] dates = { };
            switch (month)
            {
                case FakeTrialBookingClient.AugustMonth:
                    dates = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == trialType).ToArray();
                    break;
                case FakeTrialBookingClient.SeptemberMonth:
                    dates = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == trialType).ToArray();
                    break;
                case FakeTrialBookingClient.OctoberMonth:
                    dates = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == trialType).ToArray();
                    break;
                case FakeTrialBookingClient.NovemberMonth:
                    dates = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == trialType).ToArray();
                    break;
            }

            for (int x = 0; x < dates.Length && x < 5; x++)
            {
                _dateSelections.Add(new DateSelection
                {
                    Id = 1,
                    CaseBookingRequestId = caseBookingRequestId,
                    Date = dates[x].Date,
                    PreferenceOrder = x + 1
                });
            }
            
        }
    }
}
