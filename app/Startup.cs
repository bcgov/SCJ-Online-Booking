using System;
using System.Threading.Tasks;
using Community.Microsoft.Extensions.Caching.PostgreSql;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using SCJ.Booking.Data;
using SCJ.Booking.MVC.Services;
using SCJ.Booking.MVC.Services.COA;
using SCJ.Booking.MVC.Services.SC;
using SCJ.Booking.MVC.Utils;
using SerilogLoggerFactory = Serilog.Extensions.Logging.SerilogLoggerFactory;

namespace SCJ.Booking.MVC
{
    public class Startup
    {
        private const int AuthExpiryMinutes = 120;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var oidcRealmUri =
                $"{Configuration["Keycloak:Domain"]}/auth/realms/{Configuration["Keycloak:Realm"]}";
            string oidcClientId = Configuration["Keycloak:ClientId"];
            string oidcClientSecret = Configuration["KEYCLOAK_CLIENT_SECRET"];

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddApplicationDbContext(Configuration);

            services.AddSession(options =>
            {
                options.Cookie.Name = "ScjBooking.Session";
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<SessionService>();
            services.AddSingleton<ScCacheService>();
            services.AddSingleton<Services.COA.CacheService>();

            if (Configuration["TAG_NAME"] == "localdev")
            {
                // Use memory cache for sessions and caching on local development
                services.AddDistributedMemoryCache();
            }
            else
            {
                // Use a PostgreSQL table for sessions and caching on OpenShift (to support load balancing)
                services.AddDistributedPostgreSqlCache(options =>
                {
                    options.ConnectionString = Configuration["ConnectionString"];
                    options.SchemaName = "public";
                    options.TableName = "aspnet_cache";
                    options.CreateInfrastructure = true;
                });

                // Use a PostgreSQL table for session encryption keys
                services
                    .AddDataProtection()
                    .PersistKeysToDbContext<ApplicationDbContext>()
                    .SetApplicationName("SCJ-Online-Booking");
            }

            // this setting is needed because NTLM auth does not work by default on Unix
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services
                .AddAuthentication(options =>
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
                            // only store the id_token (for logout) not the other tokens
                            // that would be included by options.SaveTokens. This makes the cookie
                            // much smaller
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
                            // add extra user oidc claims and save the user to our db
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
                                // redirect to our custom sso page to pick an identity provider
                                string ssoUrl = "/scjob/sso";
                                var pathString = ctx.Request.Path.ToString();
                                string court = pathString.ToLower().Contains("/coa") ? "coa" : "sc";
                                ctx.Response.Redirect($"{ssoUrl}?court={court}");
                                ctx.HandleResponse();
                                return Task.FromResult(0);
                            }

                            // Add parameters to the keycloak redirect querystring

                            // add an idp hint to bypass the default keycloak SSO page
                            ctx.ProtocolMessage.SetParameter("kc_idp_hint", idp);

                            // change the redirect_uri to the reverse proxy
                            string proxyHost = OpenIdConnectHelper.GetProxyHost(ctx.Request);
                            if (proxyHost != null)
                            {
                                ctx.ProtocolMessage.SetParameter(
                                    "redirect_uri",
                                    $"https://{proxyHost}/scjob/signin-oidc"
                                );
                            }

                            // add a parameter to specify the proof configuration to use for digital
                            // credentials (these configurations are pre-created by Pathfinder SSO)
                            ctx.ProtocolMessage.SetParameter(
                                "pres_req_conf_id",
                                Configuration["Keycloak:DigitalCredential:ConfigId"]
                            );

                            return Task.FromResult(0);
                        },
                        OnRedirectToIdentityProviderForSignOut = ctx =>
                        {
                            // change the post-logout redirect_uri to the reverse proxy
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
                            // change the post-logout redirect_uri to the reverse proxy
                            string proxyHost = OpenIdConnectHelper.GetProxyHost(ctx.Request);
                            if (proxyHost != null)
                            {
                                ctx.Properties.RedirectUri = $"https://{proxyHost}/scjob";
                            }
                            return Task.FromResult(0);
                        },
                        OnRemoteFailure = ctx =>
                        {
                            var logger = new SerilogLoggerFactory().CreateLogger<Startup>();
                            logger.LogWarning("SCJOB KC OnRemoteFailure redirect to '/scjob'");
                            logger.LogWarning(ctx.Failure?.ToString() ?? "ctx.Failure is null");
                            ctx.Response.Redirect("/scjob");
                            ctx.HandleResponse();
                            return Task.FromResult(0);
                        }
                    };
                });

            //services
            services.AddTransient<ScCoreService>();
            services.AddTransient<ScTrialBookingService>();
            services.AddTransient<ScLongChambersBookingService>();
            services.AddTransient<ScConferenceBookingService>();
            services.AddTransient<CoaBookingService>();
            services.AddTransient<SelectListService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/scjob");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // run migrations
            UpdateDatabase(app);

            if (env.IsDevelopment())
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

            // Resolve the required services here
            var distributedCache = app.ApplicationServices.GetRequiredService<IDistributedCache>();
            var distributedTicketStore = new DistributedTicketStore(
                distributedCache,
                TimeSpan.FromMinutes(AuthExpiryMinutes)
            );

            // Get the CookieAuthenticationOptions and set the SessionStore dynamically
            var cookieOptionsMonitor = app.ApplicationServices.GetRequiredService<
                IOptionsMonitor<CookieAuthenticationOptions>
            >();
            var cookieOptions = cookieOptionsMonitor.Get(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            // Set the SessionStore to DistributedTicketStore
            cookieOptions.SessionStore = distributedTicketStore;
        }

        // automatically run migrations at startup
        private void UpdateDatabase(IApplicationBuilder app)
        {
            var platform = new Platform();

            using (
                IServiceScope serviceScope = app
                    .ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope()
            )
            {
                using (
                    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                )
                {
                    string cs =
                        Configuration["ConnectionString"]
                        ?? // environment variable
                        Configuration["Data:DefaultConnection:ConnectionString"]; // appsettings.json

                    // the migrations should run on pretty much any platform except Mac localdev environments
                    if (cs != string.Empty || !platform.UseInMemoryStore)
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }
    }
}
