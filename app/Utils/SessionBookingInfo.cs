namespace SCJ.Booking.MVC.Utils
{
    public class SessionBookingInfo
    {
        public int CaseId { get; set; }
        public string CaseNumber { get; set; }
        public string FullCaseNumber { get; set; }
        public int LocationId { get; set; }
        public string RegistryName { get; set; }
        public int HearingLengthMinutes { get; set; }
        public int ContainerId { get; set; }
        public int HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public string TimeSlotFriendlyName { get; set; }
        public string SelectedCaseDate { get; set; }
    }
}
