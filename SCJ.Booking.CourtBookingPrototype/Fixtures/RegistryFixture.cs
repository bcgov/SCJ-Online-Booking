using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCJ.Booking.CourtBookingPrototype.Fixtures
{
    public class RegistryFixture
    {
        private static Registry _vancouverRegistry;
        public static Registry VancouverRegistry
        {
            get
            {
                if (_vancouverRegistry == null)
                    _vancouverRegistry = new Registry{ Id = 1, Location = "VA" };

                return _vancouverRegistry;
            }
        } 
    }

    public class Registry
    {
        public int Id { get; set; }
        public string Location { get; set; }
    }
}
