using EVM.Services.Features.Event.Query;
using EVM.Services.Features.EventTask.Commands;
using EVM.Services.Features.EventTask.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EVM.API.Endpoints;

public class EventTaskEndpoint
{
    private static readonly string Tag = "EventTask";

    public static void Register(WebApplication app)
    {
        app.MapPost(Routes.EventTask.Base,
            ([FromServices] CreateEventTasksCommandHandler commandHandler,
            [FromBody] CreateEventTaskRequest request,
            CancellationToken cancellationToken) => commandHandler.Handle(request, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapPatch(Routes.EventTask.Exact("id"),
            ([FromServices] UpdateEventTaskCommandHandler commandHandler,
            [FromBody] UpdateTaskRequest request,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) => commandHandler.Handle(id, request, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapDelete(Routes.EventTask.Exact("id"),
            ([FromServices] DeleteEventTaskCommandHandler queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) => queryHandler.Handle(id, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapGet(Routes.EventTask.Exact("id"),
            ([FromServices] GetByIdEventQueryHandler queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) => queryHandler.Handle(id, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);
    }
}
