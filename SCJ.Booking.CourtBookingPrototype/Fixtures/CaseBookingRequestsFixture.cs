using SCJ.Booking.CourtBookingPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public static class CaseBookingRequestsFixture
    {
        #region August CaseBookingRequests
        private static List<CaseBookingRequest> _augustCaseBookingRequests;
        public static List<CaseBookingRequest> AugustCaseBookingRequests
        {
            get
            {
                if(_augustCaseBookingRequests == null || _augustCaseBookingRequests.Count <= 0)
                {
                    _augustCaseBookingRequests = new List<CaseBookingRequest>();
                    _augustCaseBookingRequests.AddRange(AugustSixteenPlusDayCaseBookingRequests);
                    _augustCaseBookingRequests.AddRange(AugustFifteenToSixDayCaseBookingRequests);
                    _augustCaseBookingRequests.AddRange(AugustFiveDayCaseBookingRequests);
                    _augustCaseBookingRequests.AddRange(AugustFourDayCaseBookingRequests);
                    _augustCaseBookingRequests.AddRange(AugustThreeDayCaseBookingRequests);
                    _augustCaseBookingRequests.AddRange(AugustTwoDayCaseBookingRequests);
                    _augustCaseBookingRequests.AddRange(AugustOneDayCaseBookingRequests);
                }
                    
                return _augustCaseBookingRequests;
            }
        }

        public static CaseBookingRequest[] AugustSixteenPlusDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 196,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 196,
                BookingPeriodId = 1,
                TrialLength = 16,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 197,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 197,
                BookingPeriodId = 1,
                TrialLength = 19,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 198,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 198,
                BookingPeriodId = 1,
                TrialLength = 20,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 199,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 199,
                BookingPeriodId = 1,
                TrialLength = 25,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] AugustFifteenToSixDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 181,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 181,
                BookingPeriodId = 1,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 182,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 182,
                BookingPeriodId = 15,
                TrialLength = 14,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 183,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 183,
                BookingPeriodId = 1,
                TrialLength = 14,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 184,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 184,
                BookingPeriodId = 1,
                TrialLength = 13,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 185,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 185,
                BookingPeriodId = 1,
                TrialLength = 12,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 186,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 186,
                BookingPeriodId = 1,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 187,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 187,
                BookingPeriodId = 1,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 188,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 188,
                BookingPeriodId = 1,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 189,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 189,
                BookingPeriodId = 1,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 190,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 190,
                BookingPeriodId = 1,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 191,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 191,
                BookingPeriodId = 1,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 192,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 192,
                BookingPeriodId = 1,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 193,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 193,
                BookingPeriodId = 1,
                TrialLength = 9,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 194,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 194,
                BookingPeriodId = 1,
                TrialLength = 9,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 195,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 195,
                BookingPeriodId = 1,
                TrialLength = 8,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] AugustFiveDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 1,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 1,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 2,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 2,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 3,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 3,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 4,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 4,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 5,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 5,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 6,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 6,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 7,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 7,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 8,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 8,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 9,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 9,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 0,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 11,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 1,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 12,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 2,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 13,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 3,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 14,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 4,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 15,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 5,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 16,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 6,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 17,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 7,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 18,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 8,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 19,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 9,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 21,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 21,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 22,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 22,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 23,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 23,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 24,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 24,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 25,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 25,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 26,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 26,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 27,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 27,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 28,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 28,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 29,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 29,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 31,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 31,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 32,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 32,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 33,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 33,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 34,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 34,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 35,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 35,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 36,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 36,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 37,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 37,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 38,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 38,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 39,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 39,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 40,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 40,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 41,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 41,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 42,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 42,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 43,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 43,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 44,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 44,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 45,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 45,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 46,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 46,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 47,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 47,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 48,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 48,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 49,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 49,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 50,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 50,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 51,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 51,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 52,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 52,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 53,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 53,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 54,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 54,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 55,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 55,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 56,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 56,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 57,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 57,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 58,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 58,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 59,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 59,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 60,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 60,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 61,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 61,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 62,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 62,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 63,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 63,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 64,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 64,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 65,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 65,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 66,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 66,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 67,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 67,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 68,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 68,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 69,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 69,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 70,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 70,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 71,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 71,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 72,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 72,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 73,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 73,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 74,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 74,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 75,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 75,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 76,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 76,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 77,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 77,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 78,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 78,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 79,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 79,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 80,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 80,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 81,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 81,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 82,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 82,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 83,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 83,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 84,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 84,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 85,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 85,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 86,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 86,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 87,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 87,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 88,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 88,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 89,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 89,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 90,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 90,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 91,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 91,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 92,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 92,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 93,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 93,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 94,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 94,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 95,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 95,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 96,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 96,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 97,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 97,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 98,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 98,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 99,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 99,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 100,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 100,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 101,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 101,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 102,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 102,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 103,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 103,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 104,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 104,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 105,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 105,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 106,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 106,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 107,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 107,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 108,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 108,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 109,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 109,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 110,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 110,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 111,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 111,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 112,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 112,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 113,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 113,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 114,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 114,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 115,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 115,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 116,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 116,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 117,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 117,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 118,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 118,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 119,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 119,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 120,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 120,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 121,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 121,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 122,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 122,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 123,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 123,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 124,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 124,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 125,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 125,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 126,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 126,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 127,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 127,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 128,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 128,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 129,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 129,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 130,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 130,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 131,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 131,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 132,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 132,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 133,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 133,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 134,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 134,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 135,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 135,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 136,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 136,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 137,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 137,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 138,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 138,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 139,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 139,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 140,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 140,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 141,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 141,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 142,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 142,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 143,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 143,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 144,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 144,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 145,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 145,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 146,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 146,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 147,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 147,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 148,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 148,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 149,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 149,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 150,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 150,
                BookingPeriodId = 1,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] AugustFourDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 151,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 151,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 152,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 152,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 153,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 153,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 154,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 154,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 155,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 155,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 156,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 156,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 157,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 157,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 158,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 158,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 159,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 159,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 160,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 160,
                BookingPeriodId = 1,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] AugustThreeDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 161,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 161,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 162,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 162,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 163,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 163,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 164,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 164,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 165,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 165,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 166,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 166,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 167,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 167,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 168,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 168,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 169,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 169,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 170,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 170,
                BookingPeriodId = 1,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] AugustTwoDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 171,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 171,
                BookingPeriodId = 1,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 172,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 172,
                BookingPeriodId = 1,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 173,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 173,
                BookingPeriodId = 1,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 174,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 174,
                BookingPeriodId = 1,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 175,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 175,
                BookingPeriodId = 1,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] AugustOneDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 176,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 176,
                BookingPeriodId = 1,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 177,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 177,
                BookingPeriodId = 1,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 178,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 178,
                BookingPeriodId = 1,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 179,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 179,
                BookingPeriodId = 1,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 180,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 180,
                BookingPeriodId = 1,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };
        #endregion

        #region September CaseBookingRequests
        private static List<CaseBookingRequest> _septemberCaseBookingRequests;
        public static List<CaseBookingRequest> SeptemberCaseBookingRequests
        {
            get
            {
                if (_septemberCaseBookingRequests == null || _septemberCaseBookingRequests.Count <= 0)
                {
                    _septemberCaseBookingRequests = new List<CaseBookingRequest>();
                    _septemberCaseBookingRequests.AddRange(SeptemberSixteenPlusDayCaseBookingRequests);
                    _septemberCaseBookingRequests.AddRange(SeptemberFifteenToSixDayCaseBookingRequests);
                    _septemberCaseBookingRequests.AddRange(SeptemberFiveDayCaseBookingRequests);
                    _septemberCaseBookingRequests.AddRange(SeptemberFourDayCaseBookingRequests);
                    _septemberCaseBookingRequests.AddRange(SeptemberThreeDayCaseBookingRequests);
                    _septemberCaseBookingRequests.AddRange(SeptemberTwoDayCaseBookingRequests);
                    _septemberCaseBookingRequests.AddRange(SeptemberOneDayCaseBookingRequests);
                }

                return _septemberCaseBookingRequests;
            }
        }

        public static CaseBookingRequest[] SeptemberSixteenPlusDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 396,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 396,
                BookingPeriodId = 2,
                TrialLength = 30,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 397,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 397,
                BookingPeriodId = 2,
                TrialLength = 25,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 398,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 398,
                BookingPeriodId = 2,
                TrialLength = 18,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 399,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 399,
                BookingPeriodId = 2,
                TrialLength = 20,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] SeptemberFifteenToSixDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 381,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 381,
                BookingPeriodId = 2,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 382,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 382,
                BookingPeriodId = 25,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 383,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 383,
                BookingPeriodId = 2,
                TrialLength = 14,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 384,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 384,
                BookingPeriodId = 2,
                TrialLength = 13,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 385,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 385,
                BookingPeriodId = 2,
                TrialLength = 12,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 386,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 386,
                BookingPeriodId = 2,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 387,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 387,
                BookingPeriodId = 2,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 388,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 388,
                BookingPeriodId = 2,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 389,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 389,
                BookingPeriodId = 2,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 390,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 390,
                BookingPeriodId = 2,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 391,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 391,
                BookingPeriodId = 2,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 392,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 392,
                BookingPeriodId = 2,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 393,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 393,
                BookingPeriodId = 2,
                TrialLength = 9,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 394,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 394,
                BookingPeriodId = 2,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 395,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 395,
                BookingPeriodId = 2,
                TrialLength = 8,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] SeptemberFiveDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 201,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 201,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 202,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 202,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 203,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 203,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 204,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 204,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 205,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 205,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 206,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 206,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 207,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 207,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 208,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 208,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 209,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 209,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 210,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 210,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 211,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 211,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 212,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 212,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 213,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 213,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 214,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 214,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 215,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 215,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 216,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 216,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 217,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 217,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 218,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 218,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 219,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 219,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 220,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 220,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 221,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 221,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 222,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 222,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 223,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 223,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 224,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 224,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 225,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 225,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 226,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 226,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 227,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 227,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 228,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 228,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 229,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 229,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 230,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 230,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 231,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 231,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 232,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 232,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 233,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 233,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 234,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 234,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 235,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 235,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 236,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 236,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 237,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 237,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 238,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 238,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 239,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 239,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 240,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 240,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 241,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 241,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 242,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 242,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 243,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 243,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 244,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 244,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 245,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 245,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 246,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 246,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 247,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 247,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 248,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 248,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 249,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 249,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 250,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 250,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 251,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 251,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 252,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 252,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 253,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 253,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 254,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 254,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 255,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 255,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 256,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 256,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 257,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 257,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 258,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 258,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 259,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 259,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 260,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 260,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 261,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 261,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 262,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 262,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 263,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 263,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 264,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 264,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 265,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 265,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 266,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 266,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 267,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 267,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 268,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 268,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 269,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 269,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 270,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 270,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 271,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 271,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 272,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 272,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 273,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 273,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 274,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 274,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 275,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 275,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 276,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 276,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 277,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 277,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 278,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 278,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 279,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 279,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 280,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 280,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 281,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 281,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 282,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 282,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 283,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 283,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 284,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 284,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 285,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 285,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 286,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 286,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 287,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 287,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 288,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 288,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 289,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 289,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 290,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 290,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 291,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 291,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 292,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 292,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 293,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 293,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 294,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 294,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 295,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 295,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 296,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 296,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 297,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 297,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 298,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 298,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 299,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 299,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 300,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 300,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 301,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 301,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 302,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 302,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 303,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 303,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 304,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 304,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 305,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 305,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 306,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 306,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 307,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 307,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 308,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 308,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 309,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 309,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 310,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 310,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 311,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 311,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 312,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 312,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 313,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 313,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 314,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 314,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 315,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 315,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 316,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 316,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 317,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 317,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 318,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 318,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 319,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 319,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 320,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 320,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 321,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 321,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 322,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 322,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 323,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 323,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 324,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 324,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 325,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 325,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 326,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 326,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 327,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 327,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 328,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 328,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 329,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 329,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 330,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 330,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 331,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 331,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 332,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 332,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 333,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 333,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 334,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 334,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 335,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 335,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 336,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 336,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 337,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 337,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 338,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 338,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 339,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 339,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 340,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 340,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 341,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 341,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 342,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 342,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 343,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 343,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 344,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 344,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 345,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 345,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 346,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 346,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 347,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 347,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 348,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 348,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 349,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 349,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 350,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 350,
                BookingPeriodId = 2,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] SeptemberFourDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 351,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 351,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 352,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 352,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 353,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 353,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 354,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 354,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 355,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 355,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 356,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 356,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 357,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 357,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 358,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 358,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 359,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 359,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 360,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 360,
                BookingPeriodId = 2,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            }
        };

        public static CaseBookingRequest[] SeptemberThreeDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 361,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 361,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 362,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 362,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 363,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 363,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 364,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 364,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 365,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 365,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
             new CaseBookingRequest
            {
                Id = 366,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 366,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 367,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 367,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 368,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 368,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 369,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 369,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 370,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 370,
                BookingPeriodId = 2,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] SeptemberTwoDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 371,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 371,
                BookingPeriodId = 2,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 372,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 372,
                BookingPeriodId = 2,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 373,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 373,
                BookingPeriodId = 2,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 374,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 374,
                BookingPeriodId = 2,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 375,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 375,
                BookingPeriodId = 2,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] SeptemberOneDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 376,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 376,
                BookingPeriodId = 2,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 377,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 377,
                BookingPeriodId = 2,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 378,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 378,
                BookingPeriodId = 2,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 379,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 379,
                BookingPeriodId = 2,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 380,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 380,
                BookingPeriodId = 2,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };
        #endregion

        #region October CaseBookingRequests

        #endregion
    }
}
