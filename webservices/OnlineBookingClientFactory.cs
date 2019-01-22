using System.ServiceModel;
using Microsoft.Extensions.Configuration;
using SCJ.SC.OnlineBooking;

namespace SCJ.Booking.RemoteAPIs
{
    public static class OnlineBookingClientFactory
    {
        /// <summary>
        ///     Factory method for creating online booking clients
        /// </summary>
        public static IOnlineBooking GetClient(IConfiguration configuration)
        {
            string env = configuration["TAG_NAME"] ?? string.Empty;

            if (env.ToLower().Equals("localdev"))
                return new FakeOnlineBookingClient();

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            // todo: this should be stored in an OpenShift environment variable
            var endpointAddress = new EndpointAddress(configuration["API_ENDPOINT"]);

            // use basic authentication to connect
            var factory = new ChannelFactory<IOnlineBooking>(binding, endpointAddress);

            // todo: these should be stored as OpenShift secrets
            factory.Credentials.UserName.UserName = configuration["API_USERNAME"];
            factory.Credentials.UserName.Password = configuration["API_PASSWORD"];

            return factory.CreateChannel();
        }
    }
}
