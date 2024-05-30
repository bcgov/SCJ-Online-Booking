using System;
using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public static class ScAvailableDatesByLocationFixture
    {
        internal static readonly AvailableDatesByLocation AvailableDatesResult =
            new()
            {
                AvailableDates = new[]
                {
                    new ContainerInfo
                    {
                        ContainerID = 365524,
                        Date_Time = DateTime.Now.Date.AddDays(90).AddHours(10).AddMinutes(15)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 365650,
                        Date_Time = DateTime.Now.Date.AddDays(90).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465510,
                        Date_Time = DateTime.Now.Date.AddDays(91).AddHours(10).AddMinutes(15)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465582,
                        Date_Time = DateTime.Now.Date.AddDays(91).AddHours(11).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465590,
                        Date_Time = DateTime.Now.Date.AddDays(93).AddHours(11).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465667,
                        Date_Time = DateTime.Now.Date.AddDays(93).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465519,
                        Date_Time = DateTime.Now.Date.AddDays(96).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465624,
                        Date_Time = DateTime.Now.Date.AddDays(100).AddHours(10).AddMinutes(15)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465750,
                        Date_Time = DateTime.Now.Date.AddDays(100).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465710,
                        Date_Time = DateTime.Now.Date.AddDays(101).AddHours(10).AddMinutes(15)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465782,
                        Date_Time = DateTime.Now.Date.AddDays(101).AddHours(11).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465790,
                        Date_Time = DateTime.Now.Date.AddDays(103).AddHours(11).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465767,
                        Date_Time = DateTime.Now.Date.AddDays(103).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 465719,
                        Date_Time = DateTime.Now.Date.AddDays(106).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565510,
                        Date_Time = DateTime.Now.Date.AddDays(111).AddHours(10).AddMinutes(15)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565582,
                        Date_Time = DateTime.Now.Date.AddDays(111).AddHours(11).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565590,
                        Date_Time = DateTime.Now.Date.AddDays(113).AddHours(11).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565667,
                        Date_Time = DateTime.Now.Date.AddDays(113).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565519,
                        Date_Time = DateTime.Now.Date.AddDays(116).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565624,
                        Date_Time = DateTime.Now.Date.AddDays(120).AddHours(10).AddMinutes(15)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565750,
                        Date_Time = DateTime.Now.Date.AddDays(120).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565710,
                        Date_Time = DateTime.Now.Date.AddDays(121).AddHours(10).AddMinutes(15)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565782,
                        Date_Time = DateTime.Now.Date.AddDays(121).AddHours(11).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565790,
                        Date_Time = DateTime.Now.Date.AddDays(123).AddHours(11).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565767,
                        Date_Time = DateTime.Now.Date.AddDays(123).AddHours(14).AddMinutes(45)
                    },
                    new ContainerInfo
                    {
                        ContainerID = 565719,
                        Date_Time = DateTime.Now.Date.AddDays(126).AddHours(14).AddMinutes(45)
                    }
                },
                BookingDetails = new BookingDetail
                {
                    bookingNotes =
                        "Supreme Court Civil Rule 12-2 was amended on July 1, 2016 with new timelines for filing & serving trial briefs. Failure to comply will result in your trial & booking being removed from the court list, unless the court otherwise orders. \r\n\r\nPlease contact Scheduling at 604-660-2853 to book if you encounter issues with Online Booking.",
                    detailBookingLength = 30
                }
            };
    }
}
