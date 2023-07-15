using System.Reflection;

namespace NadinTask.API.Services
{
    public static class DependencyInjectionRepository
    {
        public static void RegisterRepositoryDependencies(this IServiceCollection services)
        {
            var classes = Assembly.Load("NadinTask.Infrastructure").GetTypes().Where(x => x.IsClass).ToList();
            Assembly.Load("NadinTask.Infrastructure").GetTypes()
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
