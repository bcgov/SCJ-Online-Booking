using System;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;
using Xunit;

namespace SCJ.Booking.UnitTest
{
    public class RemoteApiTests
    {
        public RemoteApiTests()
        {
            _soapClient = new FakeOnlineBookingClient();
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
                caseID = 234076,
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
                caseID = 234076,
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
            // VAM is not a valid location
            int result = _soapClient.caseNumberValidAsync("VAM14761").Result;

            // If no case is found then 0 is returned
            Assert.True(result == 0);
        }

        [Fact]
        public void CaseNumberValid()
        {
            int result = _soapClient.caseNumberValidAsync("VAM147619").Result;

            Assert.True(result == 234076);
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
            Assert.True(result.AvailableDates.Length ==138);
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
                email = "mike@olund.ca",
                hearingDate = DateTime.Parse("2019/11/08 00:00:00.0000000"),
                hearingLength = "Full",
                phone = "778-865-7042",
                hearingTypeId = 24,
                requestedBy = "Mike Olund"
            };

            BookingHearingResult result = _soapClient.CoAQueueHearingAsync(booking).Result;
        }
    }
}
