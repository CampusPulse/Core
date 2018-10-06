﻿using CampusPulse.Core.Service.Bootstrap;
using CampusPulse.Core.Service.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CampusPulse.Core.Service
{
    public class ServiceWebHost<Tstartup> where Tstartup : class
    {
        public static IWebHost BuildWebHost(string[] args)
        {
            var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine(currentEnv);

            var baseRoot = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
               .SetBasePath(baseRoot)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{currentEnv}.json", optional: true)
               .AddEnvironmentVariables()
               .Build();

            var elasticUri = config["ElasticConfiguration:Uri"];

           

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)

                .UseContentRoot(baseRoot)
                .UseKestrel(options =>
                {
                    options.ConfigureEndpoints();
                })
                .UseConfiguration(config);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                webHostBuilder.UseUrls("https://local.dev.campuspulse.com:44340");
            }
            return webHostBuilder.UseStartup<Tstartup>().Build();
        }
    }
}
