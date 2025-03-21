using System;
using System.Linq;
using SCJ.OnlineBooking;
using Xunit;

namespace SCJ.Booking.UnitTest
{
    public class RemoteApiTests
    {
        public RemoteApiTests()
        {
            _soapClient = new FakeOnlineBookingClient();

            // To connect these tests to the real API:
            //  1. Comment out the line above that creates a FakeOnlineBookingClient
            //  2. Uncomment the 3 lines below
            //  3. Add 3 settings to test/.env : API_ENDPOINT, API_USERNAME, API_PASSWORD and change USE_FAKE_API to false

            // new EnvLoader().Load();
            // IConfiguration configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            // _soapClient = OnlineBookingClientFactory.GetClient(configuration);
        }

        private readonly IOnlineBooking _soapClient;

        [Fact]
        public void AvailableDatesByLocation()
        {
            const int vancouver = 1;
            const int victoria = 2;
            const int trialManagementConference = 9090;

            AvailableDatesByLocation vancouverResults = _soapClient
                .AvailableDatesByLocationAsync(vancouver, trialManagementConference)
                .Result;

            Assert.NotNull(vancouverResults);

            AvailableDatesByLocation victoriaResults = _soapClient
                .AvailableDatesByLocationAsync(victoria, trialManagementConference)
                .Result;

            Assert.NotNull(victoriaResults);
        }

        [Fact]
        public void BookingHearingFail()
        {
            // this booking will fail because the date is too close in the future

            const int trialManagementConference = 9090;

            var booking = new BookHearingInfo
            {
                CEIS_Physical_File_ID = 3879m,
                containerID = 305291,
                dateTime = new DateTime(2019, 1, 22, 11, 45, 0),
                hearingLength = 30,
                hearingTypeId = trialManagementConference,
                locationID = 1,
                requestedBy = "John Smith 604-555-1212 somebody@email.com"
            };

            BookingHearingResult result = _soapClient.BookingHearingAsync(booking).Result;

            Assert.NotNull(result);
        }

        [Fact]
        public void BookingHearingSuccess()
        {
            const int trialManagementConference = 9090;

            var booking = new BookHearingInfo
            {
                CEIS_Physical_File_ID = 3879m,
                containerID = 305273,
                dateTime = new DateTime(2019, 2, 25, 11, 45, 0),
                hearingLength = 30,
                hearingTypeId = trialManagementConference,
                locationID = 1,
                requestedBy = "John Smith 604-555-1212 somebody@email.com"
            };

            BookingHearingResult result = _soapClient.BookingHearingAsync(booking).Result;

            Assert.NotNull(result);
        }

        [Fact]
        public void CaseNumberInvalid()
        {
            CourtFile[] searchResults = _soapClient.caseNumberValidAsync("VAM14761").Result;

            // If no case is found then 0 is returned
            Assert.True(searchResults.Length == 0);
        }

        [Fact]
        public void CaseNumberValid()
        {
            CourtFile[] searchResults = _soapClient.caseNumberValidAsync("KEM111").Result;

            Assert.True(searchResults.Length > 0);
        }

        [Fact]
        public void GetLocations()
        {
            Location[] locations = _soapClient.getLocationsAsync().Result;

            Assert.NotEmpty(locations);
        }

        [Fact]
        public void CoACaseNumberValidAsync_Civil()
        {
            COACaseList result = _soapClient.CoACaseNumberValidAsync("CA39029").Result;
            Assert.NotNull(result);
            Assert.True(result.CaseList[0].CaseId == 37351);
            Assert.True(result.CaseType == "Civil");
        }

        [Fact]
        public void CoACaseNumberValidAsync_Criminal()
        {
            COACaseList result = _soapClient.CoACaseNumberValidAsync("CA42024").Result;
            Assert.NotNull(result);
            Assert.True(result.CaseList[0].CaseId == 40368);
            Assert.True(result.CaseType == "Criminal");
        }

        [Fact]
        public void CoACaseNumberValidAsync_Invalid()
        {
            COACaseList result = _soapClient.CoACaseNumberValidAsync("12345").Result;
            Assert.NotNull(result);
            Assert.True(result.CaseList.Length == 1);
            Assert.True(result.CaseType == "Not Found");
        }

        [Fact]
        public void COAAvailableDatesAsync()
        {
            CoAAvailableDates result = _soapClient.COAAvailableDatesAsync().Result;
            Assert.NotNull(result);
            Assert.True(result.AvailableDates.Any());
        }

