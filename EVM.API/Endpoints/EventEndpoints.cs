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
            ([FromServices] CreateEventCommand command,
            [FromBody] CreateEventRequest request,
            CancellationToken cancellationToken) => command.ExecuteAsync(request, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);
    }
}
