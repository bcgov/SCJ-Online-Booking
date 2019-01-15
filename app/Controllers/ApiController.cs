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
        public async Task<List<DayViewModel>> AvailableDatesByLocation(int locationId,
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
            var result = new List<DayViewModel>();

            DayViewModel day = null;
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

                    day = new DayViewModel
                    {
                        Date = date,
                        Times = new List<TimeSlotViewModel>()
                    };
                }

                day.Times.Add(new TimeSlotViewModel
                {
                    ContainerId = d.ContainerID,
                    Start = d.Date_Time,
                    End = d.Date_Time.AddMinutes(soapResult.BookingDetails.detailBookingLength)
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
