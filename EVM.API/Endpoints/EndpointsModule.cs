namespace EVM.API.Endpoints;

public static class EndpointsModule
{
    public static void Register(WebApplication app)
    {
        IdentityEndpoints.Register(app);
        UsersEndpoints.Register(app);
        PaymentsEndpoints.Register(app);
        EnvironmentEndpoints.Register(app);
        EnvironmentVariableEndpoints.Register(app);
    }
}
