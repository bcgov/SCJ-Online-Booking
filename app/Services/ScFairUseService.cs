using SCJ.Booking.MVC.Extensions;
using SCJ.Booking.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCJ.Booking.MVC.Services
{
    public class ScFairUseService
    {
        public static IList<T> PerformFairUseAlgorithm<T>(IList<T> list)
        {
            list.Shuffle();
            return list;
        }
    }
}
