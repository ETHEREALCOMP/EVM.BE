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

    public static class Event
    {
        public static string Base => "event";

        public static string Exact(string paramName) => Base + RouteParam.Exact(paramName);

        public static class EventTask
        {
            public static string Base => $"{Event.Base}/task";

            public static string Exact(string paramName) => Base + RouteParam.Exact(paramName);
        }
    }

    public static class Resource
    {
        public static string Base => "resource";

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