        [Fact]
        public void CoAQueueHearingAsync()
        {
            // civil case
            // case number = "CA39029"
            // case id = 37351

            // hearingLength = "Half" or "Full" (not case sensitive)

            // criminal case
            // case number = "CA42024"
            // case id = 40368

            // Hearing Type
            // - civil hearing of appeal = 24
            // - criminal hearing of appeal = 72
            // - criminal conviction appeal = 96
            // - criminal sentence appeal = 97

            var booking = new CoABookingHearingInfo
            {
                caseID = 37351,
                email = "somebody@email.com",
                hearingDate = DateTime.Parse("2024/03/12 00:00:00.0000000"),
                hearingLength = "Full",
                phone = "604-555-1212",
                hearingTypeId = 24,
                requestedBy = "John Smith",
                MainCase = true,
                RelatedCases = ""
            };

            _ = _soapClient.CoAQueueHearingAsync(booking).Result;
        }

        [Fact]
        public void CoAChambersApplicationsListAsync()
        {
            // types: Civil, Criminal
            // 255 is the maximum character limit for the definition
            // 255 is the limit for names too, but the longest one right now is 50. Average is about 25-30

            var criminal = _soapClient.CoAChambersApplicationsListAsync("Criminal").Result;
            Assert.NotNull(criminal);

            var civil = _soapClient.CoAChambersApplicationsListAsync("Civil").Result;
            Assert.NotNull(civil);
        }

        [Fact]
        public void CoAChambersQueueHearingAsync()
        {
            // "Fail - Booking could not be completed. Please contact scheduling or select a different date/time."
            // "Success - Hearing Booked"

            var booking = new CoAChambersBookingHearingInfo
            {
                caseID = 37351,
                email = "somebody@email.com",
                hearingDate = DateTime.Parse("2023-12-18T00:00:00.0000000"),
                hearingLength = "One Hour",
                phone = "604-555-1212",
                HearingTypeListID = "116|114",
                requestedBy = "John Smith",
                MainCase = true,
                RelatedCases = ""
            };

            var result = _soapClient.CoAChambersQueueHearingAsync(booking).Result;

            Assert.NotEmpty(result.bookingResult);
        }

        [Fact]
        public void CoAAvailableDatesChambersAsync()
        {
            var result = _soapClient.CoAAvailableDatesChambersAsync().Result;
            Assert.True(result.AvailableDates.Any());
        }

        [Fact]
        public void GetAvailableBookingTypesAsync()
        {
            var result = _soapClient.GetAvailableBookingTypesAsync().Result;
            Assert.True(result.Length > 0);
        }

        [Fact]
        public void BookTrialHearingAsync()
        {
            BookTrialHearingInfo bookingInfo =
                new()
                {
                    CEIS_Physical_File_ID = 3879m,
                    RequestedBy = "John Smith 604-555-1212 somebody@email.com",
                    BookingLocationID = 1,
                    CourtClass = "E",
                    HearingDate = new DateTime(2025, 6, 22),
                    FormulaType = "Regular", // Regular or Fair-Use
                    HearingLength = 5,
                    HearingType = 9001, // Unmet demand is 20538
                    LocationID = 41
                };
            var result = _soapClient.BookTrialHearingAsync(bookingInfo).Result;
            Assert.StartsWith("success", result.bookingResult, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void AvailableTrialBookingFormulasByLocationAsync()
        {
            var result = _soapClient.AvailableTrialBookingFormulasByLocationAsync("", "").Result;

            Assert.True(result.Length > 0);
        }

        [Fact]
        public void AvailableTrialDatesByLocationAsync_Regular()
        {
            AvailableTrialDatesRequestInfo regular =
                new()
                {
                    BookingLocationID = 41,
                    Courtclass = "E",
                    StartDate = DateTime.Parse("2024/08/01"), // these dates come from formulas by location
                    EndDate = DateTime.Parse("2026/01/31"),
                    HearingLength = 5,
                    FormulaType = "Regular",
                    LocationID = 1
                };
            var result = _soapClient.AvailableTrialDatesByLocationAsync(regular).Result;
            Assert.True(result.AvailableTrialDates.AvailablesDatesInfo.Length > 0);
        }

        [Fact]
        public void AvailableTrialDatesByLocationAsync_FairUse()
        {
            AvailableTrialDatesRequestInfo fairUse =
                new()
                {
                    BookingLocationID = 41,
                    Courtclass = "E",
                    StartDate = DateTime.Parse("2025/08/01"), // 18 months in the future
                    EndDate = DateTime.Parse("2025/08/31"), // these dates come from formulas by location
                    HearingLength = 5,
                    FormulaType = "Fair-Use",
                    LocationID = 1
                };
            var result = _soapClient.AvailableTrialDatesByLocationAsync(fairUse).Result;
            Assert.True(result.AvailableTrialDates.AvailablesDatesInfo.Length > 0);
        }
    }
}
