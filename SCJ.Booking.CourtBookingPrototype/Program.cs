using SCJ.Booking.CourtBookingPrototype.Clients;
using SCJ.Booking.CourtBookingPrototype.Enumerations;
using SCJ.Booking.CourtBookingPrototype.Extensions;
using SCJ.Booking.CourtBookingPrototype.Fixtures;
using SCJ.Booking.CourtBookingPrototype.Models;
using SCJ.Booking.MVC.Utils;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SCJ.Booking.CourtBookingPrototype
{
    public class Program
    {
        private static string BookingScheduleTemplate = "~/Templates/Lottery-Booking-Schedule-Template.csv";

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

        //data for CaseBookingRequests are hard coded.  From there, the date selections for each CaseBookingRequest is generated in the DateSelection Fixture
        public static List<DateSelection> DateSelections = DateSelectionFixture.DateSelections;

        //storage of all CaseBookingRequests
        public static List<CaseBookingRequest> BookingRequests = new List<CaseBookingRequest>();

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

            //run lottery simulation for August
            var augustAvailabilityParameters = AvailabilityParametersFixture.AugustParameters;
            RunLotterySimulation(augustAvailabilityParameters.RegistryId, augustAvailabilityParameters.HearingType, augustAvailabilityParameters.CourtClass, augustAvailabilityParameters.HearingLength, 2024, FakeTrialBookingClient.AugustMonth);

            #endregion
        }

        private static void RunLotterySimulation(int registryId, decimal hearingType, string courtClass, decimal hearingLength, int bookingYear, int bookingMonth)
        {
            //duplicate the template for writing
            string newFilePath = $"~/Outputs/{bookingMonth}-{bookingYear}-Booking-Schedule-" + DateTime.Now.ToString() + ".csv";
            try
            {
                File.Copy(BookingScheduleTemplate, newFilePath);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
            
            using (var writer = new StreamWriter(newFilePath))
            {
                //get all available dates for the booking period
                TrialDate[] trialDates = Client.GetAvailableTrialDates(registryId, hearingType, courtClass, hearingLength, bookingYear, bookingMonth);

                //get all the demand for the current booking period
                if (bookingMonth == FakeTrialBookingClient.AugustMonth)
                    BookingRequests = CaseBookingRequestsFixture.AugustCaseBookingRequests;
                else if (bookingMonth == FakeTrialBookingClient.SeptemberMonth)
                    BookingRequests = CaseBookingRequestsFixture.SeptemberCaseBookingRequests;

                #region book unmet demand
                //get all unmet demand so we book those first
                List<List<UnmetDemand>> previousUnmetDemand = Client.GetUnmetDemand();

                //create a list of all unmet demand that we weren't able to book
                List<UnmetDemand> remainingUnmetDemand = new List<UnmetDemand>();

                //try to create booking for unmet demand
                foreach (var unmetDemandTier in previousUnmetDemand)
                {
                    //run lottery to determine the order
                    int lotteryRanking = 1;
                    unmetDemandTier.Shuffle();
                    foreach (var unmetDemand in unmetDemandTier)
                    {
                        var matchingCaseBookingRequest = BookingRequests.Where(x => x.Id == unmetDemand.CaseBookingRequestId).FirstOrDefault();
                        if (matchingCaseBookingRequest != null)
                        {
                            var courtFileNumber = $"{RegistryFixture.VancouverRegistry.Location} {courtClass}{String.Format("0:00000", matchingCaseBookingRequest.PhysicalFileId)}";

                            //set a flag to indicate if we could create a booking for any of the date selections
                            bool successfulBooking = false;

                            //get all the selected dates of this booking request and run through them to try and book a date
                            var matchingDateSelections = DateSelections.Where(x => x.CaseBookingRequestId == matchingCaseBookingRequest.Id).OrderBy(x => x.PreferenceOrder);

                            foreach (var dateSelection in matchingDateSelections)
                            {
                                //check if date has availability and try to book date
                                var matchingTrialDate = trialDates.Where(x => x.Date == dateSelection.Date).FirstOrDefault();
                                if (matchingTrialDate != null && matchingTrialDate.BookingSlotsAvailable > 0)
                                {
                                    var result = Client.BookTrial(matchingCaseBookingRequest.Id, dateSelection.Date, registryId, hearingType, hearingLength);
                                    if (result == "success")    //will always be able to successfully book since there is no API call atm
                                    {
                                        successfulBooking = true;
                                        DecrementAvailabilityDates(matchingCaseBookingRequest.TrialLength, ref trialDates, dateSelection.Date);

                                        //write booking to csv
                                        var newLine = string.Format(
                                            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                                            courtFileNumber,
                                            unmetDemand.Count,
                                            lotteryRanking++,
                                            hearingLength,
                                            RegistryFixture.VancouverRegistry.Location,
                                            string.Format("{0} - Motor Vehicle", courtClass),
                                            matchingDateSelections.ElementAt(0).Date.ToString("dd-MMMM-yyyy"),
                                            matchingDateSelections.ElementAt(1).Date.ToString("dd-MMMM-yyyy"),
                                            matchingDateSelections.ElementAt(2).Date.ToString("dd-MMMM-yyyy"),
                                            matchingDateSelections.ElementAt(3).Date.ToString("dd-MMMM-yyyy"),
                                            matchingDateSelections.ElementAt(4).Date.ToString("dd-MMMM-yyyy"),
                                            dateSelection.Date.ToString("dd-MMMM-yyyy"),
                                            0
                                        );

                                        writer.WriteLine(newLine);
                                        lotteryRanking++;
                                        break;
                                    }
                                }
                            }

                            //if we were unable to successfully book something, we increment the unmetDemand count and add it to the remaining demand
                            //to be passed on
                            if (!successfulBooking)
                            {
                                unmetDemand.Count++;
                                remainingUnmetDemand.Add(unmetDemand);

                                //write booking to csv
                                var newLine = string.Format(
                                    "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                                    courtFileNumber,
                                    unmetDemand.Count,
                                    lotteryRanking++,
                                    hearingLength,
                                    RegistryFixture.VancouverRegistry.Location,
                                    string.Format("{0} - Motor Vehicle", courtClass),
                                    matchingDateSelections.ElementAt(0).Date.ToString("dd-MMMM-yyyy"),
                                    matchingDateSelections.ElementAt(1).Date.ToString("dd-MMMM-yyyy"),
                                    matchingDateSelections.ElementAt(2).Date.ToString("dd-MMMM-yyyy"),
                                    matchingDateSelections.ElementAt(3).Date.ToString("dd-MMMM-yyyy"),
                                    matchingDateSelections.ElementAt(4).Date.ToString("dd-MMMM-yyyy"),
                                    "Not Booked",
                                    unmetDemand.Count
                                );

                                writer.WriteLine(newLine);
                            }
                            else    //remove this case booking from the master list of booking requests as we were able to create a successful booking
                            {
                                BookingRequests.Remove(matchingCaseBookingRequest);
                            }
                        }
                    }
                }
                #endregion

                #region book for normal slots
                //run lottery to determine the order
                BookingRequests.Shuffle();

                foreach (var bookingRequest in BookingRequests)
                {
                    int lotteryRanking = 1;
                    var courtFileNumber = $"{RegistryFixture.VancouverRegistry.Location} {courtClass}{String.Format("0:00000", bookingRequest.PhysicalFileId)}";

                    //set a flag to indicate if we could create a booking for any of the date selections
                    bool successfulBooking = false;

                    //get all the selected dates of this booking request and run through them to try and book a date
                    var matchingDateSelections = DateSelections.Where(x => x.CaseBookingRequestId == bookingRequest.Id).OrderBy(x => x.PreferenceOrder);
                    foreach (var dateSelection in matchingDateSelections)
                    {
                        //check if date has availability and try to book date
                        var matchingTrialDate = trialDates.Where(x => x.Date == dateSelection.Date).FirstOrDefault();
                        if (matchingTrialDate != null && matchingTrialDate.BookingSlotsAvailable > 0)
                        {
                            var result = Client.BookTrial(bookingRequest.Id, dateSelection.Date, registryId, hearingType, hearingLength);
                            if (result == "success")    //will always be able to successfully book since there is no API call atm
                            {
                                successfulBooking = true;
                                DecrementAvailabilityDates(bookingRequest.TrialLength, ref trialDates, dateSelection.Date);

                                //write booking to csv
                                var newLine = string.Format(
                                    "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                                    courtFileNumber,
                                    0,
                                    lotteryRanking++,
                                    hearingLength,
                                    RegistryFixture.VancouverRegistry.Location,
                                    string.Format("{0} - Motor Vehicle", courtClass),
                                    matchingDateSelections.ElementAt(0).Date.ToString("dd-MMMM-yyyy"),
                                    matchingDateSelections.ElementAt(1).Date.ToString("dd-MMMM-yyyy"),
                                    matchingDateSelections.ElementAt(2).Date.ToString("dd-MMMM-yyyy"),
                                    matchingDateSelections.ElementAt(3).Date.ToString("dd-MMMM-yyyy"),
                                    matchingDateSelections.ElementAt(4).Date.ToString("dd-MMMM-yyyy"),
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
                        remainingUnmetDemand.Add(new UnmetDemand
                        {
                            CaseBookingRequestId = bookingRequest.Id,
                            BookingPeriodId = bookingRequest.BookingPeriodId,
                            Count = 1
                        });

                        //write booking to csv
                        var newLine = string.Format(
                            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                            courtFileNumber,
                            0,
                            lotteryRanking++,
                            hearingLength,
                            RegistryFixture.VancouverRegistry.Location,
                            string.Format("{0} - Motor Vehicle", courtClass),
                            matchingDateSelections.ElementAt(0).Date.ToString("dd-MMMM-yyyy"),
                            matchingDateSelections.ElementAt(1).Date.ToString("dd-MMMM-yyyy"),
                            matchingDateSelections.ElementAt(2).Date.ToString("dd-MMMM-yyyy"),
                            matchingDateSelections.ElementAt(3).Date.ToString("dd-MMMM-yyyy"),
                            matchingDateSelections.ElementAt(4).Date.ToString("dd-MMMM-yyyy"),
                            "Not Booked",
                            1
                        );
                    }
                }
                #endregion
            }
        }

        //function used to decrement the prototype's available date slots based on the trial length
        private static void DecrementAvailabilityDates(decimal trialLength, ref TrialDate[] trialDates, DateTime trialDate)
        {
            TrialDate startDate = trialDates.Where(x => x.Date == trialDate).First();
            
            //modify the availability based on the fixed trial lengths (2 - 5)
            if (trialLength <= 5)
            {
                if(trialLength == 1)
                {
                    DecrementAvailabilityDate(ref trialDates, trialDate, 1);

                    if(trialDate.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        var oneDayPrior = trialDate.AddDays(-1);
                        var oneDayPriorThreeDay = trialDates.Where(x => x.Date == oneDayPrior && x.TrialType == TrialType.ThreeDay).FirstOrDefault();
                        if (oneDayPriorThreeDay != null)
                            oneDayPriorThreeDay.BookingSlotsAvailable--;

                        var oneDayPriorTwoDay = trialDates.Where(x => x.Date == oneDayPrior && x.TrialType == TrialType.TwoDay).FirstOrDefault();
                        if (oneDayPriorTwoDay != null)
                            oneDayPriorTwoDay.BookingSlotsAvailable--;
                    }
                    else if(trialDate.DayOfWeek == DayOfWeek.Thursday || trialDate.DayOfWeek == DayOfWeek.Friday)
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
                    if(trialDate.DayOfWeek == DayOfWeek.Wednesday)
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
                        if(otherThreeDay != null)
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
                //6+ day trials can only be booked on mondays so we can break it into 5 day weeks and the remainder
                int weeks = decimal.ToInt32(trialLength) / 5;
                int remainingDays = decimal.ToInt32(trialLength) % 5;

                //for each week, reduce every mon to fri slot by 1
                for (int x = 0; x < weeks; x++)
                {
                    DecrementAvailabilityDate(ref trialDates, trialDate, 5);
                    trialDate.AddDays(7);
                }

                //reduce the remaining days left
                DecrementAvailabilityDate(ref trialDates, trialDate, remainingDays);
            }
        }

        private static void DecrementAvailabilityDate(ref TrialDate[] trialDates, DateTime trialDate, int decrementAmount)
        {
            for(int x = 0; x < decrementAmount; x++)
            {
                foreach(var day in trialDates.Where(x => x.Date == trialDate))
                    day.BookingSlotsAvailable--;
                
                trialDate.AddDays(1);
            }
        }


    }
}
