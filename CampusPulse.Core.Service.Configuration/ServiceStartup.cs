using CampusPulse.Core.Service.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace CampusPulse.Core.Service.Configuration
{
    public class ServiceStartup
    {
        public readonly string baseRoot;
        public readonly ILoggerFactory loggerFactory;
        public readonly IConfiguration configuration;

        public ServiceStartup(IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.loggerFactory = loggerFactory;
            this.configuration = configuration;

            this.baseRoot = Directory.GetCurrentDirectory();          

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)            
            .CreateLogger();
           
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual void ConfigureServices(IServiceCollection services)
        {
            MvcConfigurationManager.ConfigureService(services);
            CacheConfigurationManager.ConfigureService(services, this.configuration);
            LoggingConfigurationManager.ConfigureService(services);
            configureDependency(services);
            //services.AddSingleton<Serilog.ILogger, Serilog.Logger>();

            /*services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("example.com");
                options.ExcludedHosts.Add("www.example.com");
            });
            */
        }

        protected virtual void configureDependency(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddSerilog();

            ConfigureHttpsEndpoints(app, env);

            app.UseMvc(routes => ConfigureRoutes(routes));

            if (env.IsDevelopment())
            {
                app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Route not configured");
                });
            }
        }

        private void ConfigureHttpsEndpoints(IApplicationBuilder app, IHostingEnvironment env)
        {
            int? httpsPort = null;
            var httpsSection = configuration.GetSection("HttpServer:Endpoints:Https");
            if (httpsSection.Exists())
            {
                var httpsEndpoint = new EndpointConfiguration();
                httpsSection.Bind(httpsEndpoint);
                httpsPort = httpsEndpoint.Port;
            }
            var statusCode = env.IsDevelopment() ? StatusCodes.Status302Found : StatusCodes.Status301MovedPermanently;

            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(statusCode, httpsPort));
        }

        protected virtual void ConfigureRoutes( IRouteBuilder routes)
        {

        }
    }

}
