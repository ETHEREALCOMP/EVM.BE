﻿namespace EVM.Services.Features.Identity.Models.Requests;

public record RegisterRequest
{
    public required string UserName { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
