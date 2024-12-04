namespace EVM.Services.Features.Models.Responses;

public class ApiError<TData>
    where TData : class
{
    public required string Message { get; set; }

    public required string Code { get; set; }

    public TData? Data { get; set; }
}