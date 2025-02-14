using EVM.Services.Features.Event.Commands;
using EVM.Services.Features.Event.Models.Requests;
using EVM.Services.Features.Event.Query;
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

        app.MapGet(Routes.Event.Base,
            ([FromServices] GetAllEventsQueryHandler queryHandler,
            CancellationToken cancellationToken) => queryHandler.Handle(cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapGet(Routes.Event.Exact("id"),
            ([FromServices] GetByIdEventQueryHandler queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) => queryHandler.Handle(id, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapPatch(Routes.Event.Exact("id"),
            ([FromServices] UpdateEventCommandHandler queryHandler,
            [FromBody] UpdateEventRequest request,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) => queryHandler.Handle(id, request, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapDelete(Routes.Event.Exact("id"),
            ([FromServices] DeleteEventCommandHandler queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) => queryHandler.Handle(id, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapGet(Routes.Event.EventTask.Exact("id"),
            ([FromServices] GetAllTaskQueryHandler queryHandler,
            [FromRoute] Guid eventId,
            CancellationToken cancellationToken) => queryHandler.Handle(eventId, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapGet(Routes.Event.EventTask.Exact("id"),
            ([FromServices] GetByIdEventQueryHandler queryHandler,
            [FromRoute] Guid taskId,
            CancellationToken cancellationToken) => queryHandler.Handle(taskId, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);
    }
}
