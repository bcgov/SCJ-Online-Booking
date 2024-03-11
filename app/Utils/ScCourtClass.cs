using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Graph;

namespace SCJ.Booking.MVC.Utils
{
    public class ScCourtClass
    {
        public static OrderedDictionary CourtClasses { get; } =
            new()
            {
                { "B", "Bankruptcy" },
                { "E", "Family Law Proceedings" },
                { "H", "Foreclosure" },
                { "L", "Enforcement/Legislated Statute" },
                { "M", "Motor Vehicle Accidents" },
                { "N", "Adoption" },
                { "P", "Probate" },
                { "S", "Supreme Civil (General)" },
                { "V", "Caveat" },
                { "D", "Divorce" },
                { "C", "Small Claims" },
            };

        public static string GetCourtClass(string value)
        {
            if (CourtClasses.Contains(value.ToUpper()))
            {
                return CourtClasses[value.ToUpper()]?.ToString();
            }

            return $"Unknown Court Class for {value}?";
        }
    }
}
