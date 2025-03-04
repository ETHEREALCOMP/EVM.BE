﻿using EVM.Services.Features.Event.Commands;
using EVM.Services.Features.Event.Query;
using EVM.Services.Features.EventTask.Commands;
using EVM.Services.Features.EventTask.Query;
using Microsoft.Extensions.DependencyInjection;

namespace EVM.Services.Features.Event;

public class EventModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<CreateEventCommandHandler>();
        services.AddScoped<GetAllEventsQueryHandler>();
        services.AddScoped<GetByIdEventQueryHandler>();
        services.AddScoped<UpdateEventCommandHandler>();
        services.AddScoped<DeleteEventCommandHandler>();
    }
}
