using SCJ.Booking.CourtBookingPrototype.Clients;
using SCJ.Booking.CourtBookingPrototype.Enumerations;
using SCJ.Booking.CourtBookingPrototype.Extensions;
using SCJ.Booking.CourtBookingPrototype.Fixtures;
using SCJ.Booking.CourtBookingPrototype.Models;
using SCJ.Booking.MVC.Utils;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SCJ.Booking.CourtBookingPrototype
{
    public class Program
    {
        private static string LotteryCSVHeader = "Court File number,Unmet demand (months),Lottery ranking,Hearing Length (days),Registry ID,Court Class,First Choice Date,Second Choice Date,Third Choice Date,Fourth Choice Date,Fifth Choice Date,Date booked,New unmet demand (months)";
        public static string WorkingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string LotteryMonthSummaryCSVHeader = "16+,,,,15-6,,,,5,,,,4,,,,3,,,,2,,,,1,,,";
        private static string LotteryMonthSummaryCSVSubheader = "Dates,Remaining,Booked,,Dates,Remaining,Booked,,Dates,Remaining,Booked,,Dates,Remaining,Booked,,Dates,Remaining,Booked,,Dates,Remaining,Booked,,Dates,Remaining,Booked,";

        public static decimal DefaultDemandSupplyRatio = 1.25m;

        //average amount of total bookings per booking period based on data provided by Lorne
        public static int DefaultNumberOfBookingsPerBookingPeriod = 167;

        private static FakeTrialBookingClient _client;
        public static FakeTrialBookingClient Client
        {
            get
            {
                if (_client == null)
                    _client = new FakeTrialBookingClient();

                return _client;
            }
        }

        //Date selections for each CaseBookingRequest is generated in DateSelectionFixture and written to the Demand CSV on startup
        public static List<DateSelection> DateSelections = DateSelectionFixture.DateSelections;

        //storage of all CaseBookingRequests
        public static List<CaseBookingRequest> CurrentBookingMonthRequests;
        public static List<CaseBookingRequest> _bookingRequests;
        public static List<CaseBookingRequest> BookingRequests
        {
            get
            {
                if (_bookingRequests == null || _bookingRequests.Count <= 0)
                {
                    _bookingRequests = new List<CaseBookingRequest>();
                    _bookingRequests.AddRange(CaseBookingRequestsFixture.AugustCaseBookingRequests);
                    _bookingRequests.AddRange(CaseBookingRequestsFixture.SeptemberCaseBookingRequests);
                    _bookingRequests.AddRange(CaseBookingRequestsFixture.OctoberCaseBookingRequests);
                    _bookingRequests.AddRange(CaseBookingRequestsFixture.NovemberCaseBookingRequests);
                    _bookingRequests.AddRange(CaseBookingRequestsFixture.DecemberCaseBookingRequests);
                }

                return _bookingRequests;
            }
        }

        public static void Main(string[] args)
        {
            #region parse arguments
            //set the different default settings based on the number of arguments provided
            switch (args.Length)
            {
                case 1:
                    DefaultDemandSupplyRatio = decimal.Parse(args[0]);
                    break;
                case 2:
                    DefaultDemandSupplyRatio = decimal.Parse(args[0]);
                    RegistrySettingsFixture.DefaultNumberOfPicksPerUser = int.Parse(args[1]);
                    break;
                case 3:
                    DefaultDemandSupplyRatio = decimal.Parse(args[0]);
                    RegistrySettingsFixture.DefaultNumberOfPicksPerUser = int.Parse(args[1]);
                    RegistrySettingsFixture.DefaultUsesLottery = bool.Parse(args[2]);
                    break;
            }
            #endregion

            #region connect to db
            //var builder = new ConfigurationBuilder()
            //            .SetBasePath(Directory.GetCurrentDirectory())
            //            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //IConfiguration _configuration = builder.Build();

            //using (var context = new SCJ_CourtBookingContext(_configuration))
            //{
            //}
            #endregion

            #region Algorithm Runner
            var client = new FakeTrialBookingClient();

            AvailabilityDatesFixture.CreateSupplyCSV();

            //run lottery simulation for August
            var augustAvailabilityParameters = AvailabilityParametersFixture.AugustParameters;
            RunLotterySimulation(augustAvailabilityParameters.RegistryId, augustAvailabilityParameters.HearingType, augustAvailabilityParameters.CourtClass, augustAvailabilityParameters.HearingLength, 2024, FakeTrialBookingClient.AugustMonth);

            //run lottery simulation for September
            var septemberAvailabilityParameters = AvailabilityParametersFixture.SeptemberParameters;
            RunLotterySimulation(septemberAvailabilityParameters.RegistryId, septemberAvailabilityParameters.HearingType, septemberAvailabilityParameters.CourtClass, septemberAvailabilityParameters.HearingLength, 2024, FakeTrialBookingClient.SeptemberMonth);

            //run lottery simulation for October
            var octoberAvailabilityParameters = AvailabilityParametersFixture.OctoberParameters;
            RunLotterySimulation(octoberAvailabilityParameters.RegistryId, octoberAvailabilityParameters.HearingType, octoberAvailabilityParameters.CourtClass, octoberAvailabilityParameters.HearingLength, 2024, FakeTrialBookingClient.OctoberMonth);

            //run lottery simulation for November
            var novemberAvailabilityParameters = AvailabilityParametersFixture.NovemberParameters;
            RunLotterySimulation(novemberAvailabilityParameters.RegistryId, novemberAvailabilityParameters.HearingType, novemberAvailabilityParameters.CourtClass, novemberAvailabilityParameters.HearingLength, 2024, FakeTrialBookingClient.NovemberMonth);

            //run lottery simulation for December
            var decemberAvailabilityParameters = AvailabilityParametersFixture.DecemberParameters;
            RunLotterySimulation(decemberAvailabilityParameters.RegistryId, decemberAvailabilityParameters.HearingType, decemberAvailabilityParameters.CourtClass, decemberAvailabilityParameters.HearingLength, 2024, FakeTrialBookingClient.DecemberMonth);

            //run lottery simulation for January
            var januaryAvailabilityParameters = AvailabilityParametersFixture.JanuaryParameters;
            RunLotterySimulation(januaryAvailabilityParameters.RegistryId, januaryAvailabilityParameters.HearingType, januaryAvailabilityParameters.CourtClass, januaryAvailabilityParameters.HearingLength, 2025, FakeTrialBookingClient.JanuaryMonth);

            #endregion
        }

        private static void RunLotterySimulation(int registryId, decimal hearingType, string courtClass, decimal hearingLength, int bookingYear, int bookingMonth)
        {
            DateTime currentTime = DateTime.Now;
            //get all available dates for the booking period
            TrialDate[] trialDates = Client.GetAvailableTrialDates(registryId, hearingType, courtClass, hearingLength, bookingYear, bookingMonth);

            //set up lottery csv 
            FileStream fileStream = null;
            string newFilePath = $"{WorkingDirectory}/Outputs/{bookingMonth}-{bookingYear}-Booking-Schedule-" + currentTime.ToString("MM-dd-yyyy-H-mm-ss") + ".csv";
            try
            {
                fileStream = new FileStream(newFilePath, FileMode.OpenOrCreate);

                using (var writer = new StreamWriter(fileStream))
                {
                    //write the header to the file
                    writer.WriteLine(LotteryCSVHeader);

                    //get all the demand for the current booking period
                    if (bookingMonth == FakeTrialBookingClient.AugustMonth)
                        CurrentBookingMonthRequests = CaseBookingRequestsFixture.AugustCaseBookingRequests;
                    else if (bookingMonth == FakeTrialBookingClient.SeptemberMonth)
                        CurrentBookingMonthRequests = CaseBookingRequestsFixture.SeptemberCaseBookingRequests;
                    else if (bookingMonth == FakeTrialBookingClient.OctoberMonth)
                        CurrentBookingMonthRequests = CaseBookingRequestsFixture.OctoberCaseBookingRequests;
                    else if (bookingMonth == FakeTrialBookingClient.NovemberMonth)
                        CurrentBookingMonthRequests = CaseBookingRequestsFixture.NovemberCaseBookingRequests;
                    else if (bookingMonth == FakeTrialBookingClient.DecemberMonth)
                        CurrentBookingMonthRequests = CaseBookingRequestsFixture.DecemberCaseBookingRequests;
                    else if (bookingMonth == FakeTrialBookingClient.JanuaryMonth)
                        CurrentBookingMonthRequests = CaseBookingRequestsFixture.JanuaryCaseBookingRequests;

                    #region book unmet demand
                    //get all unmet demand so we book those first
                    List<List<UnmetDemand>> previousUnmetDemand = Client.GetUnmetDemand();

                    //try to create booking for unmet demand
                    foreach (var unmetDemandTier in previousUnmetDemand)
                    {
                        //run lottery to determine the order
                        int unmetDemandLotteryRanking = 1;
                        unmetDemandTier.Shuffle();
                        foreach (var unmetDemand in unmetDemandTier)
                        {
                            var matchingCaseBookingRequest = BookingRequests.Where(x => x.Id == unmetDemand.CaseBookingRequestId).FirstOrDefault();
                            if (matchingCaseBookingRequest != null)
                            {
                                var courtFileNumber = $"{RegistryFixture.VancouverRegistry.Location} {courtClass}{matchingCaseBookingRequest.PhysicalFileId.ToString("00000")}";

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
                                var matchingDateSelections = DateSelections.Where(x => x.CaseBookingRequestId == matchingCaseBookingRequest.Id && x.Date.Month == bookingMonth).OrderBy(x => x.PreferenceOrder);

                                foreach (var dateSelection in matchingDateSelections)
                                {
                                    //check if date has availability and try to book date
                                    var matchingTrialDate = trialDates.Where(x => x.Date == dateSelection.Date && x.TrialType == trialType).FirstOrDefault();
                                    if (matchingTrialDate != null && matchingTrialDate.BookingSlotsAvailable > 0)
                                    {
                                        var result = Client.BookTrial(matchingCaseBookingRequest.Id, dateSelection.Date, registryId, hearingType, hearingLength);
                                        if (result == "success")    //will always be able to successfully book since there is no API call atm
                                        {
                                            successfulBooking = true;
                                            DecrementAvailabilityDates(matchingCaseBookingRequest.TrialLength, ref trialDates, dateSelection.Date, trialType);

                                            //write booking to csv
                                            var firstDateSelection = matchingDateSelections.Take(1).FirstOrDefault();
                                            var secondDateSelection = matchingDateSelections.Skip(1).Take(1).FirstOrDefault();
                                            var thirdDateSelection = matchingDateSelections.Skip(2).Take(1).FirstOrDefault();
                                            var fourthDateSelection = matchingDateSelections.Skip(3).Take(1).FirstOrDefault();
                                            var fifthDateSelection = matchingDateSelections.Skip(4).Take(1).FirstOrDefault();

                                            var newLine = string.Format(
                                                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                                                courtFileNumber,
                                                unmetDemand.Count,
                                                unmetDemandLotteryRanking++,
                                                matchingCaseBookingRequest.TrialLength,
                                                RegistryFixture.VancouverRegistry.Location,
                                                string.Format("{0} - Motor Vehicle Accidents", courtClass),
                                                firstDateSelection != null ? firstDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                                secondDateSelection != null ? secondDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                                thirdDateSelection != null ? thirdDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                                fourthDateSelection != null ? fourthDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                                fifthDateSelection != null ? fifthDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                                dateSelection.Date.ToString("dd-MMMM-yyyy"),
                                                0
                                            );

                                            writer.WriteLine(newLine);
                                            break;
                                        }
                                    }
                                }

                                //if we were unable to successfully book something, we increment the unmetDemand count and add it to the remaining demand
                                //to be passed on
                                if (!successfulBooking)
                                {
                                    //we need to also generate new dates for this new unmet demand for next month
                                    int newBookingMonth = bookingMonth + 1;
                                    if (newBookingMonth > 12)
                                        newBookingMonth = 1;

                                    DateSelectionFixture.AddUnmetDemandDateSelectionsForNextMonth(matchingCaseBookingRequest.Id, trialType, newBookingMonth);

                                    //write booking to csv
                                    var firstDateSelection = matchingDateSelections.Take(1).FirstOrDefault();
                                    var secondDateSelection = matchingDateSelections.Skip(1).Take(1).FirstOrDefault();
                                    var thirdDateSelection = matchingDateSelections.Skip(2).Take(1).FirstOrDefault();
                                    var fourthDateSelection = matchingDateSelections.Skip(3).Take(1).FirstOrDefault();
                                    var fifthDateSelection = matchingDateSelections.Skip(4).Take(1).FirstOrDefault();
                                    int increasedDemand = unmetDemand.Count + 1;
                                    var newLine = string.Format(
                                        "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                                        courtFileNumber,
                                        unmetDemand.Count,
                                        unmetDemandLotteryRanking++,
                                        matchingCaseBookingRequest.TrialLength,
                                        RegistryFixture.VancouverRegistry.Location,
                                        string.Format("{0} - Motor Vehicle Accidents", courtClass),
                                        firstDateSelection != null ? firstDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        secondDateSelection != null ? secondDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        thirdDateSelection != null ? thirdDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        fourthDateSelection != null ? fourthDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        fifthDateSelection != null ? fifthDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        "Not Booked",
                                        increasedDemand
                                    );
                                    writer.WriteLine(newLine);

                                    Client.RecordUnmetDemand(matchingCaseBookingRequest.Id, matchingCaseBookingRequest.BookingPeriodId);
                                }
                                else    //remove this case booking from the master list of booking requests as we were able to create a successful booking
                                {
                                    BookingRequests.Remove(matchingCaseBookingRequest);
                                    Client.RemoveUnmetDemand(unmetDemand.Id);
                                }
                            }
                        }
                    }
                    #endregion

                    #region book for normal slots
                    //run lottery to determine the order
                    CurrentBookingMonthRequests.Shuffle();

                    //get the number of unmet demand to reduce from the current month's requests
                    int totalUnmetDemandCount = previousUnmetDemand.SelectMany(x => x).Count();
                    int newAmountOfDemand = CurrentBookingMonthRequests.Count - totalUnmetDemandCount;

                    int lotteryRanking = 1;
                    foreach (var bookingRequest in CurrentBookingMonthRequests.Take(newAmountOfDemand))
                    {
                        var courtFileNumber = $"{RegistryFixture.VancouverRegistry.Location} {courtClass}{bookingRequest.PhysicalFileId.ToString("00000")}";

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
                        var matchingDateSelections = DateSelections.Where(x => x.CaseBookingRequestId == bookingRequest.Id && x.Date.Month == bookingMonth).OrderBy(x => x.PreferenceOrder);
                        foreach (var dateSelection in matchingDateSelections)
                        {
                            //check if date has availability and try to book date
                            var matchingTrialDate = trialDates.Where(x => x.Date == dateSelection.Date && x.TrialType == trialType).FirstOrDefault();
                            if (matchingTrialDate != null && matchingTrialDate.BookingSlotsAvailable > 0)
                            {
                                var result = Client.BookTrial(bookingRequest.Id, dateSelection.Date, registryId, hearingType, hearingLength);
                                if (result == "success")    //will always be able to successfully book since there is no API call atm
                                {
                                    successfulBooking = true;
                                    DecrementAvailabilityDates(bookingRequest.TrialLength, ref trialDates, dateSelection.Date, trialType);

                                    //write booking to csv
                                    var firstDateSelection = matchingDateSelections.Take(1).FirstOrDefault();
                                    var secondDateSelection = matchingDateSelections.Skip(1).Take(1).FirstOrDefault();
                                    var thirdDateSelection = matchingDateSelections.Skip(2).Take(1).FirstOrDefault();
                                    var fourthDateSelection = matchingDateSelections.Skip(3).Take(1).FirstOrDefault();
                                    var fifthDateSelection = matchingDateSelections.Skip(4).Take(1).FirstOrDefault();

                                    var newLine = string.Format(
                                        "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                                        courtFileNumber,
                                        0,
                                        lotteryRanking++,
                                        bookingRequest.TrialLength,
                                        RegistryFixture.VancouverRegistry.Location,
                                        string.Format("{0} - Motor Vehicle Accidents", courtClass),
                                        firstDateSelection != null ? firstDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        secondDateSelection != null ? secondDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        thirdDateSelection != null ? thirdDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        fourthDateSelection != null ? fourthDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        fifthDateSelection != null ? fifthDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                        dateSelection.Date.ToString("dd-MMMM-yyyy"),
                                        0
                                    );

                                    writer.WriteLine(newLine);
                                    break;
                                }
                            }
                        }

                        //if we were unable to successfully book something, we increment the unmetDemand count and add it to the remaining demand
                        //to be passed on
                        if (!successfulBooking)
                        {
                            //we need to also generate new dates for this new unmet demand for next month
                            int newBookingMonth = bookingMonth + 1;
                            if (newBookingMonth > 12)
                                newBookingMonth = 1;

                            DateSelectionFixture.AddUnmetDemandDateSelectionsForNextMonth(bookingRequest.Id, trialType, newBookingMonth);

                            var firstDateSelection = matchingDateSelections.Take(1).FirstOrDefault();
                            var secondDateSelection = matchingDateSelections.Skip(1).Take(1).FirstOrDefault();
                            var thirdDateSelection = matchingDateSelections.Skip(2).Take(1).FirstOrDefault();
                            var fourthDateSelection = matchingDateSelections.Skip(3).Take(1).FirstOrDefault();
                            var fifthDateSelection = matchingDateSelections.Skip(4).Take(1).FirstOrDefault();

                            //write booking to csv
                            var newLine = string.Format(
                                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                                courtFileNumber,
                                0,
                                lotteryRanking++,
                                bookingRequest.TrialLength,
                                RegistryFixture.VancouverRegistry.Location,
                                string.Format("{0} - Motor Vehicle Accidents", courtClass),
                                firstDateSelection != null ? firstDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                secondDateSelection != null ? secondDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                thirdDateSelection != null ? thirdDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                fourthDateSelection != null ? fourthDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                fifthDateSelection != null ? fifthDateSelection.Date.ToString("dd-MMMM-yyyy") : "",
                                "Not Booked",
                                1
                            );
                            writer.WriteLine(newLine);

                            Client.RecordUnmetDemand(bookingRequest.Id, bookingRequest.BookingPeriodId);
                        }
                    }
                    #endregion
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Dispose();
            }

            //set up summary csv
            FileStream summaryFileStream = null;
            string summaryFilePath = $"{WorkingDirectory}/Outputs/{bookingMonth}-{bookingYear}-Booking-Summary-" + currentTime.ToString("MM-dd-yyyy-H-mm-ss") + ".csv";
            try
            {
                summaryFileStream = new FileStream(summaryFilePath, FileMode.OpenOrCreate);

                using (var writer = new StreamWriter(summaryFileStream))
                {
                    writer.WriteLine(LotteryMonthSummaryCSVHeader);
                    writer.WriteLine(LotteryMonthSummaryCSVSubheader);

                    foreach(var group in trialDates.GroupBy(x => x.Date).OrderBy(x => x.Key))
                    {
                        //get all the slots for a particular date and write them out on a single line
                        string line = "";
                        var matchingDays = group.ToList();
                        for (int x = 7; x > 0; x--)
                        {
                            
                            var date = group.Key.ToString("d-MMM-yyyy", DateTimeFormatInfoExtension.DateTimeFormatInfoEx);
                            var trialTypeDay = matchingDays.Where(y => (int)y.TrialType == x).FirstOrDefault();
                            if (trialTypeDay != null)
                            {
                                //booked and remaining dates need to be properly calculated since the booking slots available goes negative if overbooked
                                int booked = trialTypeDay.InitialBookingSlotsAvailable;
                                int remaining = 0;
                                if (trialTypeDay.BookingSlotsAvailable > 0)
                                {
                                    remaining = trialTypeDay.BookingSlotsAvailable;
                                    booked -= trialTypeDay.BookingSlotsAvailable;
                                }
                                    
                                line += $"{date},{remaining},{booked},,";
                            }
                            else
                                line += $"{date},,,,";

                        }
                        writer.WriteLine(line.Substring(0, line.Length - 1));
                    }
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
            finally
            {
                if (summaryFileStream != null)
                    summaryFileStream.Dispose();
            }
        }

        //function used to decrement the prototype's available date slots based on the trial length
        private static void DecrementAvailabilityDates(decimal trialLength, ref TrialDate[] trialDates, DateTime trialDate, TrialType trialType)
        {
            TrialDate startDate = trialDates.Where(x => x.Date == trialDate && x.TrialType == trialType).First();

            //modify the availability based on the fixed trial lengths (2 - 5)
            if (trialLength <= 5)
            {
                if (trialLength == 1)
                {
                    DecrementAvailabilityDate(ref trialDates, trialDate, 1);

                    if (trialDate.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        var oneDayPrior = trialDate.AddDays(-1);
                        var oneDayPriorThreeDay = trialDates.Where(x => x.Date == oneDayPrior && x.TrialType == TrialType.ThreeDay).FirstOrDefault();
                        if (oneDayPriorThreeDay != null)
                            oneDayPriorThreeDay.BookingSlotsAvailable--;

                        var oneDayPriorTwoDay = trialDates.Where(x => x.Date == oneDayPrior && x.TrialType == TrialType.TwoDay).FirstOrDefault();
                        if (oneDayPriorTwoDay != null)
                            oneDayPriorTwoDay.BookingSlotsAvailable--;
                    }
                    else if (trialDate.DayOfWeek == DayOfWeek.Thursday || trialDate.DayOfWeek == DayOfWeek.Friday)
                    {
                        var twoDaysPrior = trialDate.AddDays(-2);
                        var twoDayPriorThreeDay = trialDates.Where(x => x.Date == twoDaysPrior && x.TrialType == TrialType.ThreeDay).FirstOrDefault();
                        if (twoDayPriorThreeDay != null)
                            twoDayPriorThreeDay.BookingSlotsAvailable--;

                        var oneDayPrior = trialDate.AddDays(-1);
                        var oneDayPriorThreeDay = trialDates.Where(x => x.Date == oneDayPrior && x.TrialType == TrialType.ThreeDay).FirstOrDefault();
                        if (oneDayPriorThreeDay != null)
                            oneDayPriorThreeDay.BookingSlotsAvailable--;

                        var oneDayPriorTwoDay = trialDates.Where(x => x.Date == oneDayPrior && x.TrialType == TrialType.TwoDay).FirstOrDefault();
                        if (oneDayPriorTwoDay != null)
                            oneDayPriorTwoDay.BookingSlotsAvailable--;
                    }
                }
                else if (trialLength == 2)
                {
                    DecrementAvailabilityDate(ref trialDates, trialDate, 2);

                    //if wednesday is selected, will need to decrement the 2 and 3 day slots for tuesday
                    if (trialDate.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        var date = trialDate.AddDays(-1);
                        var otherThreeDay = trialDates.Where(x => x.Date == date && x.TrialType == TrialType.ThreeDay).FirstOrDefault();
                        if (otherThreeDay != null)
                            otherThreeDay.BookingSlotsAvailable--;

                        var otherTwoDay = trialDates.Where(x => x.Date == date && x.TrialType == TrialType.TwoDay).FirstOrDefault();
                        if (otherTwoDay != null)
                            otherTwoDay.BookingSlotsAvailable--;
                    }
                    //if thursday is selected, decrement all 3 day slots and the 2day slot for wednesday
                    else if (trialDate.DayOfWeek == DayOfWeek.Thursday)
                    {
                        var tuesday = trialDate.AddDays(-2);
                        var tuesdayThreeDay = trialDates.Where(x => x.Date == tuesday && x.TrialType == TrialType.ThreeDay).FirstOrDefault();
                        if (tuesdayThreeDay != null)
                            tuesdayThreeDay.BookingSlotsAvailable--;

                        var wednesday = trialDate.AddDays(-1);
                        var wednesdayThreeDay = trialDates.Where(x => x.Date == wednesday && x.TrialType == TrialType.ThreeDay).FirstOrDefault();
                        if (wednesdayThreeDay != null)
                            wednesdayThreeDay.BookingSlotsAvailable--;

                        var wednesdayTwoDay = trialDates.Where(x => x.Date == wednesday && x.TrialType == TrialType.TwoDay).FirstOrDefault();
                        if (wednesdayTwoDay != null)
                            wednesdayTwoDay.BookingSlotsAvailable--;
                    }
                }
                //3 day trials would decrement the other 3 day slot and 
                else if (trialLength == 3)
                {
                    DecrementAvailabilityDate(ref trialDates, trialDate, 3);

                    //if the 3day trial was on wednesday, we will need to decrement the tuesday slot for 3days as well
                    if (trialDate.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        var date = trialDate.AddDays(-1);
                        var otherThreeDay = trialDates.Where(x => x.Date == date && x.TrialType == TrialType.ThreeDay).FirstOrDefault();
                        if (otherThreeDay != null)
                            otherThreeDay.BookingSlotsAvailable--;
                    }
                }
                //since 4 & 5 day trials must start on Mon, we can simply decrement all the slots for the days of the week for that trial length
                else if (trialLength == 4)
                    DecrementAvailabilityDate(ref trialDates, trialDate, 4);
                else if (trialLength == 5)
                    DecrementAvailabilityDate(ref trialDates, trialDate, 5);
                else    //we will handle decrements for half/quarter days here
                {

                }
            }
            //for longer trials, we will need to loop through the number of days and decrement those slots
            else
            {
                //6+ day trials can only be booked on mondays or tuesdays so we can break it into weeks and the remainder
                int weeks = decimal.ToInt32(trialLength) / 5;
                int remainingDays = decimal.ToInt32(trialLength) % 5;

                //if the trial date starts on a tuesday, we need to decrement by 4 to account for the missing day
                if (trialDate.DayOfWeek == DayOfWeek.Tuesday)
                {
                    weeks--;
                    remainingDays++;
                    DecrementAvailabilityDate(ref trialDates, trialDate, 4);
                    trialDate = trialDate.AddDays(6);
                }
                

                //for each week, reduce every mon to fri slot by 1
                for (int x = 0; x < weeks; x++)
                {
                    DecrementAvailabilityDate(ref trialDates, trialDate, 5);
                    trialDate = trialDate.AddDays(7);
                }

                //reduce the remaining days left
                DecrementAvailabilityDate(ref trialDates, trialDate, remainingDays);
            }
        }

        private static void DecrementAvailabilityDate(ref TrialDate[] trialDates, DateTime trialDate, int decrementAmount)
        {
            for (int x = 0; x < decrementAmount; x++)
            {
                foreach (var day in trialDates.Where(x => x.Date == trialDate))
                    day.BookingSlotsAvailable--;

                trialDate = trialDate.AddDays(1);
            }
        }


    }
}
