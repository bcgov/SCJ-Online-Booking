using SCJ.Booking.CourtBookingPrototype.Models;
using SCJ.Booking.MVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public static class CaseBookingRequestsFixture
    {
        public static int MotorVehicleHearingType = 9999;

        //these are the average amount of bookings that we will multiply the demand ratio by, any half values will be rounded up
        public static decimal BaseNumberOfOneDayBookingsPerMonth = 1;
        public static decimal BaseNumberOfTwoDayBookingsPerMonth = 1;
        public static decimal BaseNumberOfThreeDayBookingsPerMonth = 5;
        public static decimal BaseNumberOfFourDayBookingsPerMonth = 5;
        public static decimal BaseNumberOfFiveDayBookingsPerMonth = 122;
        public static decimal BaseNumberOfSixToFifteenDayBookingsPerMonth = 22;
        public static decimal BaseNumberOfSixteenPlusDayBookingsPerMonth = 4;

        //predefined demand ratios for each month as indicated by Lorne
        private static decimal AugustDefaultDemandSupplyRatio = 1.2m;
        private static decimal SeptemberDefaultDemandSupplyRatio = 1.75m;
        private static decimal OctoberDefaultDemandSupplyRatio = 1.75m;
        private static decimal NovemberDefaultDemandSupplyRatio = 1.5m;
        private static decimal DecemberDefaultDemandSupplyRatio = 1.5m;
        private static decimal JanuaryDefaultDemandSupplyRatio = 1.75m;

        //Ids for August start at 1
        #region August CaseBookingRequests
        private static List<CaseBookingRequest> _augustCaseBookingRequests;
        public static List<CaseBookingRequest> AugustCaseBookingRequests
        {
            get
            {
                if(_augustCaseBookingRequests == null || _augustCaseBookingRequests.Count <= 0)
                {
                    int startingId = 1;
                    _augustCaseBookingRequests = new List<CaseBookingRequest>();

                    #region 16+ dates
                    int numberOfSixteenPlusDates = (int)Math.Round(BaseNumberOfSixteenPlusDayBookingsPerMonth * AugustDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixteenPlusDates; x++)
                    {
                        Random r = new Random();
                        _augustCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 1,
                            TrialLength = r.Next(16, 30),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 6 to 15 dates
                    int numberOfSixToFifteenDates = (int)Math.Round(BaseNumberOfSixToFifteenDayBookingsPerMonth * AugustDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixToFifteenDates; x++)
                    {
                        Random r = new Random();
                        _augustCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 1,
                            TrialLength = r.Next(6, 15),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 5 dates
                    int numberOfFiveDates = (int)Math.Round(BaseNumberOfFiveDayBookingsPerMonth * AugustDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFiveDates; x++)
                    {
                        _augustCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 1,
                            TrialLength = 5,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 4 dates
                    int numberOfFourDates = (int)Math.Round(BaseNumberOfFourDayBookingsPerMonth * AugustDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFourDates; x++)
                    {
                        _augustCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 1,
                            TrialLength = 4,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 3 dates
                    int numberOfThreeDates = (int)Math.Round(BaseNumberOfThreeDayBookingsPerMonth * AugustDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfThreeDates; x++)
                    {
                        _augustCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 1,
                            TrialLength = 3,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 2 dates
                    int numberOfTwoDates = (int)Math.Round(BaseNumberOfTwoDayBookingsPerMonth * AugustDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfTwoDates; x++)
                    {
                        _augustCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 1,
                            TrialLength = 2,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 1 dates
                    int numberOfOneDates = (int)Math.Round(BaseNumberOfOneDayBookingsPerMonth * AugustDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfOneDates; x++)
                    {
                        _augustCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 1,
                            TrialLength = 1,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion
                }

                return _augustCaseBookingRequests;
            }
        }
        #endregion

        //Ids for September start at 10000
        #region September CaseBookingRequests
        private static List<CaseBookingRequest> _septemberCaseBookingRequests;
        public static List<CaseBookingRequest> SeptemberCaseBookingRequests
        {
            get
            {
                if (_septemberCaseBookingRequests == null || _septemberCaseBookingRequests.Count <= 0)
                {
                    int startingId = 10000;
                    _septemberCaseBookingRequests = new List<CaseBookingRequest>();

                    #region 16+ dates
                    int numberOfSixteenPlusDates = (int)Math.Round(BaseNumberOfSixteenPlusDayBookingsPerMonth * SeptemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixteenPlusDates; x++)
                    {
                        Random r = new Random();
                        _septemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 2,
                            TrialLength = r.Next(16, 30),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 6 to 15 dates
                    int numberOfSixToFifteenDates = (int)Math.Round(BaseNumberOfSixToFifteenDayBookingsPerMonth * SeptemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixToFifteenDates; x++)
                    {
                        Random r = new Random();
                        _septemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 2,
                            TrialLength = r.Next(6, 15),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 5 dates
                    int numberOfFiveDates = (int)Math.Round(BaseNumberOfFiveDayBookingsPerMonth * SeptemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFiveDates; x++)
                    {
                        _septemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 2,
                            TrialLength = 5,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 4 dates
                    int numberOfFourDates = (int)Math.Round(BaseNumberOfFourDayBookingsPerMonth * SeptemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFourDates; x++)
                    {
                        _septemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 2,
                            TrialLength = 4,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 3 dates
                    int numberOfThreeDates = (int)Math.Round(BaseNumberOfThreeDayBookingsPerMonth * SeptemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfThreeDates; x++)
                    {
                        _septemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 2,
                            TrialLength = 3,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 2 dates
                    int numberOfTwoDates = (int)Math.Round(BaseNumberOfTwoDayBookingsPerMonth * SeptemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfTwoDates; x++)
                    {
                        _septemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 2,
                            TrialLength = 2,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 1 dates
                    int numberOfOneDates = (int)Math.Round(BaseNumberOfOneDayBookingsPerMonth * SeptemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfOneDates; x++)
                    {
                        _septemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 2,
                            TrialLength = 1,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion
                }

                return _septemberCaseBookingRequests;
            }
        }
        #endregion

        //Ids for October start at 20000
        #region October CaseBookingRequests
        private static List<CaseBookingRequest> _octoberCaseBookingRequests;
        public static List<CaseBookingRequest> OctoberCaseBookingRequests
        {
            get
            {
                if (_octoberCaseBookingRequests == null || _octoberCaseBookingRequests.Count <= 0)
                {
                    int startingId = 20000;
                    _octoberCaseBookingRequests = new List<CaseBookingRequest>();

                    #region 16+ dates
                    int numberOfSixteenPlusDates = (int)Math.Round(BaseNumberOfSixteenPlusDayBookingsPerMonth * OctoberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixteenPlusDates; x++)
                    {
                        Random r = new Random();
                        _octoberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 3,
                            TrialLength = r.Next(16, 30),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 6 to 15 dates
                    int numberOfSixToFifteenDates = (int)Math.Round(BaseNumberOfSixToFifteenDayBookingsPerMonth * OctoberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixToFifteenDates; x++)
                    {
                        Random r = new Random();
                        _octoberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 3,
                            TrialLength = r.Next(6, 15),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 5 dates
                    int numberOfFiveDates = (int)Math.Round(BaseNumberOfFiveDayBookingsPerMonth * OctoberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFiveDates; x++)
                    {
                        _octoberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 3,
                            TrialLength = 5,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 4 dates
                    int numberOfFourDates = (int)Math.Round(BaseNumberOfFourDayBookingsPerMonth * OctoberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFourDates; x++)
                    {
                        _octoberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 3,
                            TrialLength = 4,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 3 dates
                    int numberOfThreeDates = (int)Math.Round(BaseNumberOfThreeDayBookingsPerMonth * OctoberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfThreeDates; x++)
                    {
                        _octoberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 3,
                            TrialLength = 3,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 2 dates
                    int numberOfTwoDates = (int)Math.Round(BaseNumberOfTwoDayBookingsPerMonth * OctoberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfTwoDates; x++)
                    {
                        _octoberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 3,
                            TrialLength = 2,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 1 dates
                    int numberOfOneDates = (int)Math.Round(BaseNumberOfOneDayBookingsPerMonth * OctoberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfOneDates; x++)
                    {
                        _octoberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 3,
                            TrialLength = 1,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion
                }

                return _octoberCaseBookingRequests;
            }
        }
        #endregion

        //Ids for November start at 30000
        #region November CaseBookingRequests
        private static List<CaseBookingRequest> _novemberCaseBookingRequests;
        public static List<CaseBookingRequest> NovemberCaseBookingRequests
        {
            get
            {
                if (_novemberCaseBookingRequests == null || _novemberCaseBookingRequests.Count <= 0)
                {
                    int startingId = 30000;
                    _novemberCaseBookingRequests = new List<CaseBookingRequest>();

                    #region 16+ dates
                    int numberOfSixteenPlusDates = (int)Math.Round(BaseNumberOfSixteenPlusDayBookingsPerMonth * NovemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixteenPlusDates; x++)
                    {
                        Random r = new Random();
                        _novemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 4,
                            TrialLength = r.Next(16, 30),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 6 to 15 dates
                    int numberOfSixToFifteenDates = (int)Math.Round(BaseNumberOfSixToFifteenDayBookingsPerMonth * NovemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixToFifteenDates; x++)
                    {
                        Random r = new Random();
                        _novemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 4,
                            TrialLength = r.Next(6, 15),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 5 dates
                    int numberOfFiveDates = (int)Math.Round(BaseNumberOfFiveDayBookingsPerMonth * NovemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFiveDates; x++)
                    {
                        _novemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 4,
                            TrialLength = 5,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 4 dates
                    int numberOfFourDates = (int)Math.Round(BaseNumberOfFourDayBookingsPerMonth * NovemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFourDates; x++)
                    {
                        _novemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 4,
                            TrialLength = 4,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 3 dates
                    int numberOfThreeDates = (int)Math.Round(BaseNumberOfThreeDayBookingsPerMonth * NovemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfThreeDates; x++)
                    {
                        _novemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 4,
                            TrialLength = 3,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 2 dates
                    int numberOfTwoDates = (int)Math.Round(BaseNumberOfTwoDayBookingsPerMonth * NovemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfTwoDates; x++)
                    {
                        _novemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 4,
                            TrialLength = 2,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 1 dates
                    int numberOfOneDates = (int)Math.Round(BaseNumberOfOneDayBookingsPerMonth * NovemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfOneDates; x++)
                    {
                        _novemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 4,
                            TrialLength = 1,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion
                }

                return _novemberCaseBookingRequests;
            }
        }
        #endregion

        //Ids for December start at 40000
        #region December CaseBookingRequests
        private static List<CaseBookingRequest> _decemberCaseBookingRequests;
        public static List<CaseBookingRequest> DecemberCaseBookingRequests
        {
            get
            {
                if (_decemberCaseBookingRequests == null || _decemberCaseBookingRequests.Count <= 0)
                {
                    int startingId = 40000;
                    _decemberCaseBookingRequests = new List<CaseBookingRequest>();

                    #region 16+ dates
                    int numberOfSixteenPlusDates = (int)Math.Round(BaseNumberOfSixteenPlusDayBookingsPerMonth * DecemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixteenPlusDates; x++)
                    {
                        Random r = new Random();
                        _decemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 5,
                            TrialLength = r.Next(16, 30),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 6 to 15 dates
                    int numberOfSixToFifteenDates = (int)Math.Round(BaseNumberOfSixToFifteenDayBookingsPerMonth * DecemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixToFifteenDates; x++)
                    {
                        Random r = new Random();
                        _decemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 5,
                            TrialLength = r.Next(6, 15),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 5 dates
                    int numberOfFiveDates = (int)Math.Round(BaseNumberOfFiveDayBookingsPerMonth * DecemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFiveDates; x++)
                    {
                        _decemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 5,
                            TrialLength = 5,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 4 dates
                    int numberOfFourDates = (int)Math.Round(BaseNumberOfFourDayBookingsPerMonth * DecemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFourDates; x++)
                    {
                        _decemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 5,
                            TrialLength = 4,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 3 dates
                    int numberOfThreeDates = (int)Math.Round(BaseNumberOfThreeDayBookingsPerMonth * DecemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfThreeDates; x++)
                    {
                        _decemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 5,
                            TrialLength = 3,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 2 dates
                    int numberOfTwoDates = (int)Math.Round(BaseNumberOfTwoDayBookingsPerMonth * DecemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfTwoDates; x++)
                    {
                        _decemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 5,
                            TrialLength = 2,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 1 dates
                    int numberOfOneDates = (int)Math.Round(BaseNumberOfOneDayBookingsPerMonth * DecemberDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfOneDates; x++)
                    {
                        _decemberCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 5,
                            TrialLength = 1,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion
                }

                return _decemberCaseBookingRequests;
            }
        }

        
        #endregion

        //Ids for January start at 50000
        #region January CaseBookingRequests
        private static List<CaseBookingRequest> _januaryCaseBookingRequests;
        public static List<CaseBookingRequest> JanuaryCaseBookingRequests
        {
            get
            {
                if (_januaryCaseBookingRequests == null || _januaryCaseBookingRequests.Count <= 0)
                {
                    int startingId = 50000;
                    _januaryCaseBookingRequests = new List<CaseBookingRequest>();

                    #region 16+ dates
                    int numberOfSixteenPlusDates = (int)Math.Round(BaseNumberOfSixteenPlusDayBookingsPerMonth * JanuaryDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixteenPlusDates; x++)
                    {
                        Random r = new Random();
                        _januaryCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 6,
                            TrialLength = r.Next(16, 30),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 6 to 15 dates
                    int numberOfSixToFifteenDates = (int)Math.Round(BaseNumberOfSixToFifteenDayBookingsPerMonth * JanuaryDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfSixToFifteenDates; x++)
                    {
                        Random r = new Random();
                        _januaryCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 6,
                            TrialLength = r.Next(6, 15),
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 5 dates
                    int numberOfFiveDates = (int)Math.Round(BaseNumberOfFiveDayBookingsPerMonth * JanuaryDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFiveDates; x++)
                    {
                        _januaryCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 6,
                            TrialLength = 5,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 4 dates
                    int numberOfFourDates = (int)Math.Round(BaseNumberOfFourDayBookingsPerMonth * JanuaryDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfFourDates; x++)
                    {
                        _januaryCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 6,
                            TrialLength = 4,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 3 dates
                    int numberOfThreeDates = (int)Math.Round(BaseNumberOfThreeDayBookingsPerMonth * JanuaryDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfThreeDates; x++)
                    {
                        _januaryCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 6,
                            TrialLength = 3,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 2 dates
                    int numberOfTwoDates = (int)Math.Round(BaseNumberOfTwoDayBookingsPerMonth * JanuaryDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfTwoDates; x++)
                    {
                        _januaryCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 6,
                            TrialLength = 2,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion

                    #region 1 dates
                    int numberOfOneDates = (int)Math.Round(BaseNumberOfOneDayBookingsPerMonth * JanuaryDefaultDemandSupplyRatio);
                    for (int x = 0; x < numberOfOneDates; x++)
                    {
                        _januaryCaseBookingRequests.Add(new CaseBookingRequest
                        {
                            Id = startingId,
                            SmGovUserGuid = Guid.NewGuid(),
                            PhysicalFileId = startingId,
                            BookingPeriodId = 6,
                            TrialLength = 1,
                            HearingType = MotorVehicleHearingType,
                            Email = "",
                            Phone = ""
                        });
                        startingId++;
                    }
                    #endregion
                }

                return _januaryCaseBookingRequests;
            }
        }
        #endregion
    }
}
