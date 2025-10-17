using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using SCJ.Booking.Data;
using SCJ.Booking.MVC.ViewModels.SC;
using SCJ.Booking.RemoteAPIs;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Controllers
{
    /// <summary>
    ///     REST API's for the Superior Courts Booking app
    /// </summary>
    [ApiController]
    public class ApiController : Controller
    {
        private readonly IOnlineBooking _client;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public ApiController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _client = OnlineBookingClientFactory.GetClient(configuration);
            _dbContext = dbContext;
        }

        /// <summary>
        ///     API for getting list of Supreme Court available dates.
        ///     Used by the vue.js date slider control
        /// </summary>
        [Route("/booking/api/sc-available-dates-by-location/{locationId}/{hearingType}")]
        [Authorize]
        public async Task<List<ScAvailableDayViewModel>> AvailableScDatesByLocation(
            int locationId,
            int hearingType
        )
        {
            // call the remote API
            AvailableDatesByLocation soapResult = await _client.scConfAvailableDatesByLocationAsync(
                locationId,
                hearingType
            );

            // sort the available times chronologically
            IOrderedEnumerable<ContainerInfo> dates = soapResult.AvailableDates.OrderBy(d =>
                d.Date_Time
            );

            // create the return object
            var result = new List<ScAvailableDayViewModel>();

            ScAvailableDayViewModel day = null;
            DateTime? lastDate = null;

            // loop through the available times and group them by date
            foreach (ContainerInfo item in dates)
            {
                DateTime date = item.Date_Time.Date;

                if (date != lastDate)
                {
                    // starting a new day grouping...
                    // add the previous day grouping to the result collection
                    if (lastDate != null)
                    {
                        result.Add(day);
                    }

                    // create a new day grouping
                    day = new ScAvailableDayViewModel
                    {
                        Date = date,
                        Weekday = date.DayOfWeek.ToString(),
                        FormattedDate = date.ToString("MMMM d, yyyy"),
                        Times = new List<HearingTime>()
                    };
                }

                // add the timeslot to the day grouping
                day.Times.Add(
                    new HearingTime
                    {
                        ContainerId = item.ContainerID,
                        StartDateTime = item.Date_Time,
                        Start = item.Date_Time.ToString("h:mm tt").ToLower(),
                        End = item
                            .Date_Time.AddMinutes(soapResult.BookingDetails.detailBookingLength)
                            .ToString("h:mm tt")
                            .ToLower()
                    }
                );

                lastDate = date;
            }

            // add the last day grouping to the result collection
            if (day != null)
            {
                result.Add(day);
            }

            // return the list of day groupings
            return result;
        }

        [Route("/api/lottery-results/{year}/{month}")]
        public async Task<ActionResult> LotteryResults(int year, int month)
        {
            if (!IsAllowedToExportJson(Request))
            {
                return new UnauthorizedResult();
            }

            DateTime startDate = new(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddSeconds(-1);

            var lotteries = await _dbContext
                .ScLotteries.Where(l =>
                    l.FairUseBookingPeriodEndDate >= startDate
                    && l.FairUseBookingPeriodEndDate <= endDate
                )
                .Select(l => new
                {
                    LotteryId = l.Id,
                    l.BookingLocationId,
                    l.BookHearingCode,
                    l.HearingTypeId,
                    l.FairUseBookingPeriodStartDate,
                    l.FairUseBookingPeriodEndDate,
                    l.InitiationTime,
                    l.CompletionTime,
                    TrialRequests = l
                        .TrialBookingRequests.OrderBy(x => x.ProcessingTimestamp)
                        .Select(r => new
                        {
                            r.LotteryEntryId,
                            r.FairUseSort,
                            r.LotteryPosition,
                            r.HearingLength,
                            DateSelections = r
                                .DateSelections.OrderBy(s => s.Rank)
                                .Select(s => new
                                {
                                    s.Rank,
                                    s.StartDate,
                                    s.BookingResult
                                }),
                            AllocatedSelectionRank = (int?)(
                                r.AllocatedSelectionRank > 0 ? r.AllocatedSelectionRank : null
                            ),
                            r.UnmetDemandBookingResult,
                            r.ProcessingTimestamp
                        })
                })
                .ToListAsync();

            return Json(
                lotteries,
                new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.Never }
            );
        }

        [Route("/api/lottery-requests/{year}/{month}")]
        public async Task<ActionResult> LotteryRequests(int year, int month)
        {
            if (!IsAllowedToExportJson(Request))
            {
                return new UnauthorizedResult();
            }

            DateTime startDate = new(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddSeconds(-1);

            var requests = await _dbContext
                .ScLotteryBookingRequests.Where(r =>
                    r.FairUseBookingPeriodEndDate >= startDate
                    && r.FairUseBookingPeriodEndDate <= endDate
                )
                .GroupBy(r => new
                {
                    r.BookingLocationId,
                    r.BookHearingCode,
                    r.FairUseBookingPeriodStartDate,
                    r.FairUseBookingPeriodEndDate
                })
                .Select(g => new
                {
                    g.Key.BookingLocationId,
                    g.Key.BookHearingCode,
                    g.Key.FairUseBookingPeriodStartDate,
                    g.Key.FairUseBookingPeriodEndDate,
                    TrialRequests = g.Select(r => new
                        {
                            r.CreationTimestamp,
                            r.LotteryEntryId,
                            r.FairUseSort,
                            r.HearingLength,
                            DateSelections = r
                                .DateSelections.OrderBy(s => s.Rank)
                                .Select(s => new { s.Rank, s.StartDate, }),
                        })
                        .ToList()
                })
                .ToListAsync();

            return Json(
                requests,
                new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.Never }
            );
        }

        private bool IsAllowedToExportJson(HttpRequest request)
        {
            var userToken = Request
                .Headers[HeaderNames.Authorization]
                .ToString()
                .Replace("Bearer ", "");
            var systemToken = _configuration["JSON_EXPORT_TOKEN"];

            return !string.IsNullOrEmpty(systemToken) && userToken == systemToken;
        }
    }
}
