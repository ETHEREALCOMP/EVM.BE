namespace EVM.Services.Features.Identity.Models.Requests;

public class SigninRequest
{
    public required string Username { get; set; }

    public required string Password { get; set; }

    public bool RememberMe { get; set; }
}
