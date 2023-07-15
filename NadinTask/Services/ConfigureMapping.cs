using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace NadinTask.API.Services
{
    public static class ConfigureMapping
    {
        public static void ConfigureMappingProfiles(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            var mapperConfig = new MapperConfiguration(map =>
            {
                System.Reflection.Assembly.Load("NadinTask.Domain").GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x))
                .ToList().ForEach(x => map.AddProfile(x));

              
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
