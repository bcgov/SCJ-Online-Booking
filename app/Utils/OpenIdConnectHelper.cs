using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;

namespace SCJ.Booking.MVC.Utils
{
    public class OpenIdConnectHelper
    {
        public const string BceidIdp = "bceidboth";
        public const string DigitalCredentialIdp = "digitalcredential";
        private const string BceidUserGuidClaim = "bceid_user_guid";
        private const string IdentityProviderClaim = "identity_provider";

        /// <summary>
        ///     This is a placeholder for code that needs to be called after OnTokenValidated
        /// </summary>
        public static async Task HandleUserLogin(TokenValidatedContext tokenCtx)
        {
            var idp = tokenCtx.Principal.FindFirstValue(IdentityProviderClaim);

            string identifier;
            OidcUser.CredentialTypeLookup idpType;

            switch (idp)
            {
                case BceidIdp:
                    identifier = tokenCtx.Principal.FindFirstValue(BceidUserGuidClaim);
                    idpType = OidcUser.CredentialTypeLookup.Bceid;
                    break;
                case DigitalCredentialIdp:
                    // Note: digital credential users do not have a unique identifier and
                    // we will insert a new record in the Users table every time they login
                    identifier = tokenCtx.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                    idpType = OidcUser.CredentialTypeLookup.DigitalCredential;
                    break;
                default:
                    identifier = string.Empty;
                    idpType = OidcUser.CredentialTypeLookup.None;
                    break;
            }

            if (idpType != OidcUser.CredentialTypeLookup.None)
            {
                var userId = await InsertOrUpdateUser(tokenCtx, idpType, identifier);
                var claims = new List<Claim> { new(ClaimTypes.Sid, userId.ToString()) };
                var appIdentity = new ClaimsIdentity(claims);
                tokenCtx.Principal?.AddIdentity(appIdentity);
            }
        }

        /// <summary>
        ///     Helper method for getting redirect urls
        /// </summary>
        public static string GetProxyHost(HttpRequest request)
        {
            if (request.Headers.ContainsKey("X-Forwarded-Server"))
            {
                return request.Headers["X-Forwarded-Server"][0];
            }

            if (request.Headers.ContainsKey("X-Forwarded-Host"))
            {
                return request.Headers["X-Forwarded-Host"][0];
            }

            return null;
        }

        /// <summary>
        ///     Gets the user's full name from the user claims
        /// </summary>
        public static string GetUserFullName(ClaimsPrincipal user)
        {
            var idp = user.FindFirstValue(IdentityProviderClaim);

            switch (idp)
            {
                case BceidIdp:
                    return user.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
                case DigitalCredentialIdp:
                {
                    var json = user.FindFirst("vc_presented_attributes")?.Value ?? "{}";
                    dynamic attributes = JsonConvert.DeserializeObject<ExpandoObject>(json);
                    return $"{attributes.given_names} {attributes.family_name}";
                }
                default:
                    return "Unknown User";
            }
        }

        /// <summary>
        ///     Creates a new record in the Users table, or updates the LastLogin timestamp
        ///     if a record already exists.
        /// </summary>
        private static async Task<long> InsertOrUpdateUser(
            TokenValidatedContext tokenCtx,
            OidcUser.CredentialTypeLookup idpType,
            string identifier
        )
        {
            var dbCtx =
                tokenCtx.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

            var user = await dbCtx.Users.FirstOrDefaultAsync(u =>
                u.CredentialType == idpType && u.UniqueIdentifier == identifier
            );

            long userId;

            if (user == null)
            {
                var newUser = new OidcUser
                {
                    CredentialType = idpType,
                    UniqueIdentifier = identifier,
                    LastLogin = DateTime.Now
                };

                await dbCtx.Users.AddAsync(newUser);
                await dbCtx.SaveChangesAsync();
                userId = newUser.Id;
            }
            else
            {
                userId = user.Id;
                if (!user.LastLogin.HasValue || user.LastLogin.Value < DateTime.Now.AddMinutes(-1))
                {
                    user.LastLogin = DateTime.Now;

                    dbCtx.Users.Update(user);
                    await dbCtx.SaveChangesAsync();
                }
            }

            return userId;
        }
    }
}
