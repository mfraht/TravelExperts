// -------------------------------------------------------------------------------- //
// ------------------------------------- START UP --------------------------------- //
// -------------------------------------------------------------------------------- //

/* -------------------------------------------------------------------------------  */
/* --------------------------------Team3 - Group2 -------------------------------  */

/* -------------------------------Date: 10-10-2021 -------------------------------  */
/* -------------------Purpose: THREADED PROJECT OF PROJ-009-004 ------------------  */
/* -------------------------------------------------------------------------------  */

// -------------------------------------------------------------------------------- //



using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Models;


namespace TravelExperts
{
    // ------------------------------- Definition of Startup class ---------------------------------- //
    // ---------------------------------------- start ----------------------------------------------- //
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ------ This method gets called by the runtime. Use this method to add services to the container. ------ //
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMemoryCache();
            services.AddSession();

            services.AddControllersWithViews().AddNewtonsoftJson();

            services.AddDbContext<TravelExpertsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TravelExpertsConnection")
                , b => b.MigrationsAssembly(typeof(Startup).Namespace)));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<TravelExpertsContext>()
              .AddDefaultTokenProviders();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }


        // ------ This method gets called by the runtime. Use this method to configure the HTTP request pipeline. ------ //
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // ------ The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // -------- For Authentication of Log in page
            app.UseAuthorization();  // -------- For Authorization of Log in page

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                // ------------------------- Route for Admin area ------------------------------- //
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Package}/{action=Index}/{id?}");

                // -------------- Route for paging, sorting, and filtering ---------------------- //
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter/{author}/{genre}/{price}");

                // ------------------ Route for paging and sorting only ----------------------- //
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}");

                // --------------------------- Default route ---------------------------------- //
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });

            TravelExpertsContext.CreateAdminUser(app.ApplicationServices).Wait();
        }
        // ------------------------------- Definition of Startup class ---------------------------------- //
        // ----------------------------------------- end ------------------------------------------------ //
    }
}
