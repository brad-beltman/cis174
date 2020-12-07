using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Data.Repositories;
using FinalProject.Models;
using FinalProject.OpenXML;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace FinalProject
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
            // Add identity services
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DocSearchContext>()
                .AddDefaultTokenProviders();

            // Add session services
            services.AddMemoryCache();
            services.AddSession();

            // MVC services
            services.AddControllersWithViews();

            // Add dependency injection
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IReportOps), typeof(ReportOps));

            // Configure the context to fetch the DB connection string
            //services.AddDbContext<DocSearchContext>(options => options.UseSqlServer(
            //    Configuration.GetConnectionString("DocSearchContext")));
            services.AddDbContext<DocSearchContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("AzureDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter/{ReportType}/searchString/{SearchString}"
                    );
                endpoints.MapAreaControllerRoute(
                    name: "",
                    areaName: "Admin",
                    pattern: "Admin/{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter/{ReportType}/searchString/{SearchString}"
                    );
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "search",
                    pattern: "{controller=Home}/{action=Index}/{searchString?}");
            });

            // Creates the admin role on startup if it doesn't exist, so we can access the admin area for the first time
            DocSearchContext.CreateAdminRole(app.ApplicationServices).Wait();
        }
    }
}
