namespace EVM.Services.Features.Identity.Models.Responses;

public class SigninResponse
{
    public Guid UserId { get; set; }

    public string? Token { get; set; }

    public bool? IsLocked { get; set; }

    public bool? Requires2FA { get; set; }
}
