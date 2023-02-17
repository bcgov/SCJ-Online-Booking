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
            new CaseBookingRequest
            {
                Id = 281,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 281,
                BookingPeriodId = 2,
                TrialLength = 2,
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
                TrialLength = 2,
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
                TrialLength = 2,
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
                TrialLength = 2,
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

        //Ids for October start at 10000
        #region October CaseBookingRequests
        private static List<CaseBookingRequest> _octoberCaseBookingRequests;
        public static List<CaseBookingRequest> OctoberCaseBookingRequests
        {
            get
            {
                if (_octoberCaseBookingRequests == null || _octoberCaseBookingRequests.Count <= 0)
                {
                    _octoberCaseBookingRequests = new List<CaseBookingRequest>();
                    _octoberCaseBookingRequests.AddRange(OctoberSixteenPlusDayCaseBookingRequests);
                    _octoberCaseBookingRequests.AddRange(OctoberFifteenToSixDayCaseBookingRequests);
                    _octoberCaseBookingRequests.AddRange(OctoberFiveDayCaseBookingRequests);
                    _octoberCaseBookingRequests.AddRange(OctoberFourDayCaseBookingRequests);
                    _octoberCaseBookingRequests.AddRange(OctoberThreeDayCaseBookingRequests);
                    _octoberCaseBookingRequests.AddRange(OctoberTwoDayCaseBookingRequests);
                    _octoberCaseBookingRequests.AddRange(OctoberOneDayCaseBookingRequests);
                }

                return _octoberCaseBookingRequests;
            }
        }

        public static CaseBookingRequest[] OctoberSixteenPlusDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 10000,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10000,
                BookingPeriodId = 4,
                TrialLength = 30,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10001,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10001,
                BookingPeriodId = 4,
                TrialLength = 25,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10002,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10002,
                BookingPeriodId = 4,
                TrialLength = 18,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10003,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10003,
                BookingPeriodId = 4,
                TrialLength = 20,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] OctoberFifteenToSixDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 10081,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10081,
                BookingPeriodId = 4,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10082,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10082,
                BookingPeriodId = 4,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10083,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10083,
                BookingPeriodId = 4,
                TrialLength = 14,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10084,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10084,
                BookingPeriodId = 4,
                TrialLength = 13,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10085,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10085,
                BookingPeriodId = 4,
                TrialLength = 12,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10086,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10086,
                BookingPeriodId = 4,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10087,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10087,
                BookingPeriodId = 4,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10088,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10088,
                BookingPeriodId = 4,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10089,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10089,
                BookingPeriodId = 4,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10090,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10090,
                BookingPeriodId = 4,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10091,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10091,
                BookingPeriodId = 4,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10092,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10092,
                BookingPeriodId = 4,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10093,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10093,
                BookingPeriodId = 4,
                TrialLength = 9,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10094,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10094,
                BookingPeriodId = 4,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10095,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10095,
                BookingPeriodId = 4,
                TrialLength = 8,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] OctoberFiveDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 10201,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10201,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10202,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10202,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10203,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10203,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10204,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10204,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10205,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10205,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10206,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10206,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10207,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10207,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10208,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10208,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10209,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10209,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10210,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10210,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10211,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10211,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10212,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10212,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10213,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10213,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10214,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10214,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10215,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10215,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10216,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10216,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10217,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10217,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10218,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10218,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10219,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10219,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10220,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10220,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10221,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10221,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10222,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10222,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10223,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10223,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10224,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10224,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10225,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10225,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10226,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10226,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10227,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10227,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10228,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10228,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10229,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10229,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10230,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10230,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10231,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10231,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10232,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10232,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10233,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10233,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10234,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10234,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10235,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10235,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10236,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10236,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10237,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10237,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10238,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10238,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10239,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10239,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10240,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10240,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10241,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10241,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10242,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10242,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10243,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10243,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10244,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10244,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10245,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10245,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10246,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10246,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10247,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10247,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10248,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10248,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10249,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10249,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10250,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10250,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10251,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10251,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10252,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10252,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10253,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10253,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10254,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10254,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10255,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10255,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10256,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10256,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10257,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10257,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10258,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10258,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10259,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10259,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10260,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10260,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10261,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10261,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10262,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10262,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10263,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10263,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10264,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10264,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10265,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10265,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10266,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10266,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10267,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10267,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10268,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10268,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10269,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10269,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10270,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10270,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10271,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10271,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10272,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10272,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10273,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10273,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10274,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10274,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10275,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10275,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10276,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10276,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10277,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10277,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10278,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10278,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10279,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10279,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10280,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10280,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] OctoberFourDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 10351,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10351,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10352,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10352,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10353,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10353,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10354,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10354,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10355,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10355,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10356,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10356,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10357,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10357,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10358,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10358,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10359,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10359,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10360,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10360,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            }
        };

        public static CaseBookingRequest[] OctoberThreeDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 10361,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10361,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10362,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10362,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10363,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10363,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10364,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10364,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10365,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10365,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
             new CaseBookingRequest
            {
                Id = 10366,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10366,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10367,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10367,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10368,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10368,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10369,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10369,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10370,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10370,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] OctoberTwoDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 10371,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10371,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10372,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10372,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10373,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10373,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10374,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10374,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10375,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10375,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10281,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10281,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10272,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10272,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10273,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10273,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10274,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10274,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10275,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10275,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] OctoberOneDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 10376,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10376,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10377,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10377,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10378,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10378,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10379,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10379,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 10380,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 10380,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };
        #endregion

        //Ids for November start at 20000
        #region November CaseBookingRequests
        private static List<CaseBookingRequest> _novemberCaseBookingRequests;
        public static List<CaseBookingRequest> NovemberCaseBookingRequests
        {
            get
            {
                if (_novemberCaseBookingRequests == null || _novemberCaseBookingRequests.Count <= 0)
                {
                    _novemberCaseBookingRequests = new List<CaseBookingRequest>();
                    _novemberCaseBookingRequests.AddRange(NovemberSixteenPlusDayCaseBookingRequests);
                    _novemberCaseBookingRequests.AddRange(NovemberFifteenToSixDayCaseBookingRequests);
                    _novemberCaseBookingRequests.AddRange(NovemberFiveDayCaseBookingRequests);
                    _novemberCaseBookingRequests.AddRange(NovemberFourDayCaseBookingRequests);
                    _novemberCaseBookingRequests.AddRange(NovemberThreeDayCaseBookingRequests);
                    _novemberCaseBookingRequests.AddRange(NovemberTwoDayCaseBookingRequests);
                    _novemberCaseBookingRequests.AddRange(NovemberOneDayCaseBookingRequests);
                }

                return _novemberCaseBookingRequests;
            }
        }

        public static CaseBookingRequest[] NovemberSixteenPlusDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 20000,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20000,
                BookingPeriodId = 4,
                TrialLength = 30,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20001,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20001,
                BookingPeriodId = 4,
                TrialLength = 25,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20002,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20002,
                BookingPeriodId = 4,
                TrialLength = 18,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20003,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20003,
                BookingPeriodId = 4,
                TrialLength = 20,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] NovemberFifteenToSixDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 20081,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20081,
                BookingPeriodId = 4,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20082,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20082,
                BookingPeriodId = 4,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20083,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20083,
                BookingPeriodId = 4,
                TrialLength = 14,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20084,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20084,
                BookingPeriodId = 4,
                TrialLength = 13,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20085,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20085,
                BookingPeriodId = 4,
                TrialLength = 12,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20086,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20086,
                BookingPeriodId = 4,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20087,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20087,
                BookingPeriodId = 4,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20088,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20088,
                BookingPeriodId = 4,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20089,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20089,
                BookingPeriodId = 4,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20090,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20090,
                BookingPeriodId = 4,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20091,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20091,
                BookingPeriodId = 4,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20092,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20092,
                BookingPeriodId = 4,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20093,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20093,
                BookingPeriodId = 4,
                TrialLength = 9,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20094,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20094,
                BookingPeriodId = 4,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20095,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20095,
                BookingPeriodId = 4,
                TrialLength = 8,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20248,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20248,
                BookingPeriodId = 4,
                TrialLength = 8,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20249,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20249,
                BookingPeriodId = 4,
                TrialLength = 8,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20250,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20250,
                BookingPeriodId = 4,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20251,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20251,
                BookingPeriodId = 4,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20252,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20252,
                BookingPeriodId = 4,
                TrialLength = 9,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20253,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20253,
                BookingPeriodId = 4,
                TrialLength = 9,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20254,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20254,
                BookingPeriodId = 4,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20255,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20255,
                BookingPeriodId = 4,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20256,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20256,
                BookingPeriodId = 4,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20257,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20257,
                BookingPeriodId = 4,
                TrialLength = 14,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20258,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20258,
                BookingPeriodId = 4,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20259,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20259,
                BookingPeriodId = 4,
                TrialLength = 12,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20260,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20260,
                BookingPeriodId = 4,
                TrialLength = 13,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] NovemberFiveDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 20201,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20201,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20202,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20202,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20203,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20203,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20204,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20204,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20205,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20205,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20206,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20206,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20207,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20207,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20208,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20208,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20209,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20209,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20210,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20210,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20211,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20211,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20212,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20212,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20213,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20213,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20214,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20214,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20215,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20215,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20216,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20216,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20217,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20217,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20218,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20218,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20219,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20219,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20220,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20220,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20221,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20221,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20222,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20222,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20223,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20223,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20224,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20224,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20225,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20225,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20226,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20226,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20227,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20227,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20228,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20228,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20229,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20229,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20230,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20230,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20231,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20231,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20232,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20232,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20233,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20233,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20234,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20234,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20235,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20235,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20236,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20236,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20237,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20237,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20238,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20238,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20239,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20239,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20240,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20240,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20241,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20241,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20242,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20242,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20243,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20243,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20244,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20244,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20245,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20245,
                BookingPeriodId = 4,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] NovemberFourDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 20351,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20351,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20352,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20352,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20353,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20353,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20354,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20354,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20355,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20355,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20356,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20356,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20357,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20357,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20358,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20358,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20359,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20359,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20360,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20360,
                BookingPeriodId = 4,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            }
        };

        public static CaseBookingRequest[] NovemberThreeDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 20361,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20361,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20362,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20362,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20363,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20363,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20364,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20364,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20365,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20365,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
             new CaseBookingRequest
            {
                Id = 20366,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20366,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20367,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20367,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20368,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20368,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20369,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20369,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20370,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20370,
                BookingPeriodId = 4,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] NovemberTwoDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 20371,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20371,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20372,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20372,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20373,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20373,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20374,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20374,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20375,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20375,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20281,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20281,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20272,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20272,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20273,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20273,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20274,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20274,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20275,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20275,
                BookingPeriodId = 4,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] NovemberOneDayCaseBookingRequests =
        {

            new CaseBookingRequest
            {
                Id = 20271,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20271,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20272,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20272,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20273,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20273,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20274,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20274,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20275,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20275,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20276,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20276,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20277,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20277,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20278,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20278,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20279,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20279,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20280,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20280,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20376,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20376,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20377,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20377,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20378,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20378,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20379,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20379,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 20380,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 20380,
                BookingPeriodId = 4,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };
        #endregion

        //Ids for December start at 30000
        #region December CaseBookingRequests
        private static List<CaseBookingRequest> _decemberCaseBookingRequests;
        public static List<CaseBookingRequest> DecemberCaseBookingRequests
        {
            get
            {
                if (_decemberCaseBookingRequests == null || _decemberCaseBookingRequests.Count <= 0)
                {
                    _decemberCaseBookingRequests = new List<CaseBookingRequest>();
                    _decemberCaseBookingRequests.AddRange(DecemberSixteenPlusDayCaseBookingRequests);
                    _decemberCaseBookingRequests.AddRange(DecemberFifteenToSixDayCaseBookingRequests);
                    _decemberCaseBookingRequests.AddRange(DecemberFiveDayCaseBookingRequests);
                    _decemberCaseBookingRequests.AddRange(DecemberFourDayCaseBookingRequests);
                    _decemberCaseBookingRequests.AddRange(DecemberThreeDayCaseBookingRequests);
                    _decemberCaseBookingRequests.AddRange(DecemberTwoDayCaseBookingRequests);
                    _decemberCaseBookingRequests.AddRange(DecemberOneDayCaseBookingRequests);
                }

                return _decemberCaseBookingRequests;
            }
        }

        public static CaseBookingRequest[] DecemberSixteenPlusDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 30000,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30000,
                BookingPeriodId = 5,
                TrialLength = 30,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30001,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30001,
                BookingPeriodId = 5,
                TrialLength = 25,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30002,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30002,
                BookingPeriodId = 5,
                TrialLength = 18,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30003,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30003,
                BookingPeriodId = 5,
                TrialLength = 20,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] DecemberFifteenToSixDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 30081,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30081,
                BookingPeriodId = 5,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30082,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30082,
                BookingPeriodId = 5,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30083,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30083,
                BookingPeriodId = 5,
                TrialLength = 14,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30084,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30084,
                BookingPeriodId = 5,
                TrialLength = 13,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30085,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30085,
                BookingPeriodId = 5,
                TrialLength = 12,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30086,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30086,
                BookingPeriodId = 5,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30087,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30087,
                BookingPeriodId = 5,
                TrialLength = 11,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30088,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30088,
                BookingPeriodId = 5,
                TrialLength = 10,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30089,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30089,
                BookingPeriodId = 5,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30090,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30090,
                BookingPeriodId = 5,
                TrialLength = 6,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30091,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30091,
                BookingPeriodId = 5,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30092,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30092,
                BookingPeriodId = 5,
                TrialLength = 7,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30093,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30093,
                BookingPeriodId = 5,
                TrialLength = 9,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30094,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30094,
                BookingPeriodId = 5,
                TrialLength = 15,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30095,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30095,
                BookingPeriodId = 5,
                TrialLength = 8,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] DecemberFiveDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 30201,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30201,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30202,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30202,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30203,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30203,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30204,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30204,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30205,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30205,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30206,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30206,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30207,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30207,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30208,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30208,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30209,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30209,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30210,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30210,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30211,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30211,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30212,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30212,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30213,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30213,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30214,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30214,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30215,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30215,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30216,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30216,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30217,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30217,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30218,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30218,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30219,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30219,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30220,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30220,
                BookingPeriodId = 5,
                TrialLength = 5,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] DecemberFourDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 30351,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30351,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30352,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30352,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30353,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30353,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30354,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30354,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30355,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30355,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30356,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30356,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30357,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30357,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30358,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30358,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30359,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30359,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30360,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30360,
                BookingPeriodId = 5,
                TrialLength = 4,
                HearingType = 9999,
                Email = "",
                Phone = ""
            }
        };

        public static CaseBookingRequest[] DecemberThreeDayCaseBookingRequests =
        {

            new CaseBookingRequest
            {
                Id = 30261,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30261,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30262,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30262,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30263,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30263,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30264,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30264,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30265,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30265,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30266,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30266,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30267,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30267,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30268,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30268,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30269,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30269,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30270,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30270,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30271,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30271,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30272,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30272,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30273,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30273,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30274,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30274,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30275,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30275,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30276,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30276,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30277,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30277,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30278,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30278,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30279,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30279,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30280,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30280,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30361,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30361,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30362,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30362,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30363,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30363,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30364,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30364,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30365,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30365,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
             new CaseBookingRequest
            {
                Id = 30366,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30366,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30367,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30367,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30368,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30368,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30369,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30369,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30370,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30370,
                BookingPeriodId = 5,
                TrialLength = 3,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] DecemberTwoDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 30221,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30221,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30222,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30222,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30223,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30223,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30224,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30224,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30225,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30225,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30226,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30226,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30227,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30227,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30228,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30228,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30229,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30229,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30230,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30230,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30231,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30231,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30232,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30232,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30233,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30233,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30234,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30234,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30235,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30235,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30236,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30236,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30237,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30237,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30238,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30238,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30239,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30239,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30240,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30240,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30371,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30371,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30372,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30372,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30373,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30373,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30374,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30374,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30375,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30375,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30281,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30281,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30272,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30272,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30273,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30273,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30274,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30274,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30275,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30275,
                BookingPeriodId = 5,
                TrialLength = 2,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };

        public static CaseBookingRequest[] DecemberOneDayCaseBookingRequests =
        {
            new CaseBookingRequest
            {
                Id = 30376,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30376,
                BookingPeriodId = 5,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30377,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30377,
                BookingPeriodId = 5,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30378,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30378,
                BookingPeriodId = 5,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30379,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30379,
                BookingPeriodId = 5,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
            new CaseBookingRequest
            {
                Id = 30380,
                SmGovUserGuid = Guid.NewGuid(),
                PhysicalFileId = 30380,
                BookingPeriodId = 5,
                TrialLength = 1,
                HearingType = 9999,
                Email = "",
                Phone = ""
            },
        };
        #endregion
    }
}
