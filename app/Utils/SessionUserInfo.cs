namespace SCJ.Booking.MVC.Utils
{
    public class SessionUserInfo
    {
        private string _email;
        private string _phone;
        private string _contactName;

        public string Email
        {
            get => _email ?? string.Empty;
            set => _email = value;
        }

        public string Phone
        {
            get => _phone ?? string.Empty;
            set => _phone = value;
        }

        public string ContactName
        {
            get => _contactName ?? string.Empty;
            set => _contactName = value;
        }
    }
}
