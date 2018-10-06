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
using Swashbuckle.AspNetCore.Swagger;
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
            services.AddServiceLogging();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "hello",//this.configuration.GetSection("service:title").ToString(),
                    Description = "hello description",//this.configuration.GetSection("service:description").ToString(),
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "{service.companyname}", Email = "{service.companyemail}", Url = "{service.companyemail}" }
                });
            });
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

            app.UseMvc(routes =>
            {
                routes.MapRoute("service-status", "service-status", defaults: new { controller = "Status", Action = "GetStatus" });
                ConfigureRoutes(routes);
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1");
            });

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
