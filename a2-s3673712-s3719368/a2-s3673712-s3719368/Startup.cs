using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.LogicManger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace a2_s3673712_s3719368
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

            services.AddControllersWithViews();

            services.AddDbContext<NationBankContext>(options => //add connection string
            {
                options.UseSqlServer(Configuration.GetConnectionString(nameof(NationBankContext)));
                // Enable lazy loading.
                options.UseLazyLoadingProxies();
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options => //enable session
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1); //auto logout after 1 minute of inactivity on any logged-on page
                
                // Make the session cookie essential.
                options.Cookie.IsEssential = true;
            });
            
            services.AddControllersWithViews();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapAreaControllerRoute(
               "admin",
               "admin",
                pattern: "{controller=Admin}/{action=Login}");
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                

            });
        }
    }
}
