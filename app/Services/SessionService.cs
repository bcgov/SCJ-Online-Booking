using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SCJ.Booking.MVC.Utils;

namespace SCJ.Booking.MVC.Services
{
    public class SessionService
    {
        // session keys
        private const string BookingInfoKey = "BookingInfo";
        private const string UserInfoKey = "UserInfo";

        // services
        private readonly HttpContext _httpContext;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        /// <summary>
        ///     Stores information about the booking in the session
        /// </summary>
        public SessionBookingInfo BookingInfo
        {
            get => GetObject<SessionBookingInfo>(_httpContext.Session, BookingInfoKey) ??
                   new SessionBookingInfo();
            set => SetObject(_httpContext.Session, BookingInfoKey, value);
        }

        /// <summary>
        ///     Stores information about the user in the session
        /// </summary>
        public SessionUserInfo UserInfo
        {
            get => GetObject<SessionUserInfo>(_httpContext.Session, UserInfoKey) ??
                   new SessionUserInfo();
            set => SetObject(_httpContext.Session, UserInfoKey, value);
        }

        /// <summary>
        ///     Clears the booking information from the session
        /// </summary>
        public void ClearBookingInfo()
        {
            SetObject(_httpContext.Session, BookingInfoKey, new SessionBookingInfo());
        }

        /// <summary>
        ///     Helper method for saving objects to the session
        /// </summary>
        private void SetObject(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        ///     Helper method for getting objects from the session
        /// </summary>
        private T GetObject<T>(ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
