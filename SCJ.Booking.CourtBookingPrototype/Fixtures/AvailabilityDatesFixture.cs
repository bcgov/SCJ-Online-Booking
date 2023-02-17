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
        public static AvailabilityDate[] AugustDates =
        {
            #region 16+ dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 6),
                NumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 12),
                NumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 19),
                NumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 26),
                NumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
            #endregion

            #region 6 - 15 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 6),
                NumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 12),
                NumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 19),
                NumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 26),
                NumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
            #endregion

            #region 5 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 12),
                NumberOfSlots = 27,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 19),
                NumberOfSlots = 27,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 26),
                NumberOfSlots = 27,
                TrialType = TrialType.FiveDay
            },
            #endregion

            #region 4 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 6),
                NumberOfSlots = 27,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 12),
                NumberOfSlots = 27,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 19),
                NumberOfSlots = 27,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 26),
                NumberOfSlots = 27,
                TrialType = TrialType.FourDay
            },
            #endregion

            #region 3 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 7),
                NumberOfSlots = 6,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 13),
                NumberOfSlots = 6,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 14),
                NumberOfSlots = 7,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 20),
                NumberOfSlots = 6,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 21),
                NumberOfSlots = 7,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 27),
                NumberOfSlots = 6,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 28),
                NumberOfSlots = 7,
                TrialType = TrialType.ThreeDay
            },
            #endregion

            #region 2 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 1),
                NumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 7),
                NumberOfSlots = 6,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 8),
                NumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 13),
                NumberOfSlots = 6,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 14),
                NumberOfSlots = 9,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 15),
                NumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 20),
                NumberOfSlots = 6,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 21),
                NumberOfSlots = 9,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 22),
                NumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 27),
                NumberOfSlots = 6,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 28),
                NumberOfSlots = 9,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 29),
                NumberOfSlots = 7,
                TrialType = TrialType.TwoDay
            },
            #endregion

            #region 1 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 1),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 2),
                NumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 7),
                NumberOfSlots = 6,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 8),
                NumberOfSlots = 9,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 9),
                NumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 13),
                NumberOfSlots = 6,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 14),
                NumberOfSlots = 11,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 15),
                NumberOfSlots = 9,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 16),
                NumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 20),
                NumberOfSlots = 6,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 21),
                NumberOfSlots = 11,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 22),
                NumberOfSlots = 9,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 23),
                NumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 27),
                NumberOfSlots = 6,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 28),
                NumberOfSlots = 11,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 29),
                NumberOfSlots = 9,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 30),
                NumberOfSlots = 7,
                TrialType = TrialType.OneDay
            },
            #endregion
        };

        public static AvailabilityDate[] SeptemberDates =
        {
            #region 16+ dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 3),
                NumberOfSlots = 2,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 9),
                NumberOfSlots = 4,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 16),
                NumberOfSlots = 4,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 23),
                NumberOfSlots = 4,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 30),
                NumberOfSlots = 4,
                TrialType = TrialType.SixteenPlusDay
            },
            #endregion

            #region 6 - 15 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 3),
                NumberOfSlots = 35,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 9),
                NumberOfSlots = 60,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 16),
                NumberOfSlots = 60,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 23),
                NumberOfSlots = 60,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 30),
                NumberOfSlots = 60,
                TrialType = TrialType.SixToFifteenDay
            },
            #endregion

            #region 5 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 9),
                NumberOfSlots = 43,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 16),
                NumberOfSlots = 43,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 23),
                NumberOfSlots = 43,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 30),
                NumberOfSlots = 43,
                TrialType = TrialType.FiveDay
            },
            #endregion

            #region 4 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 3),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 9),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 16),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 8, 23),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 30),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            #endregion

            #region 3 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 4),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 10),
                NumberOfSlots = 10,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 11),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 17),
                NumberOfSlots = 10,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 18),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 24),
                NumberOfSlots = 10,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 25),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            #endregion

            #region 2 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 4),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 5),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 10),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 11),
                NumberOfSlots = 14,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 12),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 17),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 18),
                NumberOfSlots = 14,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 19),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 24),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 25),
                NumberOfSlots = 14,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 26),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            #endregion

            #region 1 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 4),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 5),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 6),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 10),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 11),
                NumberOfSlots = 18,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 12),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 13),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 17),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 18),
                NumberOfSlots = 18,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 19),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 20),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 24),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 25),
                NumberOfSlots = 18,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 26),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 9, 27),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            #endregion
        };

        public static AvailabilityDate[] OctoberDates =
        {
            #region 16+ dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 7),
                NumberOfSlots = 4,
                TrialType = TrialType.SixteenPlusDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 15),
                NumberOfSlots = 4,
                TrialType = TrialType.SixteenPlusDay
            },
            #endregion

            #region 6 - 15 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 7),
                NumberOfSlots = 60,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 15),
                NumberOfSlots = 60,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 21),
                NumberOfSlots = 60,
                TrialType = TrialType.SixToFifteenDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 28),
                NumberOfSlots = 60,
                TrialType = TrialType.SixToFifteenDay
            },
            #endregion

            #region 5 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 7),
                NumberOfSlots = 43,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 21),
                NumberOfSlots = 43,
                TrialType = TrialType.FiveDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 28),
                NumberOfSlots = 43,
                TrialType = TrialType.FiveDay
            },
            #endregion

            #region 4 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 7),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 15),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 21),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 28),
                NumberOfSlots = 43,
                TrialType = TrialType.FourDay
            },
            #endregion

            #region 3 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 1),
                NumberOfSlots = 10,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 2),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 8),
                NumberOfSlots = 10,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 9),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 16),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 22),
                NumberOfSlots = 10,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 23),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 29),
                NumberOfSlots = 10,
                TrialType = TrialType.ThreeDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 30),
                NumberOfSlots = 8,
                TrialType = TrialType.ThreeDay
            },
            #endregion

            #region 2 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 1),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 2),
                NumberOfSlots = 14,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 3),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 8),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 9),
                NumberOfSlots = 14,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 10),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 16),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 17),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 22),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 23),
                NumberOfSlots =14,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 24),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 29),
                NumberOfSlots = 10,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 30),
                NumberOfSlots = 14,
                TrialType = TrialType.TwoDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 31),
                NumberOfSlots = 8,
                TrialType = TrialType.TwoDay
            },
            #endregion

            #region 1 dates
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 1),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 2),
                NumberOfSlots = 18,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 3),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 4),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 8),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 9),
                NumberOfSlots = 18,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 10),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 11),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 16),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 17),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 18),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 22),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 23),
                NumberOfSlots = 18,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 24),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 25),
                NumberOfSlots = 8,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 29),
                NumberOfSlots = 10,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 30),
                NumberOfSlots = 18,
                TrialType = TrialType.OneDay
            },
            new AvailabilityDate
            {
                AvailabilityParameterId = 1,
                Date = new DateTime(2024, 10, 31),
                NumberOfSlots = 14,
                TrialType = TrialType.OneDay
            },
            #endregion
        };
        
        public static AvailabilityDate[] NovemberDates =
        {

        };

        public static AvailabilityDate[] DecemberDates =
        {

        };

        public static AvailabilityDate[] JanuaryDates =
        {

        };
    }

    public class AvailabilityDate
    {
        public int AvailabilityParameterId { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfSlots { get; set; }
        public TrialType TrialType { get; set; }
    }
}
