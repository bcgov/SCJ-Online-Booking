using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCJ.Booking.MVC.Utils
{
    public static class CoaHearingType
    {
        public class CoaHearingTypeInfo
        {
            // - civil hearing of appeal = 24
            // - criminal hearing of appeal = 72
            // - criminal conviction appeal = 96
            // - criminal sentence appeal = 97
            public int HearingTypeId { get; set; }
            public string Description { get; set; }
            public bool IsCriminal { get; set; }
        }

        public static List<CoaHearingTypeInfo> GetHearingTypes()
        {
            List<CoaHearingTypeInfo> result = new List<CoaHearingTypeInfo>();

            // - civil hearing of appeal = 24
            result.Add(new CoaHearingTypeInfo
            {
                HearingTypeId = 24,
                Description = "Hearing of Appeal",
                IsCriminal = false
            });

            // - criminal conviction appeal = 96
            result.Add(new CoaHearingTypeInfo
            {
                HearingTypeId = 96,
                Description = "Conviction Appeal",
                IsCriminal = true
            });

            // - criminal sentence appeal = 97
            result.Add(new CoaHearingTypeInfo
            {
                HearingTypeId = 97,
                Description = "Sentence Appeal",
                IsCriminal = true
            });

            // - criminal hearing of appeal = 72
            result.Add(new CoaHearingTypeInfo
            {
                HearingTypeId = 72,
                Description = "Other Appeal",
                IsCriminal = true
            });

            return result;
        }
    }
}
