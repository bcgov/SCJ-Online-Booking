using System.ServiceModel;
using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs
{
    public static class OnlineBookingClientFactory
    {
        /// <summary>
        ///     Factory method for creating online booking clients
        /// </summary>
        public static IOnlineBooking GetClient(bool isLocalDev = false)
        {
            string env = System.Environment.GetEnvironmentVariable("TAG_NAME") ?? string.Empty;

            if (env.ToLower().Equals("localdev") || isLocalDev)
                return new FakeOnlineBookingClient();

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            // todo: this should be stored in an OpenShift environment variable
            var endpointAddress = new EndpointAddress(System.Environment.GetEnvironmentVariable("API_ENDPOINT"));

            // use basic authentication to connect
            var factory = new ChannelFactory<IOnlineBooking>(binding, endpointAddress);

            // todo: these should be stored as OpenShift secrets
            factory.Credentials.UserName.UserName = System.Environment.GetEnvironmentVariable("API_USERNAME");
            factory.Credentials.UserName.Password = System.Environment.GetEnvironmentVariable("API_PASSWORD");

            return factory.CreateChannel();
        }
    }
}
