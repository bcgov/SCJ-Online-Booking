using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using DotEnv.Core;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;
using Xunit;
using Xunit.Abstractions;

namespace SCJ.Booking.UnitTest
{
    public class RemoteApiTests
    {
        public RemoteApiTests(ITestOutputHelper output)
        {
            _output = output;
            new EnvLoader().Load();
            var reader = new EnvReader();
            bool useFakeApi = (reader["USE_FAKE_API"] ?? string.Empty).ToLower() == "true";

            if (useFakeApi)
            {
                _soapClient = new FakeOnlineBookingClient();
            }
            else
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();
                _soapClient = OnlineBookingClientFactory.GetClient(configuration);
            }
        }

        private readonly IOnlineBooking _soapClient;
        private readonly ITestOutputHelper _output;

        [Fact]
        public async Task AvailableDatesByLocation()
        {
            const int vancouver = 1;
            const int victoria = 2;
            const int trialManagementConference = 9090;

            AvailableDatesByLocation vancouverResults =
                await _soapClient.scConfAvailableDatesByLocationAsync(
                    vancouver,
                    trialManagementConference
                );

            Assert.NotNull(vancouverResults);

            AvailableDatesByLocation victoriaResults =
                await _soapClient.scConfAvailableDatesByLocationAsync(
                    victoria,
                    trialManagementConference
                );

            Assert.NotNull(victoriaResults);
        }

        [Fact]
        public async Task BookingHearingFail()
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

            BookingHearingResult result = await _soapClient.scConfBookHearingAsync(booking);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task BookingHearingSuccess()
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

            BookingHearingResult result = await _soapClient.scConfBookHearingAsync(booking);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CaseNumberInvalid()
        {
            CourtFile[] searchResults = await _soapClient.scCaseNumberValidAsync("VAM14761");

            // If no case is found then 0 is returned
            Assert.True(searchResults.Length == 0);
        }

        [Fact]
        public async Task CaseNumberValid()
        {
            CourtFile[] searchResults = await _soapClient.scCaseNumberValidAsync("KEM111");

            Assert.True(searchResults.Length > 0);
        }

        [Fact]
        public async Task GetLocations()
        {
            Location[] locations = await _soapClient.scGetLocationsAsync();

            Assert.NotEmpty(locations);
        }

        [Fact]
        public async Task CoACaseNumberValidAsync_Civil()
        {
            COACaseList result = await _soapClient.coaCaseNumberValidAsync("CA39029");
            Assert.NotNull(result);
            Assert.True(result.CaseList[0].CaseId == 37351);
            Assert.True(result.CaseType == "Civil");
        }

        [Fact]
        public async Task CoACaseNumberValidAsync_Criminal()
        {
            COACaseList result = await _soapClient.coaCaseNumberValidAsync("CA42024");
            Assert.NotNull(result);
            Assert.True(result.CaseList[0].CaseId == 40368);
            Assert.True(result.CaseType == "Criminal");
        }

        [Fact]
        public async Task CoACaseNumberValidAsync_Invalid()
        {
            COACaseList result = await _soapClient.coaCaseNumberValidAsync("12345");
            Assert.NotNull(result);
            Assert.True(result.CaseList.Length == 1);
            Assert.True(result.CaseType == "Not Found");
        }

        [Fact]
        public async Task COAAvailableDatesAsync()
        {
            CoAAvailableDates result = await _soapClient.coaAvailableAppealDatesAsync();
            Assert.NotNull(result);
            Assert.True(result.AvailableDates.Any());
        }

        [Fact]
        public async Task CoAQueueHearingAsync()
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

