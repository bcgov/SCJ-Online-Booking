using SCJ.Booking.CourtBookingPrototype.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public static class AvailabilityDatesFixture
    {
        public static AvailabilityDate[] AugustDatesForOne =
        {
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 1),
                InitialNumberOfSlots = 8,
                RemainingNumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 2),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 7),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 8),
                InitialNumberOfSlots = 9,
                RemainingNumberOfSlots = 9,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 9),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 13),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 14),
                InitialNumberOfSlots = 11,
                RemainingNumberOfSlots = 11,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 15),
                InitialNumberOfSlots = 9,
                RemainingNumberOfSlots = 9,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 16),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 20),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 21),
                InitialNumberOfSlots = 11,
                RemainingNumberOfSlots = 11,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 22),
                InitialNumberOfSlots = 9,
                RemainingNumberOfSlots = 9,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 23),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 27),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 28),
                InitialNumberOfSlots = 11,
                RemainingNumberOfSlots = 11,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 29),
                InitialNumberOfSlots = 9,
                RemainingNumberOfSlots = 9,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 30),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
        };

        public static AvailabilityDate[] AugustDatesForSixteenPlus =
        {
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 6),
                InitialNumberOfSlots = 2,
                RemainingNumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 12),
                InitialNumberOfSlots = 2,
                RemainingNumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 19),
                InitialNumberOfSlots = 2,
                RemainingNumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 26),
                InitialNumberOfSlots = 2,
                RemainingNumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
        };

        public static AvailabilityDate[] AugustDatesForSixToFifteen =
        {
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 6),
                InitialNumberOfSlots = 35,
                RemainingNumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 12),
                InitialNumberOfSlots = 35,
                RemainingNumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 19),
                InitialNumberOfSlots = 35,
                RemainingNumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 26),
                InitialNumberOfSlots = 35,
                RemainingNumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
        };

        public static AvailabilityDate[] AugustDatesForFive =
        {
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 12),
                InitialNumberOfSlots = 27,
                RemainingNumberOfSlots = 27,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 19),
                InitialNumberOfSlots = 27,
                RemainingNumberOfSlots = 27,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 26),
                InitialNumberOfSlots = 27,
                RemainingNumberOfSlots = 27,
                TrialType = TrialType.FiveDay
            },
        };

        public static AvailabilityDate[] AugustDatesForFour =
        {
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 6),
                InitialNumberOfSlots = 27,
                RemainingNumberOfSlots = 27,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 12),
                InitialNumberOfSlots = 27,
                RemainingNumberOfSlots = 27,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 19),
                InitialNumberOfSlots = 27,
                RemainingNumberOfSlots = 27,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 26),
                InitialNumberOfSlots = 27,
                RemainingNumberOfSlots = 27,
                TrialType = TrialType.FourDay
            },
        };
        
        public static AvailabilityDate[] AugustDatesForThree =
        {
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 7),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 13),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 14),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 20),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 21),
                InitialNumberOfSlots = 77,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 27),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 28),
                InitialNumberOfSlots = 77,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.ThreeDay
            },
        };

        public static AvailabilityDate[] AugustDatesForTwo =
        {
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 1),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 7),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 8),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 13),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 14),
                InitialNumberOfSlots = 9,
                RemainingNumberOfSlots = 9,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 15),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 20),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 21),
                InitialNumberOfSlots = 9,
                RemainingNumberOfSlots = 9,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 22),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 27),
                InitialNumberOfSlots = 6,
                RemainingNumberOfSlots = 6,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 28),
                InitialNumberOfSlots = 9,
                RemainingNumberOfSlots = 9,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 29),
                InitialNumberOfSlots = 7,
                RemainingNumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
        };

        public static AvailabilityDate[] SeptemberDates =
        {

        };
    }

    public class AvailabilityDate
    {
        public int AvailabilityParameterId { get; set; }
        public DateTime Date { get; set; }
        public int InitialNumberOfSlots { get; set; }
        public int RemainingNumberOfSlots { get; set; }
        public TrialType TrialType { get; set; }
    }
}
