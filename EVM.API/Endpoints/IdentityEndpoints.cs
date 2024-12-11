namespace EVM.API.Endpoints;

public static class IdentityEndpoints
{
    private static readonly string Tag = "Identity";

    public static void Register(WebApplication app)
    {
        // Get current identity (GET)
        app.MapGet(Routes.Identity.Current, 
            () => Results.Ok())
            .RequireAuthorization()
            .WithTags(Tag);a

        // Sign in (post)
        app.MapPost(Routes.Identity.Signin, 
            () => Results.Ok())
            .WithTags(Tag);

        // Google Sign in (post)
        app.MapPost(Routes.Identity.GoogleSignin, 
            () => Results.Ok())
            .WithTags(Tag);

        // Meta Sign in (post) 
        app.MapPost(Routes.Identity.MetaSignin, 
            () => Results.Ok())
            .WithTags(Tag);

        // Apple Sign in (post)
        app.MapPost(Routes.Identity.AppleSignin, 
            () => Results.Ok())
            .WithTags(Tag);

        // Sign up route (post)
        app.MapPost(Routes.Identity.Signup, 
            () => Results.Ok())
            .WithTags(Tag);

        // Sign out route (post)
        app.MapPost(Routes.Identity.Signout, 
            () => Results.Ok())
            .RequireAuthorization()
            .WithTags(Tag);

        // Forgot password (post)
        app.MapPost(Routes.Identity.Forgot, 
            () => Results.Ok())
            .WithTags(Tag);

        // Reset password (post)
        app.MapPost(Routes.Identity.Reset, 
            () => Results.Ok())
            .WithTags(Tag);
    }
}
