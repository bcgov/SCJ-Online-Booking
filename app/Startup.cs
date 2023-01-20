using System;
using Community.Microsoft.Extensions.Caching.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SCJ.Booking.Data;
using SCJ.Booking.MVC.Services;

namespace SCJ.Booking.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
            }

            // this setting is needed because NTLM auth does not work by default on Unix 
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);

            services.AddMvc(options => options.EnableEndpointRouting = false);

            //services
            services.AddTransient<ScBookingService>();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    "confirmbooking",
                    "{controller}/{action}/{caseId}/{locationId}/{containerId}/{bookingTime}");
            });
        }

        // automatically run migrations at startup
        private void UpdateDatabase(IApplicationBuilder app)
        {
            var platform = new Platform();

            using (IServiceScope serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context =
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    string cs = Configuration["ConnectionString"] ?? // environment variable
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
