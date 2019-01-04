using System.ServiceModel;
using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs
{
    public static class OnlineBookingClientFactory
    {
        /// <summary>
        ///     Factory method for creating online booking clients
        /// </summary>
        public static IOnlineBooking GetClient(bool fake)
        {
            if (fake)
            {
                return new FakeOnlineBookingClient();
            }

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            // todo: this should be stored in an OpenShift environment variable
            var endpointAddress = new EndpointAddress("https://localhost:8092/OnlineBooking.svc");

            // use basic authentication to connect
            var factory = new ChannelFactory<IOnlineBooking>(binding, endpointAddress);

            // todo: these should be stored as OpenShift secrets
            factory.Credentials.UserName.UserName = "username";
            factory.Credentials.UserName.Password = "password";

            return factory.CreateChannel();
        }
    }
}
