namespace EVM.Services.Features.Identity.Models.Requests;

public class SignupRequest
{
    public required string Email { get; set; }

    public required string Name { get; set; }

    public required string Password { get; set; }

    public required string UserName { get; set; }
}
