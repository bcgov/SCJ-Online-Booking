using System.Collections.Specialized;

namespace SCJ.Booking.Data.Constants
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
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (CourtClasses.Contains(value.ToUpper()))
            {
                return CourtClasses[value.ToUpper()]?.ToString()
                    ?? $"Unknown Court Class for {value}?";
            }

            return $"Unknown Court Class for {value}?";
        }
    }
}
