using Microsoft.AspNetCore.Html;
using SCJ.Booking.MVC.Data;
using System;
using System.Linq;

namespace SCJ.Booking.MVC.Services
{
    public class HearingsLeft
    {
        private readonly ApplicationDbContext _dbContext;
        private const int _maxHearingsPerDay = 3;

        public HearingsLeft(ApplicationDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public HtmlString GetHearingsLeft()
        {
            return new HtmlString(GetUserHearingsTotalLeft().ToString());
        }

        private int GetUserHearingsTotalLeft()
        {
            //TODO: Read custom header for currently logged-in user ID
            //string uGuid = Request.Headers["HTTP_SMGOV_USERGUID"];

            //get user GUID

            string uGuid = "b17a483a00124bd18a5544c8c20bf8e8";

            //today's date
            var todaysDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            //get all entries for logged-in user
            //booked on today
            var hearingsBookedForToday = _dbContext.BookingHistory.Where(b => b.SmGovUserGuid == uGuid &&  b.Timestamp.Day == todaysDate.Day && b.Timestamp.Month == todaysDate.Month && b.Timestamp.Year == b.Timestamp.Year).ToList();

            if (hearingsBookedForToday != null && hearingsBookedForToday.Count() > 0)
                //return number of hearings booked for today minus the max bookings allowed
                return (_maxHearingsPerDay - hearingsBookedForToday.Count());
            else
                //User have not made any bookings for the day
                return _maxHearingsPerDay;
        }
    }
}
