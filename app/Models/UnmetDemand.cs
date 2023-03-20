namespace SCJ.Booking.MVC.Models
{
    public class UnmetDemand
    {
        public int Id { get; set; }

        //CEIS Physical File Id
        public decimal CaseId { get; set; }
        public int BookingPeriodId { get; set; }
        public int Count { get; set; }
        public decimal BookingLength { get; set; }
    }
}
