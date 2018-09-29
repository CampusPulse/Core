using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CampusPulse.Core.Service.Bootstrap
{
    public class CacheConfigurationManager
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration["RedisCacheOptions:Configuration"];
                options.InstanceName = configuration["RedisCacheOptions:Catalog"];
            });
        }
    }
}
