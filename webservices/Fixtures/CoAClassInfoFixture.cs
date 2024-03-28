using SCJ.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs.Fixtures
{
    public static class CoAClassInfoFixture
    {
        internal static COACaseList CivilCase =
            new()
            {
                CaseType = "Civil",
                CaseList = new[]
                {
                    new CoAClassInfo
                    {
                        CaseId = 37351,
                        Case_Num = "CA39029",
                        Main = true
                    }
                }
            };

        internal static COACaseList CivilCaseWithChildCA39000 =
            new()
            {
                CaseType = "Civil",
                CaseList = new[]
                {
                    new CoAClassInfo
                    {
                        CaseId = 37350,
                        Case_Num = "CA39000",
                        Main = true
                    },
                    new CoAClassInfo
                    {
                        CaseId = 37349,
                        Case_Num = "CA39001",
                        Main = false
                    },
                    new CoAClassInfo
                    {
                        CaseId = 37348,
                        Case_Num = "CA39002",
                        Main = false
                    }
                }
            };

        internal static COACaseList CivilCaseWithParentCA39001 =
            new()
            {
                CaseType = "Civil",
                CaseList = new[]
                {
                    new CoAClassInfo
                    {
                        CaseId = 37350,
                        Case_Num = "CA39000",
                        Main = true
                    },
                    new CoAClassInfo
                    {
                        CaseId = 37349,
                        Case_Num = "CA39001",
                        Main = false
                    },
                    new CoAClassInfo
                    {
                        CaseId = 37348,
                        Case_Num = "CA39002",
                        Main = false
                    }
                }
            };

        internal static COACaseList CivilCaseWithParentCA39002 =
            new()
            {
                CaseType = "Civil",
                CaseList = new[]
                {
                    new CoAClassInfo
                    {
                        CaseId = 37350,
                        Case_Num = "CA39000",
                        Main = true
                    },
                    new CoAClassInfo
                    {
                        CaseId = 37349,
                        Case_Num = "CA39001",
                        Main = false
                    },
                    new CoAClassInfo
                    {
                        CaseId = 37348,
                        Case_Num = "CA39002",
                        Main = false
                    }
                }
            };

        internal static COACaseList CriminalCase =
            new()
            {
                CaseType = "Criminal",
                CaseList = new[]
                {
                    new CoAClassInfo
                    {
                        CaseId = 40368,
                        Case_Num = "CA42024",
                        Main = true
                    }
                }
            };

        internal static COACaseList NotFound =
            new()
            {
                CaseType = "Not Found",
                CaseList = new[]
                {
                    new CoAClassInfo
                    {
                        CaseId = 0,
                        Case_Num = "",
                        Main = false
                    }
                }
            };
    }
}
