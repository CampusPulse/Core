using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusPulse.Core.Service.Bootstrap
{
    public static class LoggingConfigurationManager
    {
        public static IServiceCollection AddServiceLogging(this IServiceCollection services)
        {
            return services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));            
        }
      
    }
}
