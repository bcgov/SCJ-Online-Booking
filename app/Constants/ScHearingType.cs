using System.Collections.Generic;

namespace SCJ.Booking.MVC.Constants
{
    public static class ScHearingType
    {
        public const int AWS = 9104;
        public const int JMC = 9095;
        public const int PTC = 9543;
        public const int TCH = 9103;

        public const int TMC = 9090;
        public const int CPC = 9089;
        public const int JCC = 9005;
        public const int TRIAL = 9001;

        public const int UNKNOWN = 0;

        public static string GetHearingType(int code)
        {
            return code switch
            {
                CPC => nameof(CPC),
                JCC => nameof(JCC),
                TMC => nameof(TMC),
                JMC => nameof(JMC),
                _ => nameof(UNKNOWN)
            };
        }

        // map string ID to integer ID
        public static readonly Dictionary<string, int> HearingTypeIdMap =
            new()
            {
                { "AWS", AWS },
                { "JMC", JMC },
                { "PTC", PTC },
                { "TCH", TCH },
                { "Trials", TRIAL },
                { "TMC", TMC },
                { "CPC", CPC },
                { "JCC", JCC },
                { "UNKNOWN", UNKNOWN },
            };

        // map integer ID to description text
        public static readonly Dictionary<int, string> HearingTypeNameMap =
            new()
            {
                { AWS, "CV-Application Written Submissions (CV-AWS)" },
                { JMC, "Judicial Management Conference (JMC)" },
                { PTC, "CV-Pre-Trial Conference (CV-PTC)" },
                { TCH, "CV-Telephone Conference Hearing (CV-TCH)" },
                { TRIAL, "Trial" },
                { TMC, "Trial Management Conference (TMC)" },
                { CPC, "Case Planning Conference (CPC)" },
                { JCC, "Judicial Case Conference (JCC)" },
                { UNKNOWN, "Unknown Conference" },
            };
    }
}
