using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;

namespace SCJ.Booking.MVC.Services
{
    public class DbWriterService
    {
        private readonly ApplicationDbContext _dbContext;

        //Constructor
        public DbWriterService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveBookingHistory(
            long userId,
            string courtLevel,
            int? scHearingType = null,
            string scFormulaType = null,
            string coaCaseType = null,
            string coaConferenceType = null
        )
        {
            DbSet<BookingHistory> bookingHistory = _dbContext.Set<BookingHistory>();

            var oidcUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            await bookingHistory.AddAsync(
                new BookingHistory
                {
                    CourtLevel = courtLevel,
                    User = oidcUser,
                    Timestamp = DateTime.Now,
                    ScHearingType = scHearingType,
                    ScFormulaType = scFormulaType,
                    CoaCaseType = coaCaseType,
                    CoaConferenceType = coaConferenceType,
                }
            );

            //save to DB
            await _dbContext.SaveChangesAsync();
        }
    }
}
