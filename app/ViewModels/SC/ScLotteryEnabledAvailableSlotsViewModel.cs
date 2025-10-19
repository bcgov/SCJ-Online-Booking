using System;
using System.Collections.Generic;
using SCJ.Booking.MVC.Utils;

namespace SCJ.Booking.MVC.ViewModels.SC
{
    public class ScLotteryEnabledAvailableSlotsViewModel
    {
        public int HearingTypeId { get; set; } = -1;
        public string FormulaType { get; set; } = string.Empty;

        public List<DateTime> AvailableRegularDates { get; set; } = new();
        public List<DateTime> AvailableFairUseDates { get; set; } = new();
        public DateTime? SelectedRegularDate { get; set; }
        public List<DateTime> SelectedFairUseDates { get; set; }
        public DateTime? FairUseStartDate { get; set; }
        public DateTime? FairUseEndDate { get; set; }
        public DateTime? FairUseResultDate { get; set; }
        public DateTime? FairUseSelectionDate { get; set; }

        // Session object
        public ScSessionBookingInfo SessionInfo { get; set; }

        // Indicates if there is an existing chambers lottery entry for the same case
        public bool HasExistingLongChambersRequest { get; set; } = false;
    }
}
