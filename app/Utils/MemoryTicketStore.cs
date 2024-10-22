using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;

public class MemoryTicketStore : ITicketStore
{
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _expiration;

    public MemoryTicketStore(IMemoryCache memoryCache, TimeSpan expiration)
    {
        _memoryCache = memoryCache;
        _expiration = expiration;
    }

    public Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = Guid.NewGuid().ToString();
        _memoryCache.Set(key, ticket, _expiration);
        return Task.FromResult(key);
    }

    public Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        _memoryCache.Set(key, ticket, _expiration);
        return Task.CompletedTask;
    }

    public Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        _memoryCache.TryGetValue(key, out AuthenticationTicket? ticket);
        return Task.FromResult(ticket);
    }

    public Task RemoveAsync(string key)
    {
        _memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}
