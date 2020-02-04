using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public static class CoAClassInfoFixture
    {
        public static COACaseList CivilCase = new COACaseList
        {
            CaseType = "Civil",
            CaseList = new CoAClassInfo[] {

                new CoAClassInfo {
                    CaseId = 37351,
                    Case_Num = "CA39029",
                    Main = true
                }
            }
        };

        public static COACaseList CivilCaseWithChildCA39000 = new COACaseList
        {
            CaseType = "Civil",
            CaseList = new CoAClassInfo[] {

                new CoAClassInfo {
                    CaseId = 37350,
                    Case_Num = "CA39000",
                    Main = true
                },

                new CoAClassInfo {
                    CaseId = 37349,
                    Case_Num = "CA39001",
                    Main = false
                }
            }
        };

        public static COACaseList CivilCaseWithParentCA39001 = new COACaseList
        {
            CaseType = "Civil",
            CaseList = new CoAClassInfo[] {

                new CoAClassInfo {
                    CaseId = 37350,
                    Case_Num = "CA39000",
                    Main = true
                },

                new CoAClassInfo {
                    CaseId = 37349,
                    Case_Num = "CA39001",
                    Main = false
                }

            }
        };



        public static COACaseList CriminalCase = new COACaseList
        {
            CaseType = "Criminal",
            CaseList = new CoAClassInfo[] {

                new CoAClassInfo {
                    CaseId = 40368,
                    Case_Num = "CA42024",
                    Main = true
                }
            }
        };

        public static COACaseList NotFound = new COACaseList
        {
            CaseType = "Not Found",
            CaseList = new CoAClassInfo[] { }
        };
    }
}

