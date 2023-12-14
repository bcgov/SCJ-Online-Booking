using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SCJ.Booking.MVC.Utils;

namespace SCJ.Booking.MVC.Services
{
    public class SessionService
    {
        // session keys
        private const string ScBookingInfoKey = "ScBookingInfo";
        private const string CoaBookingInfoKey = "CoaBookingInfo";
        private const string UserInfoKey = "UserInfo";

        // services
        private readonly HttpContext _httpContext;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        /// <summary>
        ///     Get user information based on the session variables and custom headers. Session variables would
        ///     get preference.
        /// </summary>
        public SessionUserInfo GetUserInformation()
        {
            return new SessionUserInfo
            {
                Phone = string.IsNullOrEmpty(UserInfo.Phone) ? string.Empty : UserInfo.Phone,
                Email = string.IsNullOrEmpty(UserInfo.Email) ? string.Empty : UserInfo.Email
            };
        }

        /// <summary>
        ///     Stores information about the booking in the session
        /// </summary>
        public ScSessionBookingInfo ScBookingInfo
        {
            get =>
                GetObject<ScSessionBookingInfo>(_httpContext.Session, ScBookingInfoKey)
                ?? new ScSessionBookingInfo();
            set => SetObject(_httpContext.Session, ScBookingInfoKey, value);
        }

        /// <summary>
        ///     Stores information about the booking in the session
        /// </summary>
        public CoaSessionBookingInfo CoaBookingInfo
        {
            get =>
                GetObject<CoaSessionBookingInfo>(_httpContext.Session, CoaBookingInfoKey)
                ?? new CoaSessionBookingInfo();
            set => SetObject(_httpContext.Session, CoaBookingInfoKey, value);
        }

        /// <summary>
        ///     Stores information about the user in the session
        /// </summary>
        public SessionUserInfo UserInfo
        {
            get =>
                GetObject<SessionUserInfo>(_httpContext.Session, UserInfoKey)
                ?? new SessionUserInfo();
            set => SetObject(_httpContext.Session, UserInfoKey, value);
        }

        /// <summary>
        ///     Clears the booking information from the session
        /// </summary>
        public void ClearBookingInfo()
        {
            SetObject(_httpContext.Session, ScBookingInfoKey, new ScSessionBookingInfo());
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
