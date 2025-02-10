using EVM.Services.Features.Event.Commands;
using EVM.Services.Features.Event.Query;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services.Features.Event;

public class EventModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<CreateEventCommandHandler>();
        services.AddScoped<CreateEventTasksCommandHandler>();
        services.AddScoped<GetEventsQueryHandler>();
        services.AddScoped<GetByIdEventQueryHandler>();
        services.AddScoped<UpdateEventCommandHandler>();
    }
}
