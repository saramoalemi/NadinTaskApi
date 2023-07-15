using System.Reflection;

namespace NadinTask.API.Services
{
    public static class DependencyInjectionServices
    {
        public static void RegisterServicesDependencies(this IServiceCollection services)
        {
            var classes = Assembly.Load("NadinTask.Application").GetTypes().Where(x => x.IsClass).ToList();
            Assembly.Load("NadinTask.Application").GetTypes()
                .Where(x => x.IsInterface).ToList().ForEach(i =>
                {
                    classes.Where(c => i.IsAssignableFrom(c)).ToList().ForEach(c =>
                    {
                        services.Add(new ServiceDescriptor(i, c, ServiceLifetime.Transient));
                    });
                });
        }
        }
  }
