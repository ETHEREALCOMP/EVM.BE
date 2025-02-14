using EVM.Services.Features.Resourse.Commands;
using EVM.Services.Features.Resourse.Models.Requests;
using EVM.Services.Features.Resourse.Query;
using Microsoft.AspNetCore.Mvc;
using Stripe.Forwarding;

namespace EVM.API.Endpoints;

public class ResourceEndpoints
{
    private static readonly string Tag = "Resourse";

    public static void Register(WebApplication app)
    {
        app.MapPost(Routes.Resource.Base,
            ([FromServices] CreateResourceCommandHandler commandHandler,
            [FromBody] CreateResourceRequest request,
            CancellationToken cancellationToken) => commandHandler.Handle(request, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapGet(Routes.Resource.Base,
            ([FromServices] GetResourceQueryHandler queryHandler,
            CancellationToken cancellationToken) => queryHandler.Handle(cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapGet(Routes.Resource.Exact("id"),
            ([FromServices] GetByIdResourceQueryHandler queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) => queryHandler.Handle(id, cancellationToken))
            .RequireAuthorization()
            .WithTags(Tag);

        app.MapDelete(Routes.Resource.Exact("id"),
           ([FromServices] DeleteResourcesCommandHandler commandHandler,
           [FromRoute] Guid id,
           CancellationToken cancellationToken) => commandHandler.Handle(id, cancellationToken))
           .RequireAuthorization()
           .WithTags(Tag);

        app.MapPatch(Routes.Resource.Exact("id"),
           ([FromServices] UpdateResourceCommandHandler commandHandler,
           [FromRoute] Guid id,
           [FromBody] UpdateResourceRequest request,
           CancellationToken cancellationToken) => commandHandler.Handle(id, request, cancellationToken))
           .RequireAuthorization()
           .WithTags(Tag);
    }
}
