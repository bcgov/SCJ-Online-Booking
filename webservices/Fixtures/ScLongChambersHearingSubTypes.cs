using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public static class ScLongChambersHearingSubTypes
    {
        internal static SCCHHearingSubTypeDetails[] All =
        {
            new() { ChambersDescription = "Appeal from Provincial Court", HearingSubTypeId = 9010 },
            new() { ChambersDescription = "Chambers (default or other)", HearingSubTypeId = 9012 },
            new() { ChambersDescription = "Chambers Summary Trial", HearingSubTypeId = 9013 },
            new()
            {
                ChambersDescription = "Chambers Appeal from Associate Judge or Registrar",
                HearingSubTypeId = 9014
            },
            new() { ChambersDescription = "Chambers Judicial Review", HearingSubTypeId = 9020 },
            new() { ChambersDescription = "Chambers Petition", HearingSubTypeId = 9022 }
        };
    }
}
