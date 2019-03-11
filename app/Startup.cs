using System;
using Community.Microsoft.Extensions.Caching.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using SCJ.Booking.MVC.Data;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddApplicationDbContext(Configuration);

            services.AddSession(options => {
                options.Cookie.Name = "ScjBooking.Session";
                options.Cookie.HttpOnly = true;
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddScoped<SessionService>();

            if (Configuration["TAG_NAME"] == "localdev")
            {
                // Use memory cache for for sessions and caching on local development
                services.AddMemoryCache();
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

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //service to get hearings left for logged-in user
            services.AddTransient<BookingService>();

            services.AddScoped<IViewRenderService, ViewRenderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UsePathBase("/scjob");

            // run migrations
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    string cs = Environment.GetEnvironmentVariable("ConnectionString") ?? string.Empty;

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
