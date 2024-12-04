using System.Net;

namespace EVM.Services.Features.Models.Responses;

public class ApiResponse<TData>
    where TData : class
{
    public TData? Data { get; set; }

    public List<ApiError<TData>>? Errors { get; set; }

    public List<ApiError>? Warnings { get; set; }

    public ApiResponse(HttpStatusCode errorCode, string message, List<ApiError>? warnings = null)
    {
        Errors = [
            new()
            {
                Message = message,
                Code = ((int)errorCode).ToString(),
            },
        ];
        Warnings = warnings;
    }

    public ApiResponse(HttpStatusCode errorCode, string message, TData data, List<ApiError>? warnings = null)
    {
        Data = data;
        Warnings = warnings;

        Errors = [
            new()
            {
                Message = message,
                Code = ((int)errorCode).ToString(),
            },
        ];
    }

    public ApiResponse(TData data, List<ApiError>? warnings = null)
    {
        Data = data;
        Warnings = warnings;
    }

    public ApiResponse()
    {
    }
}