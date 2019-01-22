using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using SCJ.Booking.MVC.Data;
using System;
using System.Linq;

namespace SCJ.Booking.MVC.Services
{
    public class HearingsLeft
    {
        private readonly ApplicationDbContext _dbContext;
        private IHttpContextAccessor _httpContextAccessor;
        private const int _maxHearingsPerDay = 3;

        public HearingsLeft(ApplicationDbContext dbContext, IHttpContextAccessor httpAccessor )
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpAccessor;
        }

        public HtmlString GetHearingsLeft()
        {
            return new HtmlString(GetUserHearingsTotalLeft().ToString());
        }

        private int GetUserHearingsTotalLeft()
        {
            //get user GUID
            var uGuid = string.Empty;

            //try and read the header
            if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("SMGOV-USERGUID"))
                uGuid = _httpContextAccessor.HttpContext.Request.Headers["SMGOV-USERGUID"].ToString();
            else return 0;

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
