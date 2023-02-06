
using Microsoft.Extensions.Configuration;
using SCJ.Booking.CourtBookingPrototype.Enumerations;
using SCJ.Booking.CourtBookingPrototype.Fixtures;
using SCJ.Booking.CourtBookingPrototype.Models;
using System.Runtime.CompilerServices;

namespace SCJ.Booking.CourtBookingPrototype
{
    public class Program
    {
        public static decimal DefaultDemandSupplyRatio = 1.25m;

        //average amount of total bookings per booking period based on data provided by Lorne
        public static int DefaultNumberOfBookingsPerBookingPeriod = 167;

        //the average amount of bookings for each trial length based on data provided by Lorne
        #region Default Booking Numbers
        public static int DefaultNumberOf16PlusDayBookings = 4;
        public static int DefaultNumberOf15To6DayBookings = 12;
        public static int DefaultNumberOf5DayBookings = 141;
        public static int DefaultNumberOf4DayBookings = 5;
        public static int DefaultNumberOf3DayBookings = 6;
        public static int DefaultNumberOf2DayBookings = 1;
        public static int DefaultNumberOf1DayBookings = 1;
        #endregion

        //storage of all the date selections
        public static List<DateSelection> DateSelections = new List<DateSelection>();

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


            #region generate date selections
            int dateSelectionIdCounter = 1;

            #region 16+ booking requests
            foreach(var bookingRequest in CaseBookingRequestsFixture.AugustSixteenPlusDayCaseBookingRequests.Take(DefaultNumberOf16PlusDayBookings))
            {
                for(int x = 0; x < AvailabilityDatesFixture.AugustDatesForSixteenPlus.Length; x++)
                {
                    DateSelections.Add(new DateSelection
                    {
                        Id = dateSelectionIdCounter++,
                        CaseBookingRequestId = bookingRequest.Id,
                        Date = AvailabilityDatesFixture.AugustDatesForSixteenPlus[x].Date,
                        PreferenceOrder = x + 1
                    });
                }
            }
            #endregion

            #region 6-15 date selections
            foreach(var bookingRequest in CaseBookingRequestsFixture.AugustFifteenToSixDayCaseBookingRequests.Take(DefaultNumberOf15To6DayBookings))
            {
                for(int x = 0; x < AvailabilityDatesFixture.AugustDatesForSixToFifteen.Length; x++)
                {
                    DateSelections.Add(new DateSelection
                    {
                        Id = dateSelectionIdCounter++,
                        CaseBookingRequestId = bookingRequest.Id,
                        Date = AvailabilityDatesFixture.AugustDatesForSixteenPlus[x].Date,
                        PreferenceOrder = x + 1
                    });
                }
            }
            #endregion


            #region

            #endregion

            #endregion
        }
    }
}
