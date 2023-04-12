using SCJ.Booking.CourtBookingPrototype.Clients;
using SCJ.Booking.CourtBookingPrototype.Enumerations;
using SCJ.Booking.CourtBookingPrototype.Extensions;
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

        private static bool _randomDateSelections = false;
        public static bool RandomDateSelections
        {
            get
            {
                return _randomDateSelections;
            }
            set
            {
                _randomDateSelections = value;
            }
        }

        private static string DemandCSVHeaderString = "Court File number,Hearing Length,First Choice Date,Second Choice Date,Third Choice Date,Fourth Choice Date,Fifth Choice Date";

        private static List<DateSelection> _dateSelections;
        public static List<DateSelection> DateSelections
        {
            get {
                //if dateSelections hasn't been set up yet, we will create all the DateSelections and write all the demand into a csv
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

                            //add headers to file
                            writer.WriteLine(DemandCSVHeaderString);

                            #region August dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustCaseBookingRequests.Where(x => x.TrialLength > 15))
                            {
                                string dates = "";
                                var augustDatesForSixteenPlus = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                                if (RandomDateSelections)
                                    augustDatesForSixteenPlus.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustCaseBookingRequests.Where(x => x.TrialLength >= 6 && x.TrialLength <= 15))
                            {
                                string dates = "";
                                var augustDatesForSixToFifteen = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                                if (RandomDateSelections)
                                    augustDatesForSixToFifteen.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustCaseBookingRequests.Where(x => x.TrialLength == 5))
                            {
                                string dates = "";
                                var augustDatesForFive = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                                if (RandomDateSelections)
                                    augustDatesForFive.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustCaseBookingRequests.Where(x => x.TrialLength == 4))
                            {
                                string dates = "";
                                var augustDatesForFour = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                                if (RandomDateSelections)
                                    augustDatesForFour.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustCaseBookingRequests.Where(x => x.TrialLength == 3))
                            {
                                string dates = "";
                                var augustDatesForThree = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                if (RandomDateSelections)
                                    augustDatesForThree.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustCaseBookingRequests.Where(x => x.TrialLength == 2))
                            {
                                string dates = "";
                                var augustDatesForTwo = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                if (RandomDateSelections)
                                    augustDatesForTwo.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.AugustCaseBookingRequests.Where(x => x.TrialLength == 1))
                            {
                                string dates = "";
                                var augustDatesForOne = AvailabilityDatesFixture.AugustDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                if (RandomDateSelections)
                                    augustDatesForOne.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
                            #endregion

                            #region September dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberCaseBookingRequests.Where(x => x.TrialLength > 15))
                            {
                                string dates = "";
                                var septemberDatesForSixteenPlus = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                                if (RandomDateSelections)
                                    septemberDatesForSixteenPlus.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberCaseBookingRequests.Where(x => x.TrialLength >= 6 && x.TrialLength <= 15))
                            {
                                string dates = "";
                                var septemberDatesForSixToFifteen = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                                if (RandomDateSelections)
                                    septemberDatesForSixToFifteen.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberCaseBookingRequests.Where(x => x.TrialLength == 5))
                            {
                                string dates = "";
                                var septemberDatesForFive = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                                if (RandomDateSelections)
                                    septemberDatesForFive.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberCaseBookingRequests.Where(x => x.TrialLength == 4))
                            {
                                string dates = "";
                                var septemberDatesForFour = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                                if (RandomDateSelections)
                                    septemberDatesForFour.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberCaseBookingRequests.Where(x => x.TrialLength == 3))
                            {
                                string dates = "";
                                var septemberDatesForThree = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                if (RandomDateSelections)
                                    septemberDatesForThree.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberCaseBookingRequests.Where(x => x.TrialLength == 2))
                            {
                                string dates = "";
                                var septemberDatesForTwo = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                if (RandomDateSelections)
                                    septemberDatesForTwo.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.SeptemberCaseBookingRequests.Where(x => x.TrialLength == 1))
                            {
                                string dates = "";
                                var septemberDatesForOne = AvailabilityDatesFixture.SeptemberDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                if (RandomDateSelections)
                                    septemberDatesForOne.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
                            #endregion

                            #region October dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberCaseBookingRequests.Where(x => x.TrialLength > 15))
                            {
                                string dates = "";
                                var octoberDatesForSixteenPlus = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                                if (RandomDateSelections)
                                    octoberDatesForSixteenPlus.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberCaseBookingRequests.Where(x => x.TrialLength >= 6 && x.TrialLength <= 15))
                            {
                                string dates = "";
                                var octoberDatesForSixToFifteen = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                                if (RandomDateSelections)
                                    octoberDatesForSixToFifteen.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberCaseBookingRequests.Where(x => x.TrialLength == 5))
                            {
                                string dates = "";
                                var octoberDatesForFive = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                                if (RandomDateSelections)
                                    octoberDatesForFive.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberCaseBookingRequests.Where(x => x.TrialLength == 4))
                            {
                                string dates = "";
                                var octoberDatesForFour = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                                if (RandomDateSelections)
                                    octoberDatesForFour.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberCaseBookingRequests.Where(x => x.TrialLength == 3))
                            {
                                string dates = "";
                                var octoberDatesForThree = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                if (RandomDateSelections)
                                    octoberDatesForThree.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberCaseBookingRequests.Where(x => x.TrialLength == 2))
                            {
                                string dates = "";
                                var octoberDatesForTwo = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                if (RandomDateSelections)
                                    octoberDatesForTwo.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.OctoberCaseBookingRequests.Where(x => x.TrialLength == 1))
                            {
                                string dates = "";
                                var octoberDatesForOne = AvailabilityDatesFixture.OctoberDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                if (RandomDateSelections)
                                    octoberDatesForOne.Shuffle();

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
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
                            #endregion

                            #region November dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberCaseBookingRequests.Where(x => x.TrialLength > 15))
                            {
                                string dates = "";
                                var novemberDatesForSixteenPlus = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                                if (RandomDateSelections)
                                    novemberDatesForSixteenPlus.Shuffle();

                                for (int x = 0; x < novemberDatesForSixteenPlus.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = novemberDatesForSixteenPlus[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + novemberDatesForSixteenPlus[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberCaseBookingRequests.Where(x => x.TrialLength >= 6 && x.TrialLength <= 15))
                            {
                                string dates = "";
                                var novemberDatesForSixToFifteen = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                                if (RandomDateSelections)
                                    novemberDatesForSixToFifteen.Shuffle();

                                for (int x = 0; x < novemberDatesForSixToFifteen.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = novemberDatesForSixToFifteen[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + novemberDatesForSixToFifteen[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberCaseBookingRequests.Where(x => x.TrialLength == 5))
                            {
                                string dates = "";
                                var novemberDatesForFive = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                                if (RandomDateSelections)
                                    novemberDatesForFive.Shuffle();

                                for (int x = 0; x < novemberDatesForFive.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = novemberDatesForFive[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + novemberDatesForFive[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberCaseBookingRequests.Where(x => x.TrialLength == 4))
                            {
                                string dates = "";
                                var novemberDatesForFour = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                                if (RandomDateSelections)
                                    novemberDatesForFour.Shuffle();

                                for (int x = 0; x < novemberDatesForFour.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = novemberDatesForFour[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + novemberDatesForFour[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberCaseBookingRequests.Where(x => x.TrialLength == 3))
                            {
                                string dates = "";
                                var novemberDatesForThree = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                if (RandomDateSelections)
                                    novemberDatesForThree.Shuffle();

                                for (int x = 0; x < novemberDatesForThree.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = novemberDatesForThree[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + novemberDatesForThree[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberCaseBookingRequests.Where(x => x.TrialLength == 2))
                            {
                                string dates = "";
                                var novemberDatesForTwo = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                if (RandomDateSelections)
                                    novemberDatesForTwo.Shuffle();

                                for (int x = 0; x < novemberDatesForTwo.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = novemberDatesForTwo[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + novemberDatesForTwo[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.NovemberCaseBookingRequests.Where(x => x.TrialLength == 1))
                            {
                                string dates = "";
                                var novemberDatesForOne = AvailabilityDatesFixture.NovemberDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                if (RandomDateSelections)
                                    novemberDatesForOne.Shuffle();

                                for (int x = 0; x < novemberDatesForOne.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = novemberDatesForOne[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + novemberDatesForOne[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
                            #endregion

                            #region December dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberCaseBookingRequests.Where(x => x.TrialLength > 15))
                            {
                                string dates = "";
                                var decemberDatesForSixteenPlus = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                                if (RandomDateSelections)
                                    decemberDatesForSixteenPlus.Shuffle();

                                if(decemberDatesForSixteenPlus != null && decemberDatesForSixteenPlus.Length > 0)
                                {
                                    for (int x = 0; x < decemberDatesForSixteenPlus.Length; x++)
                                    {
                                        _dateSelections.Add(new DateSelection
                                        {
                                            Id = dateSelectionIdCounter++,
                                            CaseBookingRequestId = bookingRequest.Id,
                                            Date = decemberDatesForSixteenPlus[x].Date,
                                            PreferenceOrder = x + 1
                                        });
                                        dates = dates + decemberDatesForSixteenPlus[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                    }

                                    dates = dates.Substring(0, dates.Length - 1);
                                    writer.WriteLine(String.Format(
                                        "{0},{1},{2}",
                                        $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                        bookingRequest.TrialLength,
                                        dates
                                    ));
                                }
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberCaseBookingRequests.Where(x => x.TrialLength >= 6 && x.TrialLength <= 15))
                            {
                                string dates = "";
                                var decemberDatesForSixToFifteen = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                                if (RandomDateSelections)
                                    decemberDatesForSixToFifteen.Shuffle();

                                for (int x = 0; x < decemberDatesForSixToFifteen.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = decemberDatesForSixToFifteen[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + decemberDatesForSixToFifteen[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberCaseBookingRequests.Where(x => x.TrialLength == 5))
                            {
                                string dates = "";
                                var decemberDatesForFive = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                                if (RandomDateSelections)
                                    decemberDatesForFive.Shuffle();

                                for (int x = 0; x < decemberDatesForFive.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = decemberDatesForFive[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + decemberDatesForFive[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberCaseBookingRequests.Where(x => x.TrialLength == 4))
                            {
                                string dates = "";
                                var decemberDatesForFour = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                                if (RandomDateSelections)
                                    decemberDatesForFour.Shuffle();

                                for (int x = 0; x < decemberDatesForFour.Length; x++)

                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = decemberDatesForFour[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + decemberDatesForFour[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberCaseBookingRequests.Where(x => x.TrialLength == 3))
                            {
                                string dates = "";
                                var decemberDatesForThree = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                if (RandomDateSelections)
                                    decemberDatesForThree.Shuffle();

                                for (int x = 0; x < decemberDatesForThree.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = decemberDatesForThree[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + decemberDatesForThree[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberCaseBookingRequests.Where(x => x.TrialLength == 2))
                            {
                                string dates = "";
                                var decemberDatesForTwo = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                if (RandomDateSelections)
                                    decemberDatesForTwo.Shuffle();

                                for (int x = 0; x < decemberDatesForTwo.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = decemberDatesForTwo[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + decemberDatesForTwo[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.DecemberCaseBookingRequests.Where(x => x.TrialLength == 1))
                            {
                                string dates = "";

                                var decemberDatesForOne = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                if (RandomDateSelections)
                                    decemberDatesForOne.Shuffle();

                                for (int x = 0; x < decemberDatesForOne.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = decemberDatesForOne[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + decemberDatesForOne[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
                            #endregion

                            #region January dates
                            foreach (var bookingRequest in CaseBookingRequestsFixture.JanuaryCaseBookingRequests.Where(x => x.TrialLength > 15))
                            {
                                string dates = "";
                                var januaryDatesForSixteenPlus = AvailabilityDatesFixture.JanuaryDates.Where(x => x.TrialType == TrialType.SixteenPlusDay).ToArray();
                                if (RandomDateSelections)
                                    januaryDatesForSixteenPlus.Shuffle();

                                for (int x = 0; x < januaryDatesForSixteenPlus.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = januaryDatesForSixteenPlus[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + januaryDatesForSixteenPlus[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.JanuaryCaseBookingRequests.Where(x => x.TrialLength >= 6 && x.TrialLength <= 15))
                            {
                                string dates = "";
                                var januaryDatesForSixToFifteen = AvailabilityDatesFixture.JanuaryDates.Where(x => x.TrialType == TrialType.SixToFifteenDay).ToArray();
                                if (RandomDateSelections)
                                    januaryDatesForSixToFifteen.Shuffle();

                                for (int x = 0; x < januaryDatesForSixToFifteen.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = januaryDatesForSixToFifteen[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + januaryDatesForSixToFifteen[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.JanuaryCaseBookingRequests.Where(x => x.TrialLength == 5))
                            {
                                string dates = "";
                                var januaryDatesForFive = AvailabilityDatesFixture.JanuaryDates.Where(x => x.TrialType == TrialType.FiveDay).ToArray();
                                if (RandomDateSelections)
                                    januaryDatesForFive.Shuffle();

                                for (int x = 0; x < januaryDatesForFive.Length; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = januaryDatesForFive[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + januaryDatesForFive[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.JanuaryCaseBookingRequests.Where(x => x.TrialLength == 4))
                            {
                                string dates = "";
                                var januaryDatesForFour = AvailabilityDatesFixture.JanuaryDates.Where(x => x.TrialType == TrialType.FourDay).ToArray();
                                if (RandomDateSelections)
                                    januaryDatesForFour.Shuffle();

                                for (int x = 0; x < januaryDatesForFour.Length; x++)

                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = januaryDatesForFour[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + januaryDatesForFour[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.JanuaryCaseBookingRequests.Where(x => x.TrialLength == 3))
                            {
                                string dates = "";
                                var januaryDatesForThree = AvailabilityDatesFixture.JanuaryDates.Where(x => x.TrialType == TrialType.ThreeDay).ToArray();
                                if (RandomDateSelections)
                                    januaryDatesForThree.Shuffle();

                                for (int x = 0; x < januaryDatesForThree.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = januaryDatesForThree[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + januaryDatesForThree[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.JanuaryCaseBookingRequests.Where(x => x.TrialLength == 2))
                            {
                                string dates = "";
                                var januaryDatesForTwo = AvailabilityDatesFixture.JanuaryDates.Where(x => x.TrialType == TrialType.TwoDay).ToArray();
                                if (RandomDateSelections)
                                    januaryDatesForTwo.Shuffle();

                                for (int x = 0; x < januaryDatesForTwo.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = januaryDatesForTwo[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + januaryDatesForTwo[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }

                            foreach (var bookingRequest in CaseBookingRequestsFixture.JanuaryCaseBookingRequests.Where(x => x.TrialLength == 1))
                            {
                                string dates = "";

                                var januaryDatesForOne = AvailabilityDatesFixture.JanuaryDates.Where(x => x.TrialType == TrialType.OneDay).ToArray();
                                if (RandomDateSelections)
                                    januaryDatesForOne.Shuffle();

                                for (int x = 0; x < januaryDatesForOne.Length && x < 5; x++)
                                {
                                    _dateSelections.Add(new DateSelection
                                    {
                                        Id = dateSelectionIdCounter++,
                                        CaseBookingRequestId = bookingRequest.Id,
                                        Date = januaryDatesForOne[x].Date,
                                        PreferenceOrder = x + 1
                                    });
                                    dates = dates + januaryDatesForOne[x].Date.ToString("dd-MMMM-yyyy") + ",";
                                }

                                dates = dates.Substring(0, dates.Length - 1);
                                writer.WriteLine(String.Format(
                                    "{0},{1},{2}",
                                    $"{RegistryFixture.VancouverRegistry.Location} M{bookingRequest.PhysicalFileId.ToString("00000")}",
                                    bookingRequest.TrialLength,
                                    dates
                                ));
                            }
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
                case FakeTrialBookingClient.DecemberMonth:
                    dates = AvailabilityDatesFixture.DecemberDates.Where(x => x.TrialType == trialType).ToArray();
                    break;
                case FakeTrialBookingClient.JanuaryMonth:
                    dates = AvailabilityDatesFixture.JanuaryDates.Where(x => x.TrialType == trialType).ToArray();
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
