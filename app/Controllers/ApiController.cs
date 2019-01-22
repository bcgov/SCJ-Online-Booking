using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.ViewModels;
using SCJ.Booking.RemoteAPIs;
using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.MVC.Controllers
{
    /// <summary>
    ///     REST API's for the Superior Courts Booking app
    /// </summary>
    [ApiController]
    public class ApiController : Controller
    {
        //API Client
        private readonly IOnlineBooking _client = OnlineBookingClientFactory.GetClient();

        /// <summary>
        ///     API for getting list of available dates.  Used by the vue.js date slider control
        /// </summary>
        [Route("/booking/api/available-dates-by-location/{locationId}/{hearingType}")]
        public async Task<List<AvailableDayViewModel>> AvailableDatesByLocation(int locationId,
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
            var result = new List<AvailableDayViewModel>();

            AvailableDayViewModel day = null;
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
                    day = new AvailableDayViewModel
                    {
                        Date = date,
                        Weekday = date.DayOfWeek.ToString(),
                        FormattedDate = date.ToString("MMMM dd, yyyy"),
                        Times = new List<AvailableTimeViewModel>()
                    };
                }

                // add the timeslot to the day grouping
                day.Times.Add(new AvailableTimeViewModel
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
