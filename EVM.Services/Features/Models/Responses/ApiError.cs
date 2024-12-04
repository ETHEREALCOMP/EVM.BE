namespace EVM.Services.Features.Models.Responses;

public class ApiError
{
    public required string Message { get; set; }

    public required string Code { get; set; }
}