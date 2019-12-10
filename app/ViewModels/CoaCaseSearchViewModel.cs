using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.OnlineBooking;

namespace SCJ.Booking.MVC.ViewModels
{
    public class CoaCaseSearchViewModel
    {
        public CoaCaseSearchViewModel()
        {
            //Default values
            CaseNumber = string.Empty;
        }

        [Required(ErrorMessage = "Please provide a Court File Number.")]
        public string CaseNumber { get; set; }
        public int CaseId { get; set; }
        public string CaseType { get; set; }
        public bool? IsValidCaseNumber { get; set; }
        public bool? CertificateOfReadiness { get; set; }
        public bool? DateIsAgreed { get; set; }
        public bool? LowerCourtOrder { get; set; }
        public bool? IsFullDay { get; set; }
        public SelectList HearingTypes { get; set; }
        public int? HearingTypeId { get; set; }
        public string HearingTypeName { get; set; }
        public Dictionary<DateTime, List<DateTime>> Results { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string SubmitButton { get; set; }
        public bool TimeSlotExpired { get; set; }
    }
}
