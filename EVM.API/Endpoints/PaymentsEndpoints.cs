using Microsoft.AspNetCore.Mvc;

namespace EVM.API.Endpoints;

public static class PaymentsEndpoints
{
    private static readonly string Tag = "Payments";

    public static void Register(WebApplication app)
    {
        // Payment intent (POST)
        app.MapPost(Routes.Payment.Intent, 
            (/*[FromServices]*/) => Results.Ok())
            .WithTags(Tag);

        // Payment hook (POST)
        app.MapPost(Routes.Payment.Hook, 
            (/*[FromServices]*/) => Results.Ok())
            .WithTags(Tag);

        // Subscription intent (POST)
        app.MapPost(Routes.Payment.Subscription.Intent,
            (/*[FromServices]*/) => Results.Ok()).WithTags(Tag);

        // Subscription renew (POST)
        app.MapPost(Routes.Payment.Subscription.Renew, 
            (/*[FromServices]*/) => Results.Ok())
            .WithTags(Tag);
    }
}
