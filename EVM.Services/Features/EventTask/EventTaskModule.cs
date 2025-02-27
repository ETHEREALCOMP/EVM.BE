using EVM.Services.Features.Event.Commands;
using EVM.Services.Features.Event.Query;
using EVM.Services.Features.EventTask.Commands;
using EVM.Services.Features.EventTask.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Services.Features.EventTask;

public class EventTaskModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<CreateEventTasksCommandHandler>();
        services.AddScoped<GetAllEventTaskQueryHandler>();
        services.AddScoped<GetByIdEventTaskQueryHandler>();
        services.AddScoped<UpdateEventTaskCommandHandler>();
        services.AddScoped<DeleteEventTaskCommandHandler>();
    }
}
