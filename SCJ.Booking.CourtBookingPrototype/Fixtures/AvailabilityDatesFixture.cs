using SCJ.Booking.CourtBookingPrototype.Enumerations;
using SCJ.Booking.CourtBookingPrototype.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public static class AvailabilityDatesFixture
    {
        private static string SupplyCSVHeaderFirstRow = "1 Day Trials,,2 Days,,3 Days,,4 Days,,5 Days,,6-15 Days,,16+ Days,";
        private static string SupplyCSVHeaderSecondRow = "Date,Available,Date,Available,Date,Available,Date,Available,Date,Available,Date,Available,Date,Available";

        private static List<AvailabilityDate> _augustDates { get; set; }
        public static List<AvailabilityDate> AugustDates
        {
            get
            {
                if (_augustDates == null || _augustDates.Count <= 0)
                {
                    _augustDates = new List<AvailabilityDate>
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
                }

                return _augustDates;
            }
        }

        private static List<AvailabilityDate> _septemberDates { get; set; }
        public static List<AvailabilityDate> SeptemberDates
        {
            get
            {
                if(_septemberDates == null || _septemberDates.Count <= 0)
                {
                    _septemberDates = new List<AvailabilityDate>
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
                }

                return _septemberDates;
            }
        }

        private static List<AvailabilityDate> _octoberDates { get; set; }
        public static List<AvailabilityDate> OctoberDates 
        {
            get
            {
                if (_octoberDates == null || _octoberDates.Count <= 0)
                {
                    _octoberDates = new List<AvailabilityDate> { 
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
                }

                return _octoberDates;
            }
        }

        private static List<AvailabilityDate> _novemberDates { get; set; }
        public static List<AvailabilityDate> NovemberDates
        {
            get
            {
                if(_novemberDates == null || _novemberDates.Count <= 0)
                {
                    _novemberDates = new List<AvailabilityDate>
                    {
                        #region 16+ dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 4),
                            NumberOfSlots = 4,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 12),
                            NumberOfSlots = 2,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 18),
                            NumberOfSlots = 4,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 25),
                            NumberOfSlots = 4,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        #endregion

                        #region 6 - 15 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 4),
                            NumberOfSlots = 60,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 12),
                            NumberOfSlots = 35,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 18),
                            NumberOfSlots = 60,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 25),
                            NumberOfSlots = 60,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        #endregion

                        #region 5 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 4),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FiveDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 18),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FiveDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 25),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FiveDay
                        },
                        #endregion

                        #region 4 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 4),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FourDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 12),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FourDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 18),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FourDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 25),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FourDay
                        },
                        #endregion

                        #region 3 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 5),
                            NumberOfSlots = 10,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 6),
                            NumberOfSlots = 8,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 13),
                            NumberOfSlots = 10,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 14),
                            NumberOfSlots = 8,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 19),
                            NumberOfSlots = 10,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 20),
                            NumberOfSlots = 8,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 26),
                            NumberOfSlots = 10,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 27),
                            NumberOfSlots = 8,
                            TrialType = TrialType.ThreeDay
                        },
                        #endregion

                        #region 2 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 5),
                            NumberOfSlots = 10,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 6),
                            NumberOfSlots = 14,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 7),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 13),
                            NumberOfSlots = 10,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 14),
                            NumberOfSlots = 14,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 15),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 19),
                            NumberOfSlots = 10,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 20),
                            NumberOfSlots = 14,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 21),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 26),
                            NumberOfSlots = 10,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 27),
                            NumberOfSlots = 14,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 28),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        #endregion

                        #region 1 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 1),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 5),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 6),
                            NumberOfSlots = 18,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 7),
                            NumberOfSlots = 14,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 8),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 13),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 14),
                            NumberOfSlots = 18,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 15),
                            NumberOfSlots = 14,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 16),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 19),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 20),
                            NumberOfSlots = 18,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 21),
                            NumberOfSlots = 14,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 22),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 26),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 27),
                            NumberOfSlots = 18,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 28),
                            NumberOfSlots = 14,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 11, 29),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        #endregion
                    };
                }

                return _novemberDates;
            }
        }

        private static List<AvailabilityDate> _decemberDates { get; set; }
        public static List<AvailabilityDate> DecemberDates 
        {
            get
            {
                if(_decemberDates == null || _decemberDates.Count <= 0)
                {
                    _decemberDates = new List<AvailabilityDate>
                    {
                        #region 16+ dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 2),
                            NumberOfSlots = 2,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 9),
                            NumberOfSlots = 2,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 16),
                            NumberOfSlots = 2,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        #endregion

                        #region 6 - 15 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 2),
                            NumberOfSlots = 35,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 9),
                            NumberOfSlots = 35,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 16),
                            NumberOfSlots = 35,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        #endregion

                        #region 5 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 2),
                            NumberOfSlots = 27,
                            TrialType = TrialType.FiveDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 9),
                            NumberOfSlots = 27,
                            TrialType = TrialType.FiveDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 16),
                            NumberOfSlots = 27,
                            TrialType = TrialType.FiveDay
                        },
                        #endregion

                        #region 4 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 2),
                            NumberOfSlots = 27,
                            TrialType = TrialType.FourDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 9),
                            NumberOfSlots = 27,
                            TrialType = TrialType.FourDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 16),
                            NumberOfSlots = 27,
                            TrialType = TrialType.FourDay
                        },
                        #endregion

                        #region 3 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 3),
                            NumberOfSlots = 6,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 4),
                            NumberOfSlots = 7,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 10),
                            NumberOfSlots = 6,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 11),
                            NumberOfSlots = 7,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 17),
                            NumberOfSlots = 6,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 18),
                            NumberOfSlots = 7,
                            TrialType = TrialType.ThreeDay
                        },
                        #endregion

                        #region 2 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 3),
                            NumberOfSlots = 6,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 4),
                            NumberOfSlots = 9,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 5),
                            NumberOfSlots = 7,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 10),
                            NumberOfSlots = 6,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 11),
                            NumberOfSlots = 9,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 12),
                            NumberOfSlots = 7,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 17),
                            NumberOfSlots = 6,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 18),
                            NumberOfSlots = 9,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 19),
                            NumberOfSlots = 7,
                            TrialType = TrialType.TwoDay
                        },
                        #endregion

                        #region 1 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 3),
                            NumberOfSlots = 6,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 4),
                            NumberOfSlots = 11,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 5),
                            NumberOfSlots = 9,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 6),
                            NumberOfSlots = 7,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 10),
                            NumberOfSlots = 6,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 11),
                            NumberOfSlots = 11,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 12),
                            NumberOfSlots = 9,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 13),
                            NumberOfSlots = 7,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 17),
                            NumberOfSlots = 6,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 18),
                            NumberOfSlots = 11,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 19),
                            NumberOfSlots = 9,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 20),
                            NumberOfSlots = 7,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 23),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 24),
                            NumberOfSlots = 7,
                            TrialType = TrialType.OneDay
                        },
                         new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 27),
                            NumberOfSlots = 6,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 30),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2024, 12, 31),
                            NumberOfSlots = 7,
                            TrialType = TrialType.OneDay
                        },
                        #endregion
                    };
                }

                return _decemberDates;
            }
        }

        private static List<AvailabilityDate> _januaryDates { get; set; }
        public static List<AvailabilityDate> JanuaryDates 
        {
            get
            {
                if(_januaryDates == null || _januaryDates.Count <= 0)
                {
                    _januaryDates = new List<AvailabilityDate>
                    {
                        #region 16+ dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 6),
                            NumberOfSlots = 4,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 13),
                            NumberOfSlots = 4,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 20),
                            NumberOfSlots = 4,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 27),
                            NumberOfSlots = 4,
                            TrialType = TrialType.SixteenPlusDay
                        },
                        #endregion

                        #region 6 - 15 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 6),
                            NumberOfSlots = 60,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 13),
                            NumberOfSlots = 60,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 20),
                            NumberOfSlots = 60,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 27),
                            NumberOfSlots = 60,
                            TrialType = TrialType.SixToFifteenDay
                        },
                        #endregion

                        #region 5 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 6),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FiveDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 13),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FiveDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 20),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FiveDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 27),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FiveDay
                        },
                        #endregion

                        #region 4 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 6),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FourDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 13),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FourDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 20),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FourDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 27),
                            NumberOfSlots = 43,
                            TrialType = TrialType.FourDay
                        },
                        #endregion

                        #region 3 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 7),
                            NumberOfSlots = 10,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 8),
                            NumberOfSlots = 8,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 14),
                            NumberOfSlots = 10,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 15),
                            NumberOfSlots = 8,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 21),
                            NumberOfSlots = 10,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 22),
                            NumberOfSlots = 8,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 28),
                            NumberOfSlots = 10,
                            TrialType = TrialType.ThreeDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 29),
                            NumberOfSlots = 8,
                            TrialType = TrialType.ThreeDay
                        },
                        #endregion

                        #region 2 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 2),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 7),
                            NumberOfSlots = 10,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 8),
                            NumberOfSlots = 14,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 9),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 14),
                            NumberOfSlots = 10,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 15),
                            NumberOfSlots = 14,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 16),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 21),
                            NumberOfSlots = 10,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 22),
                            NumberOfSlots = 14,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 23),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 28),
                            NumberOfSlots = 10,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 29),
                            NumberOfSlots = 14,
                            TrialType = TrialType.TwoDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 30),
                            NumberOfSlots = 8,
                            TrialType = TrialType.TwoDay
                        },
                        #endregion

                        #region 1 dates
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 2),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 3),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 7),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 8),
                            NumberOfSlots = 18,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 9),
                            NumberOfSlots = 14,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 10),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 14),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 15),
                            NumberOfSlots = 18,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 16),
                            NumberOfSlots = 14,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 17),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 21),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 22),
                            NumberOfSlots = 18,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 23),
                            NumberOfSlots = 14,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 24),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 28),
                            NumberOfSlots = 10,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 29),
                            NumberOfSlots = 18,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 30),
                            NumberOfSlots = 14,
                            TrialType = TrialType.OneDay
                        },
                        new AvailabilityDate
                        {
                            AvailabilityParameterId = 1,
                            Date = new DateTime(2025, 1, 31),
                            NumberOfSlots = 8,
                            TrialType = TrialType.OneDay
                        },
                        #endregion
                    };
                }

                return _januaryDates;
            }
        }

        public static void CreateSupplyCSV()
        {
            FileStream fileStream = null;
            string newFilePath = $"{Program.WorkingDirectory}/Outputs/Supply-dates-" + DateTime.Now.ToString("MM-dd-yyyy-H-mm-ss") + ".csv";
            fileStream = new FileStream(newFilePath, FileMode.OpenOrCreate);

            using (var writer = new StreamWriter(fileStream))
            {
                var masterList = new List<AvailabilityDate>();
                masterList.AddRange(AugustDates);
                masterList.AddRange(SeptemberDates);
                masterList.AddRange(OctoberDates);
                masterList.AddRange(NovemberDates);
                masterList.AddRange(DecemberDates);
                masterList.AddRange(JanuaryDates);

                writer.WriteLine(SupplyCSVHeaderFirstRow);
                writer.WriteLine(SupplyCSVHeaderSecondRow);
                
                foreach (var group in masterList.GroupBy(x=>x.Date).OrderBy(x=>x.Key))
                {
                    //get all the slots for a particular date and write them out on a single line
                    string line = "";
                    var matchingDays = group.ToList();
                    for(int x = 1; x <= 7; x++)
                    {
                        var trialTypeDay = matchingDays.Where(y => (int)y.TrialType == x).FirstOrDefault();
                        if (trialTypeDay != null)
                            line += $"{group.Key.ToString("d-MMM-yyyy", DateTimeFormatInfoExtension.DateTimeFormatInfoEx)},{trialTypeDay.NumberOfSlots},";
                        else if (x == 1)
                            line += $"{group.Key.ToString("d-MMM-yyyy", DateTimeFormatInfoExtension.DateTimeFormatInfoEx)},,";
                        else
                            line += ",,";

                    }
                    writer.WriteLine(line.Substring(0, line.Length-1));
                }
            }
        }
    }

    public class AvailabilityDate
    {
        public int AvailabilityParameterId { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfSlots { get; set; }
        public TrialType TrialType { get; set; }
    }
}
