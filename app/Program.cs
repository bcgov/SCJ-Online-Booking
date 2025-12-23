using System;
using System.Threading.Tasks;
using Community.Microsoft.Extensions.Caching.PostgreSql;
using DotEnv.Core;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using SCJ.Booking.Data;
using SCJ.Booking.MVC.Middleware;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.COA;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.Utils;
using SerilogLoggerFactory = Serilog.Extensions.Logging.SerilogLoggerFactory;

new EnvLoader().Load();

var builder = WebApplication.CreateBuilder(args);
const int AuthExpiryMinutes = 120;

var configuration = builder.Configuration;

var oidcRealmUri =
    $"{configuration["Keycloak:Domain"]}/auth/realms/{configuration["Keycloak:Realm"]}";
string oidcClientId = configuration["Keycloak:ClientId"];
string oidcClientSecret = configuration["KEYCLOAK_CLIENT_SECRET"];

// Configure services
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddApplicationDbContext(configuration);

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "ScjBooking.Session";
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddSingleton<ScCacheService>();
builder.Services.AddSingleton<SCJ.Booking.MVC.Services.COA.CacheService>();

if (configuration["TAG_NAME"] == "localdev")
{
    builder.Services.AddDistributedMemoryCache();
}
else
{
    builder.Services.AddDistributedPostgreSqlCache(options =>
    {
        options.ConnectionString = configuration["ConnectionString"];
        options.SchemaName = "public";
        options.TableName = "aspnet_cache";
        options.CreateInfrastructure = true;
    });

    builder
        .Services.AddDataProtection()
        .PersistKeysToDbContext<ApplicationDbContext>()
        .SetApplicationName("SCJ-Online-Booking");
}

AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.LoginPath = "/home/NotAuthorized";
        options.AccessDeniedPath = "/home/NotAuthorized";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(AuthExpiryMinutes);
    })
    .AddOpenIdConnect(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = oidcRealmUri;
        options.RequireHttpsMetadata = true;
        options.ClientId = oidcClientId;
        options.ClientSecret = oidcClientSecret;
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            ValidateIssuer = true
        };
        options.Events = new OpenIdConnectEvents
        {
            OnTokenResponseReceived = c =>
            {
                c.Properties.StoreTokens(
                    new[]
                    {
                        new AuthenticationToken
                        {
                            Name = OidcConstants.TokenTypes.IdentityToken,
                            Value = c.TokenEndpointResponse.IdToken
                        }
                    }
                );
                c.Properties.IsPersistent = true;
                return Task.CompletedTask;
            },
            OnTokenValidated = async ctx =>
            {
                await OpenIdConnectHelper.HandleUserLogin(ctx);
            },
            OnRedirectToIdentityProvider = ctx =>
            {
                var idp = ctx.Request.Query["kc_idp_hint"];
                if (
                    idp != OpenIdConnectHelper.BceidIdp
                    && idp != OpenIdConnectHelper.DigitalCredentialIdp
                )
                {
                    string ssoUrl = "/scjob/sso";
                    var pathString = ctx.Request.Path.ToString();
                    string court = pathString.ToLower().Contains("/coa") ? "coa" : "sc";
                    ctx.Response.Redirect($"{ssoUrl}?court={court}");
                    ctx.HandleResponse();
                    return Task.FromResult(0);
                }

                ctx.ProtocolMessage.SetParameter("kc_idp_hint", idp);

                string proxyHost = OpenIdConnectHelper.GetProxyHost(ctx.Request);
                if (proxyHost != null)
                {
                    ctx.ProtocolMessage.SetParameter(
                        "redirect_uri",
                        $"https://{proxyHost}/scjob/signin-oidc"
                    );
                }

                ctx.ProtocolMessage.SetParameter(
                    "pres_req_conf_id",
                    configuration["Keycloak:DigitalCredential:ConfigId"]
                );

                return Task.FromResult(0);
            },
            OnRedirectToIdentityProviderForSignOut = ctx =>
            {
                string proxyHost = OpenIdConnectHelper.GetProxyHost(ctx.Request);
                if (proxyHost != null)
                {
                    ctx.ProtocolMessage.SetParameter(
                        "post_logout_redirect_uri",
                        $"https://{proxyHost}/scjob/signout-callback-oidc"
                    );
                }
                return Task.FromResult(0);
            },
            OnSignedOutCallbackRedirect = ctx =>
            {
                string proxyHost = OpenIdConnectHelper.GetProxyHost(ctx.Request);
                if (proxyHost != null)
                {
                    ctx.Properties.RedirectUri = $"https://{proxyHost}/scjob";
                }
                return Task.FromResult(0);
            },
            OnRemoteFailure = ctx =>
            {
                var logger = new SerilogLoggerFactory().CreateLogger<Program>();
                logger.LogWarning("SCJOB KC OnRemoteFailure redirect to '/scjob'");
                logger.LogWarning(ctx.Failure?.ToString() ?? "ctx.Failure is null");
                ctx.Response.Redirect("/scjob");
                ctx.HandleResponse();
                return Task.FromResult(0);
            }
        };
    });

builder.Services.AddTransient<ScCoreBookingService>();
builder.Services.AddTransient<ScTrialBookingService>();
builder.Services.AddTransient<ScLongChambersBookingService>();
builder.Services.AddTransient<ScConferenceBookingService>();
builder.Services.AddTransient<CoaBookingService>();
builder.Services.AddTransient<SelectListService>();
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();

var app = builder.Build();

app.UsePathBase("/scjob");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add maintenance mode check early in the pipeline
app.UseMiddleware<MaintenanceModeMiddleware>();

// Run migrations
var platform = new SCJ.Booking.MVC.Platform();
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
    {
        string cs =
            configuration["ConnectionString"]
            ?? configuration["Data:DefaultConnection:ConnectionString"];
        if (cs != string.Empty || !platform.UseInMemoryStore)
        {
            context.Database.Migrate();
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();

app.UseMvc(routes =>
{
    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
    routes.MapRoute(
        "confirmbooking",
        "{controller}/{action}/{caseId}/{locationId}/{containerId}/{bookingTime}"
    );
});

// Configure distributed ticket store
var distributedCache = app.Services.GetRequiredService<IDistributedCache>();
var distributedTicketStore = new DistributedTicketStore(
    distributedCache,
    TimeSpan.FromMinutes(AuthExpiryMinutes)
);

var cookieOptionsMonitor = app.Services.GetRequiredService<
    IOptionsMonitor<CookieAuthenticationOptions>
>();
var cookieOptions = cookieOptionsMonitor.Get(CookieAuthenticationDefaults.AuthenticationScheme);
cookieOptions.SessionStore = distributedTicketStore;

app.Run();
