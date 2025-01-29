namespace EVM.Services.Features.Identity.Models.Const;

public class AuthSettings
{
    public static readonly TimeSpan CookiesExpiration = TimeSpan.FromDays(30);

    public static readonly string CookieName = "EVM.Auth";
}
