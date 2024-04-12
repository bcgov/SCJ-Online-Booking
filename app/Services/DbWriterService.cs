using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;
using SCJ.Booking.MVC.Utils;

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
            string bookingLocationName,
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
                    BookingLocationName = bookingLocationName
                }
            );

            //save to DB
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveFairUseRequest(
            long userId,
            ScSessionBookingInfo bookingInfo,
            SessionUserInfo userInfo
        )
        {
            var formula = bookingInfo.FairUseFormula;

            if (!formula.FairUseBookingPeriodStartDate.HasValue)
            {
                throw new InvalidOperationException("FairUseBookingPeriodStartDate cannot be null");
            }

            if (!formula.FairUseBookingPeriodEndDate.HasValue)
            {
                throw new InvalidOperationException("FairUseBookingPeriodEndDate cannot be null");
            }

            if (!formula.FairUseContactDate.HasValue)
            {
                throw new InvalidOperationException("FairUseContactDate cannot be null");
            }

            if (
                !bookingInfo.EstimatedTrialLength.HasValue
                || bookingInfo.EstimatedTrialLength.Value < 1
                || bookingInfo.EstimatedTrialLength.Value > 99
            )
            {
                throw new InvalidOperationException(
                    "HearingLength must be greater than zero and less than 100"
                );
            }

            DbSet<ScTrialBookingRequest> trialBookingRequests =
                _dbContext.Set<ScTrialBookingRequest>();

            var oidcUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var bookingRequest = new ScTrialBookingRequest
            {
                User = oidcUser,
                BookHearingCode = formula.BookingHearingCode,
                BookingLocationId = formula.BookingLocationID,
                CeisPhysicalFileId = bookingInfo.PhysicalFileId,
                CourtClassCode = bookingInfo.SelectedCourtClass,
                CourtClassName = bookingInfo.SelectedCourtClassName,
                CreationTimestamp = DateTime.Now,
                Email = userInfo.Email,
                Phone = userInfo.Phone,
                RequestedByName = userInfo.ContactName,
                UnmetDemandMonths = bookingInfo.UnmetDemandMonths,
                TrialLocationName = bookingInfo.BookingLocationName,
                FullCaseNumber = bookingInfo.FullCaseNumber,
                StyleOfCause = bookingInfo.SelectedCourtFile.styleOfCause,
                TrialPeriodStartDate = formula.StartDate,
                TrialPeriodEndDate = formula.EndDate,
                LocationId = bookingInfo.TrialLocationRegistryId,
                FairUseBookingPeriodStartDate = formula.FairUseBookingPeriodStartDate.Value,
                FairUseBookingPeriodEndDate = formula.FairUseBookingPeriodEndDate.Value,
                HearingLength = bookingInfo.EstimatedTrialLength.Value,
                FairUseContactDate = formula.FairUseContactDate.Value,
            };

            int selectionRank = 1;
            foreach (var date in bookingInfo.SelectedFairUseTrialDates)
            {
                bookingRequest.TrialDateSelections.Add(
                    new ScTrialDateSelection { Rank = selectionRank, TrialStartDate = date }
                );
                selectionRank++;
            }

            //save to DB
            await trialBookingRequests.AddAsync(bookingRequest);

            await _dbContext.SaveChangesAsync();
        }
    }
}
