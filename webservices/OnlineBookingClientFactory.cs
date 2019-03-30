using System;
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
            {
                return new FakeOnlineBookingClient();
            }

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            // The conservative default value is 65536.  Allow 100 times the default
            binding.MaxReceivedMessageSize = (long) ushort.MaxValue * 100;

            var endpointAddress = new EndpointAddress(configuration["API_ENDPOINT"]);

            // use basic authentication to connect
            var factory = new ChannelFactory<IOnlineBooking>(binding, endpointAddress);

            factory.Credentials.UserName.UserName = configuration["API_USERNAME"];
            factory.Credentials.UserName.Password = configuration["API_PASSWORD"];

            return factory.CreateChannel();
        }
    }
}
