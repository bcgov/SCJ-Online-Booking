using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SCJ.Booking.MVC.ViewModels;
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
        //API Client
        private readonly IOnlineBooking _client;

        public ApiController(IConfiguration configuration)
        {
            _client = OnlineBookingClientFactory.GetClient(configuration);
        }

        /// <summary>
        ///     API for getting list of Supreme Court available dates.
        ///     Used by the vue.js date slider control
        /// </summary>
        [Route("/booking/api/sc-available-dates-by-location/{locationId}/{hearingType}")]
        public async Task<List<ScAvailableDayViewModel>> AvailableScDatesByLocation(int locationId,
            int hearingType)
        {
            // call the remote API
            AvailableDatesByLocation soapResult = await _client
                .AvailableDatesByLocationAsync(locationId, hearingType);

            // sort the available times chronologically
            IOrderedEnumerable<ContainerInfo> dates = soapResult
                .AvailableDates
                .OrderBy(d => d.Date_Time);

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
                        FormattedDate = date.ToString("MMMM dd, yyyy"),
                        Times = new List<ScAvailableTimeViewModel>()
                    };
                }

                // add the timeslot to the day grouping
                day.Times.Add(new ScAvailableTimeViewModel
                {
                    ContainerId = item.ContainerID,
                    StartDateTime = item.Date_Time,
                    Start = item.Date_Time.ToString("hh:mmtt").ToLower(),
                    End = item.Date_Time.AddMinutes(soapResult.BookingDetails.detailBookingLength)
                        .ToString("hh:mmtt").ToLower()
                });

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
    }
}
