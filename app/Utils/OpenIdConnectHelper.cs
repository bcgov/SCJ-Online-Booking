using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SCJ.Booking.Data;
using SCJ.Booking.Data.Models;

namespace SCJ.Booking.MVC.Utils
{
    public class OpenIdConnectHelper
    {
        private const string BceidUserGuidClaim = "bceid_user_guid";
        private const string IdentityProviderClaim = "identity_provider";

        /// <summary>
        ///     This is a placeholder for code that needs to be called after OnTokenValidated
        /// </summary>
        public static async Task HandleUserLogin(TokenValidatedContext tokenCtx)
        {
            string idp = tokenCtx.Principal.FindFirstValue(IdentityProviderClaim);

            if (idp == "bceidboth")
            {
                string guid = tokenCtx.Principal.FindFirstValue(BceidUserGuidClaim);

                //Get EF context
                var dbCtx =
                    tokenCtx.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

                OidcUser user = await dbCtx.Users.FirstOrDefaultAsync(u =>
                    u.CredentialType == OidcUser.CredentialTypeLookup.KeycloakBceid
                    && u.UniqueIdentifier == guid
                );

                long userId;

                if (user == null)
                {
                    var newUser = new OidcUser
                    {
                        CredentialType = OidcUser.CredentialTypeLookup.KeycloakBceid,
                        UniqueIdentifier = guid,
                        LastLogin = DateTime.Now
                    };

                    await dbCtx.Users.AddAsync(newUser);
                    await dbCtx.SaveChangesAsync();
                    userId = newUser.Id;
                }
                else
                {
                    userId = user.Id;
                    if (
                        !user.LastLogin.HasValue
                        || user.LastLogin.Value < DateTime.Now.AddMinutes(-1)
                    )
                    {
                        user.LastLogin = DateTime.Now;

                        await dbCtx.SaveChangesAsync();
                    }
                }

                var claims = new List<Claim> { new(ClaimTypes.Sid, userId.ToString()), };

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
    }
}
