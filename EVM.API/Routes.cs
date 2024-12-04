namespace EVM.API;

public static class Routes
{
    public static class Identity
    {
        public static string Base => "identity";

        public static string Current => $"{Base}/current";

        public static string Signin => $"{Base}/signin";

        public static string GoogleSignin => $"{Base}/signin/google";

        public static string MetaSignin => $"{Base}/signin/meta";

        public static string AppleSignin => $"{Base}/signin/apple";

        public static string Signup => $"{Base}/signup";

        public static string Signout => $"{Base}/logout";

        public static string Forgot => $"{Base}/forgot";

        public static string Reset => $"{Base}/reset";
    }

    public static class Projects
    {
        public static string Base => "project";

        public static string Exact(string projectId) => Base + RouteParam.Exact(projectId);
    }

    public static class Environment
    {
        public static string Base => "environments";

        public static string CreateEnv => $"{Base}/create";

        public static string Exact(string paramName) => Base + RouteParam.Exact(paramName);
    }

    public static class EnvironmentVariable
    {
        public static string Base => "environment-variables";

        public static string Exact(string paramName) => Base + RouteParam.Exact(paramName);
    }

    public static class Payment
    {
        public static string Base => "pay";

        public static string Intent => $"{Base}/intent";

        public static string Hook => $"{Base}/hook";

        public static class Subscription
        {
            public static string Base => $"{Payment.Base}/subscription";

            public static string Intent => $"{Base}/intent";

            public static string Renew => $"{Base}/renew";
        }
    }
    
    public static class RouteParam
    {
        public static string Exact(string? paramName)
        {
            return String.IsNullOrWhiteSpace(paramName) ? String.Empty : "/{" + paramName + "}";
        }
    }
}