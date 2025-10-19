using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SCJ.Booking.MVC.ViewModels.SC;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.Services.SC
{
    public interface IScLotteryEnabledBookingService
    {
        bool IsLocalDevEnvironment { get; }

        Task<ScLotteryEnabledAvailableSlotsViewModel> LoadAvailableDatesFormAsync();

        Task<ScLotteryEnabledAvailableSlotsViewModel> LoadAvailableDatesFormulaInfoAsync(
            ScLotteryEnabledAvailableSlotsViewModel model,
            FormulaLocation fairUseFormula
        );

        void SaveAvailableDatesFormAsync(ScLotteryEnabledAvailableSlotsViewModel model);

        Task<Tuple<List<DateTime>, FormulaLocation>> GetAvailableBookingDatesAsync(
            string formulaType,
            FormulaLocation formula
        );

        string GenerateLotteryEntryId();

        Task<bool> CheckIfTrialAlreadyRequestedAsync();

        Task<bool> CheckIfLongChambersAlreadyRequestedAsync();

        Task<bool> CheckIfBookingAlreadyRequestedAsync(int hearingTypeId);

        Task<ScCaseConfirmViewModel> CreateBookingAsync(
            ScCaseConfirmViewModel model,
            ClaimsPrincipal user
        );
    }
}
