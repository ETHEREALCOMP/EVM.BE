namespace EVM.Services.Features.Identity.Models.Requests;

public record LoginRequest
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
