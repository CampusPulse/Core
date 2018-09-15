using CampusPulse.Core.Service.Filter;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace CampusPulse.Core.Service.Bootstrap
{
    public class MvcConfigurationManager
    {
        public static void ConfigureService(IServiceCollection services)
        {

            var mvcBuilder = services.AddMvc(options =>
            {
                options.Filters.Add(new ServiceExceptionFilterAtribute(null));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });


        }
    }
}
