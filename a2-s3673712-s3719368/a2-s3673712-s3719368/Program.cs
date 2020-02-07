//Assignment 3: National Wealth Bank of Australasia ASP.NET Core web app Project
//Authors:    Bach Truong Dao - s3673712
//            Yongqian Huang  - s3719368
//Reference:  Codes are copied and modified from lecture examples/ lab examples from weeks 4-9
//Css stylesheet from fontawesome-icon
//images copied and modified from internet: Error.png and Login_background


using System;
using a2_s3673712_s3719368.Data;
using a2_s3673712_s3719368.LogicManger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace a2_s3673712_s3719368
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedItems.Initialize(services); //Insert seed items to database
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }
        //Program will start with log in page
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<WebListener>();
                }
                ); 
    }
}