            await _soapClient.coaQueueAppealHearingAsync(booking);
        }

        [Fact]
        public async Task CoAChambersApplicationsListAsync()
        {
            // types: Civil, Criminal
            // 255 is the maximum character limit for the definition
            // 255 is the limit for names too, but the longest one right now is 50. Average is about 25-30

            var criminal = await _soapClient.coaCHApplicationListAsync("Criminal");
            Assert.NotNull(criminal);

            var civil = await _soapClient.coaCHApplicationListAsync("Civil");
            Assert.NotNull(civil);
        }

        [Fact]
        public async Task CoAChambersQueueHearingAsync()
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

            var result = await _soapClient.coaQueueCHHearingAsync(booking);

            Assert.NotEmpty(result.bookingResult);
        }

        [Fact]
        public async Task CoAAvailableDatesChambersAsync()
        {
            var result = await _soapClient.coaAvailableCHDatesAsync();
            Assert.True(result.AvailableDates.Any());
        }

        [Fact]
        public async Task GetAvailableBookingTypesAsync()
        {
            var result = await _soapClient.scGetAvailableBookingTypesAsync();
            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task BookTrialHearingAsync()
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
            var result = await _soapClient.scTrialBookHearingAsync(bookingInfo);
            Assert.StartsWith("success", result.bookingResult, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task AvailableTrialBookingFormulasByLocationAsync()
        {
            var result = await _soapClient.scAvailableFormulasByHearingTypeAndLocationAsync(
                "",
                "",
                ""
            );

            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task AvailableTrialDatesByLocationAsync_Regular()
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
                    LocationID = 1,
                    HearingTypeId = 9001
                };
            var result = await _soapClient.scAvailableDatesByHearingTypeAndLocationAsync(regular);
            Assert.True(result.AvailableTrialDates.AvailablesDatesInfo.Length > 0);
        }

        [Fact]
        public async Task AvailableTrialDatesByLocationAsync_FairUse()
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
                    LocationID = 1,
                    HearingTypeId = 9001
                };
            var result = await _soapClient.scAvailableDatesByHearingTypeAndLocationAsync(fairUse);
            Assert.True(result.AvailableTrialDates.AvailablesDatesInfo.Length > 0);
        }

        [Fact]
        public async Task GetLongChambersSubTypes()
        {
            var result = await _soapClient.scCHHearingSubTypeAsync();
            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task BookLongChambersHearingAsync()
        {
            BookingSCCHHearingInfo hearingInfo =
                new()
                {
                    HearingDate = new DateTime(2025, 6, 22),
                    HearingLength = 30,
                    HearingTypeId = 9012,
                    LocationID = 1,
                    RequestedBy = "John Smith",
                    BookingLocationID = 41,
                    CEIS_Physical_File_ID = 3879m,
                    CourtClass = "E",
                    FormulaType = "Regular"
                };
            var result = await _soapClient.scCHBookHearingAsync(hearingInfo);
            Assert.StartsWith("success", result.bookingResult, StringComparison.OrdinalIgnoreCase);
        }

        // This is mainly used for finding test data, but it also functions as a test.
        // Run this test from the Test Explorer in VS Code to see output in the "Test Results" window.
        [Fact]
        public async Task FindChambersAvailableDates()
        {
            var hearingTypeId = Data.Constants.ScHearingType.LONG_CHAMBERS;

            var formulas = await _soapClient.scAvailableFormulasByHearingTypeAndLocationAsync(
                "",
                "",
                hearingTypeId.ToString()
            );

            foreach (var formula in formulas)
            {
                // Determine a relevant court class to match the BookHearingCode of the formula.
                // "E" is family. "M" is motor vehicle, but works for testing with "All" or "All Other"
                // BookHearingCode values.
                var courtClass = formula.BookingHearingCode == "E" ? "E" : "M";

                AvailableTrialDatesRequestInfo requestInfo =
                    new()
                    {
                        BookingLocationID = formula.LocationID,
                        Courtclass = courtClass,
                        StartDate = formula.StartDate,
                        EndDate = formula.EndDate,
                        HearingLength = 1, // 1 day
                        FormulaType = formula.FormulaType,
                        LocationID = 1, // Vancouver
                        HearingTypeId = hearingTypeId, // long chambers
                    };
                var datesResult = await _soapClient.scAvailableDatesByHearingTypeAndLocationAsync(
                    requestInfo
                );

                _output.WriteLine(
                    $"Location: {formula.BookingLocationID} ({formula.HearingTypeId}) - {formula.FormulaType} - {formula.StartDate:d} to {formula.EndDate:d}"
                );
                _output.WriteLine(
                    $"{formula.FairUseBookingPeriodStartDate} - {formula.FairUseBookingPeriodEndDate}"
                );
                _output.WriteLine($"{formula.BookingHearingCode}");
                _output.WriteLine(
                    $"Available Dates: {datesResult?.AvailableTrialDates?.AvailablesDatesInfo?.Length}"
                );
                _output.WriteLine("");
            }
        }
    }
}
