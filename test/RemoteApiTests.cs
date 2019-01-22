using System;
using SCJ.Booking.RemoteAPIs;
using SCJ.SC.OnlineBooking;
using Xunit;

namespace SCJ.Booking.UnitTest
{
    public class RemoteApiTests
    {
        public RemoteApiTests()
        {
            _soapClient = OnlineBookingClientFactory.GetClient();
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
    }
}
