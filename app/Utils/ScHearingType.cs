using System.Collections.Generic;

namespace SCJ.Booking.MVC.Utils
{
    public static class ScHearingType
    {
        public const int TMC = 9090;
        public const int TCH = 9103;

        public static readonly Dictionary<int, string> HearingTypeNameMap = new Dictionary<int, string>
        {
            {TMC, "Trial Management Conference (TMC)" },
            {TCH, "CV-Telephone Conference Hearing (CV-TCH)" }
        };
    }
}
