using EVM.Services.Features.Event;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services;

public static class ServicesModule
{
    public static void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        EventModule.Register(services);
    }
}
