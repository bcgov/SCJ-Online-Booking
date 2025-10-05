using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

public class DistributedTicketStore : ITicketStore
{
    private readonly IDistributedCache _distributedCache;
    private readonly TimeSpan _expiration;

    public DistributedTicketStore(IDistributedCache distributedCache, TimeSpan expiration)
    {
        _distributedCache = distributedCache;
        _expiration = expiration;
    }

    public async Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = Guid.NewGuid().ToString();
        var ticketData = SerializeTicket(ticket);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _expiration
        };
        await _distributedCache.SetStringAsync(key, ticketData, options);
        return key;
    }

    public async Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        var ticketData = SerializeTicket(ticket);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _expiration
        };
        await _distributedCache.SetStringAsync(key, ticketData, options);
    }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public async Task<AuthenticationTicket?> RetrieveAsync(string key)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    {
        var ticketData = await _distributedCache.GetStringAsync(key);
        if (ticketData != null)
        {
            return DeserializeTicket(ticketData);
        }
        return null;
    }

    public Task RemoveAsync(string key)
    {
        return _distributedCache.RemoveAsync(key);
    }

    private string SerializeTicket(AuthenticationTicket ticket)
    {
        var ticketModel = new TicketModel
        {
            AuthenticationScheme = ticket.AuthenticationScheme,
            Properties = ticket.Properties,
            Claims = ticket
                .Principal.Claims.Select(c => new ClaimModel
                {
                    Type = c.Type,
                    Value = c.Value,
                    Issuer = c.Issuer,
                    OriginalIssuer = c.OriginalIssuer
                })
                .ToList()
        };

        return JsonConvert.SerializeObject(ticketModel);
    }

    private AuthenticationTicket DeserializeTicket(string serializedTicket)
    {
        var ticketModel = JsonConvert.DeserializeObject<TicketModel>(serializedTicket);

        var claims = ticketModel.Claims.Select(c => new System.Security.Claims.Claim(
            c.Type,
            c.Value,
            null,
            c.Issuer,
            c.OriginalIssuer
        ));
        var identity = new System.Security.Claims.ClaimsIdentity(
            claims,
            ticketModel.AuthenticationScheme
        );
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);

        return new AuthenticationTicket(
            principal,
            ticketModel.Properties,
            ticketModel.AuthenticationScheme
        );
    }

    private class TicketModel
    {
        public string AuthenticationScheme { get; set; }
        public AuthenticationProperties Properties { get; set; }
        public List<ClaimModel> Claims { get; set; }
    }

    private class ClaimModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string Issuer { get; set; }
        public string OriginalIssuer { get; set; }
    }
}
