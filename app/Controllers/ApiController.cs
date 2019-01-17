using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCJ.Booking.MVC.Models;
using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.MVC.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        //API Client
        private readonly FakeOnlineBookingClient _client = new FakeOnlineBookingClient();

        [Route("/booking/api/available-dates-by-location/{locationId}/{hearingType}")]
        public async Task<List<AvailableDayViewModel>> AvailableDatesByLocation(int locationId,
            int hearingType)
        {
            // call the remote API
            AvailableDatesByLocation soapResult = await _client
                .AvailableDatesByLocationAsync(locationId, hearingType);

            // get sort the dates chronologically
            IOrderedEnumerable<ContainerInfo> dates = soapResult
                .AvailableDates
                .OrderBy(d => d.Date_Time);

            // create the return object
            var result = new List<AvailableDayViewModel>();

            AvailableDayViewModel day = null;
            DateTime? lastDate = null;

            foreach (ContainerInfo d in dates)
            {
                var date = new DateTime(d.Date_Time.Year, d.Date_Time.Month, d.Date_Time.Day);

                if (date != lastDate)
                {
                    if (lastDate != null)
                    {
                        result.Add(day);
                    }

                    day = new AvailableDayViewModel
                    {
                        Date = date,
                        Weekday = date.DayOfWeek.ToString(),
                        FormattedDate = date.ToString("MMMM dd, yyyy"),
                        Times = new List<AvailableTimeViewModel>()
                    };
                }

                day.Times.Add(new AvailableTimeViewModel
                {
                    ContainerId = d.ContainerID,
                    StartDateTime = d.Date_Time,
                    Start = d.Date_Time.ToString("hh:mmtt").ToLower(),
                    End = d.Date_Time.AddMinutes(soapResult.BookingDetails.detailBookingLength).ToString("hh:mmtt").ToLower(),
                });

                lastDate = date;
            }

            if (day != null)
            {
                result.Add(day);
            }

            return result;
        }
    }
}
