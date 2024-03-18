using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private const string EmailClaim =
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        private const string NameClaim =
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

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

                    userId = await dbCtx.SaveChangesAsync();
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

                        dbCtx.Users.Update(user);
                        await dbCtx.SaveChangesAsync();
                    }
                }

                var claims = new List<Claim> { new(ClaimTypes.Sid, userId.ToString()), };

                var appIdentity = new ClaimsIdentity(claims);

                tokenCtx.Principal?.AddIdentity(appIdentity);
            }
        }

        /// <summary>
        ///     Checks if the access token needs to be refreshed, and uses the refresh token to update
        ///     the access token.
        ///     The decryption of the cookie has already happened so we have access to the user claims
        ///     and cookie properties - expiration, etc..
        /// </summary>
        /// <remarks>
        ///     Based on https://github.com/mderriey/aspnet-core-token-renewal
        /// </remarks>
        public static async Task HandleOidcRefreshToken(
            CookieValidatePrincipalContext context,
            string oidcRealmUri,
            string oidcClientId,
            string oidcClientSecret
        )
        {
            // Since our cookie lifetime is based on the access token one,
            // check if we're more than halfway of the cookie lifetime
            DateTimeOffset now = DateTimeOffset.UtcNow;
            TimeSpan timeElapsed = now.Subtract(context.Properties.IssuedUtc.Value);
            TimeSpan timeRemaining = context.Properties.ExpiresUtc.Value.Subtract(now);

            if (timeElapsed > timeRemaining)
            {
                var identity = (ClaimsIdentity)context.Principal.Identity;
                Claim accessTokenClaim = identity.FindFirst("access_token");
                Claim refreshTokenClaim = identity.FindFirst("refresh_token");

                // If we have to refresh, grab the refresh token from the claims, and request
                // new access token and refresh token
                string refreshToken = refreshTokenClaim.Value;
                TokenResponse response = await new HttpClient().RequestRefreshTokenAsync(
                    new RefreshTokenRequest
                    {
                        Address = $"{oidcRealmUri}/protocol/openid-connect/token",
                        ClientId = oidcClientId,
                        ClientSecret = oidcClientSecret,
                        RefreshToken = refreshToken
                    }
                );

                if (!response.IsError)
                {
                    // Everything went right, remove old tokens and add new ones
                    identity.RemoveClaim(accessTokenClaim);
                    identity.RemoveClaim(refreshTokenClaim);

                    identity.AddClaims(
                        new[]
                        {
                            new Claim("access_token", response.AccessToken),
                            new Claim("refresh_token", response.RefreshToken)
                        }
                    );

                    // Indicate to the cookie middleware to renew the session cookie.
                    // The new lifetime will be the same as the old one, so the alignment
                    // between cookie and access token is preserved
                    context.ShouldRenew = true;
                }
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
