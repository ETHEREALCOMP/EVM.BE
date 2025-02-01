using EVM.Services.Features.Event.Commands;
using EVM.Services.Features.Event.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EVM.API.Endpoints;

public class EventEndpoints
{
    private static readonly string Tag = "Event";

    public static void Register(WebApplication app)
    {
        app.MapPost(Routes.Event.Base,
            ([FromServices] CreateEventCommandHandler commandHandler,
            [FromBody] CreateEventRequest request,
            CancellationToken cancellationToken) => commandHandler.Handle(request, cancellationToken))
            .AllowAnonymous()
            .WithTags(Tag);

        app.MapPost(Routes.Event.EventTask.Base,
            ([FromServices] CreateEventTasksCommandHandler commandHandler,
            [FromBody] CreateEventTaskRequest request,
            CancellationToken cancellationToken) => commandHandler.Handle(request, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);
    }
}
