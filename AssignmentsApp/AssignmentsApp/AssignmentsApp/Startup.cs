using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AssignmentsApp.Areas.Module6.Data;
using AssignmentsApp.Areas.Module8.Data;
using AssignmentsApp.Areas.Module8.Models.DataLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Hosting;

namespace AssignmentsApp
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
            services.AddMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<Module6Context>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureModule6And7")));
            services.AddDbContext<Module8Context>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureModule8")));
            //services.AddDbContext<Module8Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Module8Context")));
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

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "module5",
                    areaName: "Module5",
                    pattern: "Module5/{controller=Home}/{action=Index}");

                endpoints.MapAreaControllerRoute(
                    name: "module5",
                    areaName: "Module5",
                    pattern: "Module5/{controller=Home}/Custom");

                endpoints.MapAreaControllerRoute(
                    name: "module6_game_category",
                    areaName: "Module6",
                    pattern: "Module6/{controller=Home}/{action=Index}/{ActiveGame=all}/{ActiveCategory=all}");

                endpoints.MapAreaControllerRoute(
                    name: "",
                    areaName: "Module8",
                    pattern: "Module8/{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}");

                endpoints.MapAreaControllerRoute(
                    name: "module8",
                    areaName: "Module8",
                    pattern: "Module8/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
