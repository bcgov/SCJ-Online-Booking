using System.Collections.Generic;

namespace SCJ.Booking.MVC.Utils
{
    public static class ScHearingType
    {
        public const int TMC = 9090;
        public const int TCH = 9103;
        public const int AWS = 9104;
        public const int PTC = 9543;
        public const int JMC = 9095;

        public static readonly Dictionary<int, string> HearingTypeNameMap =
            new Dictionary<int, string>
            {
                {TMC, "Trial Management Conference (TMC)"},
                {TCH, "CV-Telephone Conference Hearing (CV-TCH)"},
                {AWS, "CV-Application Written Submissions (CV-AWS)"},
                {PTC, "CV-Pre-Trial Conference (CV-PTC)"},
                {JMC, "Judicial Management Conference (JMC)"}
            };
    }
}
