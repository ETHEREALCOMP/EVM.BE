using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EVM.Services.Extensions;

public static class DIExtensions
{
    public static void AddAllImplementations<T>(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var types = assembly.GetTypes()
                            .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in types)
        {
            services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }
    }
}