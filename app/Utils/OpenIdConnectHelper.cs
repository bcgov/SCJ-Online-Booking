using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace SCJ.Booking.MVC.Utils
{
    public class OpenIdConnectHelper
    {
        /// <summary>
        ///     This is a placeholder for code that needs to be called after OnTokenValidated
        /// </summary>
        public static void HandleUserLogin(TokenValidatedContext tokenCtx)
        {
            // add any code that needs to execute when the use logs in here
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
    }
}
