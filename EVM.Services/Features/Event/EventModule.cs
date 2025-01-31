using EVM.Services.Features.Event.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services.Features.Event;

public class EventModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<CreateEventCommandHandler>();
        services.AddScoped<CreateEventTasksCommandHandler>();
    }
}
