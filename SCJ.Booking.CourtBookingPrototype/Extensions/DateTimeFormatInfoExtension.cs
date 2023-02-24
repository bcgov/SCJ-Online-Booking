using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Extensions
{
    //used to remove the period from the end of the abbreviated month name
    public class DateTimeFormatInfoExtension
    {
        private static string[] AbbreviatedMonthNames = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "" };
        private static CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
        private static DateTimeFormatInfo _dateTimeFormatInfo { get; set; }
        public static DateTimeFormatInfo DateTimeFormatInfoEx
        {
            get
            {
                if(_dateTimeFormatInfo == null)
                {
                    _dateTimeFormatInfo = ci.DateTimeFormat;
                    _dateTimeFormatInfo.AbbreviatedMonthNames = AbbreviatedMonthNames;
                    _dateTimeFormatInfo.AbbreviatedMonthGenitiveNames = _dateTimeFormatInfo.AbbreviatedMonthNames;
                }

                return _dateTimeFormatInfo;
            }
        }
    }
}
