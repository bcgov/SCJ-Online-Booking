using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Constants;
using SCJ.Booking.Data.Models;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.Utils;

namespace SCJ.Booking.MVC.Services
{
    public class DataWriterService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ScCacheService _cacheService;

        //Constructor
        public DataWriterService(ApplicationDbContext dbContext, ScCacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
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

        public async Task SaveLotteryEntry(
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
                throw new InvalidOperationException("LotteryStartDate cannot be null");
            }

            if (
                !bookingInfo.BookingLength.HasValue
                || bookingInfo.BookingLength.Value < 1
                || bookingInfo.BookingLength.Value > 40
            )
            {
                throw new InvalidOperationException(
                    "HearingLength must be greater than zero and less than 41"
                );
            }

            DbSet<ScLotteryBookingRequest> trialBookingRequests =
                _dbContext.Set<ScLotteryBookingRequest>();

            var oidcUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var courtFile = bookingInfo.SelectedCourtFile;

            var fairUseContactDate = formula.FairUseContactDate.Value.Date;
            var bookingPeriodEndDate = formula.FairUseBookingPeriodEndDate.Value;

            var lotteryStartDate = bookingPeriodEndDate.Date.AddDays(1);

            var bookingRequest = new ScLotteryBookingRequest
            {
                User = oidcUser,
                BookHearingCode = formula.BookingHearingCode,
                HearingTypeId = bookingInfo.HearingTypeId,
                BookingLocationId = formula.BookingLocationID,
                CeisPhysicalFileId = courtFile.physicalFileId.GetValueOrDefault(0),
                CourtClassCode = courtFile.courtClassCode,
                CaseRegistryId = bookingInfo.CaseRegistryId,
                CaseRegistryCode = bookingInfo.LocationPrefix,
                CreationTimestamp = DateTime.Now,
                Email = userInfo.Email,
                Phone = userInfo.Phone,
                RequestedByName = userInfo.ContactName,
                LocationName = bookingInfo.BookingLocationName,
                FairUseSort = courtFile.fairUseSort,
                CaseNumber = bookingInfo.CaseNumber,
                StyleOfCause = courtFile.styleOfCause ?? string.Empty,
                LocationId = bookingInfo.AlternateLocationRegistryId,
                FairUseBookingPeriodStartDate = formula.FairUseBookingPeriodStartDate.Value,
                FairUseBookingPeriodEndDate = formula.FairUseBookingPeriodEndDate.Value,
                HearingLength = bookingInfo.BookingLength.Value,
                LongChambersHearingSubTypeId = bookingInfo.ChambersHearingSubTypeId,
                LongChambersHearingSubTypeName =
                    bookingInfo.HearingTypeId == ScHearingType.LONG_CHAMBERS
                        ? bookingInfo.ChambersHearingSubTypeName
                        : "",
                LotteryStartDate = lotteryStartDate,
                LotteryEntryId = bookingInfo.LotteryEntryId
            };

            int selectionRank = 1;
            foreach (var date in bookingInfo.SelectedFairUseDates)
            {
                bookingRequest.DateSelections.Add(
                    new ScLotteryDateSelection { Rank = selectionRank, StartDate = date }
                );
                selectionRank++;
            }

            //save to DB
            await trialBookingRequests.AddAsync(bookingRequest);

            await _dbContext.SaveChangesAsync();
        }
    }
}
