using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCJ.Booking.MVC.Utils;

namespace SCJ.Booking.MVC.Services
{
    public class SelectListService
    {
        private readonly ScCacheService _cacheService;

        public SelectListService(ScCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        /// <summary>
        ///     Supreme court locations
        /// </summary>
        public IEnumerable<SelectListItem> SupremeLocations
        {
            get
            {
                return _cacheService
                    .GetLocationDictionaryAsync()
                    .Result.Select(x => new SelectListItem(x.Value, x.Key.ToString()));
            }
        }

        /// <summary>
        ///     Court of Appeal hearing types
        /// </summary>
        public static IEnumerable<SelectListItem> CoaHearingTypes
        {
            get
            {
                return new SelectList(
                    CoaHearingType
                        .GetHearingTypes()
                        .Where(x => x.IsCriminal)
                        .Select(x => new { Id = x.HearingTypeId, Value = x.Description }),
                    "Id",
                    "Value"
                );
            }
        }
    }
}
