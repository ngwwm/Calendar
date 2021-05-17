using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
/* .netcore 3.0 begin */
using Microsoft.Extensions.Hosting;
/* .netcore 3.0 end */
using Calendar.Data;
using Calendar.Models;
using Calendar.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
/* .netcore 3.0 begin */
//using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
/* .netcore 3.0 end */
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace Calendar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //            var builder = new ConfigurationBuilder()
            //                .SetBasePath(env.ContentRootPath)
            //                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //            if (env.IsDevelopment())
            //            {
            //                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
            //                // builder.AddUserSecrets();
            //                builder.AddUserSecrets<Startup>();
            //            }

            //            builder.AddEnvironmentVariables();
            //            Configuration = builder.Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            /* services.AddDatabaseDeveloperPageExceptionFilter();*/
            services.AddDatabaseDeveloperPageExceptionFilter();
            /* This service is added when you create the project with "Authentiation: Individual User Accounts" */
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            /* .netcore 2.0 begin */
            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            /* Use cookies without identity */

            // If you don't want the cookie to be automatically authenticated and assigned to HttpContext.User, 
            // remove the CookieAuthenticationDefaults.AuthenticationScheme parameter passed to AddAuthentication.
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

            services.AddAuthentication(o => { o.DefaultAuthenticateScheme = "CalendarApp"; o.DefaultSignInScheme = "CalendarApp"; })
                .AddCookie("CalendarApp", options => {
                    //options.LoginPath = "/Account/LogIn";
                    //options.LogoutPath = "/Account/LogOff";
                    options.LoginPath = new PathString("/login");
                 });

            /* Begin external auth */
            //services.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    Events = new CookieAuthenticationEvents
            //    {
            //        // You will need this only if you use Ajax calls with a library not compatible with IsAjaxRequest
            //        // More info here: https://github.com/aspnet/Security/issues/1056
            //        OnRedirectToAccessDenied = context =>
            //        {
            //            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            //            return TaskCache.CompletedTask;
            //        }
            //    },
            //    AuthenticationScheme = "CalendarApp",
            //    LoginPath = new PathString("/login"),
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});
            /* End external auth */

            /* .netcore 2.0 end */

            /* .netcore 3.0 begin */
            /* .netcore 2.2 begin */
            /* .netcore 2.1 begin */
            /*
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            */
            services.AddRazorPages()
                    .AddRazorRuntimeCompilation()
                    .AddNewtonsoftJson();  /* https://github.com/dotnet/corefx/issues/40120 */
            
            /* .netcore 2.1 end */
            /* .netcore 2.2 end */
            /* .netcore 3.0 end */

            // Add application services.

            /* Statistics on Events by Team/Project used in the navigation menu in LHS. */
            services.AddTransient<Calendar.Models.Services.EventStatisticsService>();     
            /* Provides LOV/constants type of things */  
            services.AddTransient<Calendar.Models.Services.StaticListOfValuesService>();
            /* Expose the Project List */
            services.AddTransient<Calendar.Controllers.ProjectsController>();
            /* Expose the Team List */
            services.AddTransient<Calendar.Controllers.TeamsController>();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            /* Ldap Auth */
            services.Configure<LdapConfig>(Configuration.GetSection("ldap"));
            services.AddScoped<IAuthenticationService, LdapAuthenticationService>();
            /* appsetting */
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<Calendar.Controllers.AppSettingsController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /* .netcore 2.2 begin, logging moved to Program.Main() 
         * public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
         * .netcore 2.2 end */
        /* .netcore 3.0 begin
         * public void Configure(IApplicationBuilder app, IHostingEnvironment env)
         * .netcore 3.0 end */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /* .netcore 2.2 begin, logging moved to Program.Main() 
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            .netcore 2.2 end */

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                /* begin .net5 
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                .net5 end*/
                /* begin .netcore 2.1
                app.UseBrowserLink();
                   .netcore 2.1 end */
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

//            app.UseStaticFiles();

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".msg"] = "application/octect-stream";         

            app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });
          
            /* .netcore 2.0 begin */
            app.UseAuthentication();
            //app.UseIdentity();
            /* .netcore 2.0 end */

            /* .netcore 3.0 begin */
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    "default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    "login", "{controller=User}/{action=Login}");
                endpoints.MapControllerRoute(
                    "logout", "{controller=User}/{action=Logout}");
            });
            /*
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "login",
                    template: "login",
                    defaults: new { controller = "User", action = "Login" }
                );
                routes.MapRoute(
                    name: "logout",
                    template: "logout",
                    defaults: new { controller = "User", action = "Logout" }
                );
            });
            */
            /* .netcore 3.0 end */
            //SeedData.Initialize(app.ApplicationServices);

        }
    }
}
